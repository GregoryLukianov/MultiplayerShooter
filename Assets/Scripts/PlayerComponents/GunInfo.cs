using UnityEngine;

namespace PlayerComponents
{
    [CreateAssetMenu(fileName = "GunInfo", menuName= "Gun/New GunInfo")]
    public class GunInfo: ScriptableObject
    {
        [SerializeField] private float _damage;
        public float Damage => _damage;
        
        [SerializeField] private float _fireRate;
        public float FireRate => _fireRate;

        [SerializeField] private float _bulletSpeed;
        public float BulletSpeed => _bulletSpeed;

        [SerializeField] private GameObject _bulletPrefab;
        public GameObject BulletPrefab => _bulletPrefab;


    }
}