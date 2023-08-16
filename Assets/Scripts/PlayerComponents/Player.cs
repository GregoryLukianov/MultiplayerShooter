using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace PlayerComponents
{
    public class Player: MonoBehaviour
    {
        [SerializeField] private Joystick _input;
        [SerializeField] private float _speed;

        [SerializeField] private GameObject _gunPrefab;
        private Gun _gun;
        [SerializeField] private GunType _gunType;
        
        private PhotonView _photonView;
        private Rigidbody2D _rigidbody;
        private ButtonInput _shootButton;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _photonView = GetComponent<PhotonView>();

            _input = FindObjectOfType<Joystick>();
            _shootButton = FindObjectOfType<ButtonInput>();
            
            InitializeGun();
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
            var x = _input.Horizontal;
            var y = _input.Vertical;
            
            if (!_photonView.IsMine)
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
        
    }
}