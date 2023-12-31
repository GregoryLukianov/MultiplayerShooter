﻿using System;
using GameComponents;
using Photon.Pun;
using UnityEngine;

namespace PlayerComponents
{
    public class Player: Damageable
    {
        public string PlayerName { get; private set; }
        
        
        private Joystick _input;

        [SerializeField] private float _speed;
        [SerializeField] private float _healthPoints;
        private float _healthPointsLeft; 
        public bool IsAlive { get; private set; }

        [SerializeField] private GameObject _gunPrefab;
        private Gun _gun;
        [SerializeField] private GunType _gunType;

        public int CoinsCollected { get; private set; }
        public int MaxCoins { get; private set; }

        private PhotonView _photonView;
        private Rigidbody2D _rigidbody;
        private AudioSource _audioSource;
        private ButtonInput _shootButton;
        
        public Rigidbody2D Rigidbody => _rigidbody;
        public PhotonView PhotonView => _photonView;
        

        public event Action OnCoinCollected;
        

        private void Start()
        {
            _healthPointsLeft = _healthPoints;
            IsAlive = true;
            CoinsCollected = 0;
            MaxCoins = 20;
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _photonView = GetComponent<PhotonView>();
            _audioSource = GetComponent<AudioSource>();

            _input = FindObjectOfType<Joystick>();
            _shootButton = FindObjectOfType<ButtonInput>();
            
            InitializeGun();
            
            AddPlayerToArray();
            SetColor();
        }

        private void InitializeGun()
        {
            var gunData = ScriptableObject.CreateInstance<GunInfo>();
            
            switch (_gunType)
            {
                case GunType.PISTOL:
                    _gun = _gunPrefab.AddComponent<Gun>();
                    gunData = Resources.Load<GunInfo>("Data/Guns/Pistol");
                    break;
                case GunType.SHOTGUN:
                    
                    break;
                case GunType.SNIPER:
                    
                    break;
            }
            
            _gun.Initialize(_photonView,_rigidbody,_shootButton,gunData.Damage,gunData.FireRate,gunData.BulletSpeed,gunData.BulletPrefab);
        }

        private void Update()
        {
            if(!IsAlive)
                return;
            
            var x = _input.Horizontal;
            var y = _input.Vertical;
            
            if (!_photonView.IsMine)
                return;
            
            if (PhotonNetwork.CurrentRoom.PlayerCount<2)
                return;
            
            
            Rotate(x,y);
            Move(x,y);
        }

        private void Move(float x,float y)
        {
            _rigidbody.velocity = new Vector2(x * _speed, y * _speed);
        }

        private void Rotate(float x, float y)
        {
            if(new Vector2(x,y) == Vector2.zero)
                return;
            
            float angle = Mathf.Atan2(y,x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        private void AddPlayerToArray()
        {
            var gameManager = FindObjectOfType<GameManager>();
            if (!gameManager._players.Contains(this))
            {
                gameManager._players.Add(this);
                OnDeath += gameManager.CheckEnd;
            }
        }
        
        public override float GetHealthPointsLeft()
        {
            return _healthPointsLeft / _healthPoints;
        }

        public override void GetDamage(float damage)
        {
            if(damage<0)
                return;
            
            if(!IsAlive)
                return;

            _healthPointsLeft -= damage;
            
            _audioSource.Play();
            
            RiseHit();
            CheckIsAlive();
        }

        private void CheckIsAlive()
        {
            if (_healthPointsLeft<=0)
            {
                Death();
                RiseDeath();
            }
        }

        private void Death()
        {
            IsAlive = false;
            _gun.CanShoot = false;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Coin"))
            {
                Destroy(col.gameObject);
                CoinsCollected += 1;
                OnCoinCollected?.Invoke();
            }
        }
        
        
        private void SetColor()
        {
            if (!_photonView.IsMine)
                return;
            
            var p = PhotonNetwork.CurrentRoom.PlayerCount;
            _photonView.RPC("SetColor",RpcTarget.AllBuffered,p);
        }
        
        [PunRPC]
        private void SetColor(int p)
        {
            switch (p)
            {
                case 1:
                    PlayerName = "Blue Boy";
                    break;
                case 2:
                    PlayerName = "Pink Girl";
                    break;
                case 3:
                    PlayerName = "PurpleBoy";
                    break;
                case 4:
                    PlayerName = "OrangeGirl";
                    break;
            }
        }
    }
}