using UnityEngine;

namespace PlayerComponents
{
    public class SimpleBullet: MonoBehaviour
    {
        public Photon.Realtime.Player Owner { get; private set; }

        private float _damage;

        public void Start()
        {
            Destroy(gameObject, 3.0f);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var col = collision.gameObject.GetComponent<Damageable>();
            if (col)
            {
                col.GetDamage(_damage);
                Debug.Log(1);
            }
            Destroy(gameObject);
        }
        

        public void InitializeBullet(Photon.Realtime.Player owner, float damage, float speed, Vector2 originalDirection, float lag)
        {
            Owner = owner;
            _damage = damage;

            //transform.forward = originalDirection;

            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.velocity = originalDirection * speed;
            rigidbody.position += rigidbody.velocity * lag;
        }
    }
}