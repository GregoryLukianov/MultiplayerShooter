using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerComponents
{
    public class Gun: MonoBehaviour
    {
        [SerializeField] protected float _damage;
        [SerializeField] protected float _fireRate;
        private float _shootingTimer;
        [SerializeField] protected float _bulletSpeed;
        [SerializeField] protected GameObject _bulletPrefab;

        private PhotonView _photonView;
        private Rigidbody2D _rigidbody;
        private ButtonInput _shootButton;

        private bool _isInitialized;


        protected virtual void Start()
        {
            _shootingTimer = 0.0f;
            _photonView = GetComponent<PhotonView>();
        }

        public virtual void Initialize(PhotonView photonView,Rigidbody2D rigidbody2D,ButtonInput buttonInput,float damage, float fireRate,float bulletSpeed,GameObject bulletPrefab)
        {
            
            _rigidbody = rigidbody2D;
            _shootButton = buttonInput;

            _damage = damage;
            _fireRate = fireRate;
            _bulletSpeed = bulletSpeed;
            _bulletPrefab = bulletPrefab;

            _isInitialized = true;
        }

        private void Update()
        {
            if(!_isInitialized)
                return;
            
            if (!_photonView.IsMine)
                return;
            Debug.Log(1);
            if (_shootButton.IsDown && _shootingTimer <= 0.0)
            {
                _shootingTimer = _fireRate;

                _photonView.RPC("Fire", RpcTarget.AllViaServer, transform.position, transform.rotation);
            }

            if (_shootingTimer > 0.0f)
            {
                _shootingTimer -= Time.deltaTime;
            }
        }
        
        
        [PunRPC]
        public void Fire(Vector3 position, Quaternion rotation, PhotonMessageInfo info)
        {
            float lag = (float) (PhotonNetwork.Time - info.SentServerTime);
            GameObject bullet;
            Debug.Log(2);
            bullet = Instantiate(_bulletPrefab, position, rotation);
            bullet.GetComponent<SimpleBullet>().InitializeBullet(_photonView.Owner,_damage,_bulletSpeed,  rotation*Vector2.right, Mathf.Abs(lag));
            
        }
        
        
        
        
    }
}