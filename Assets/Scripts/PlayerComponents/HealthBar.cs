using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerComponents
{
    public class HealthBar: MonoBehaviour
    {
        [SerializeField] private Image _bar;
        [SerializeField] private Image _barBackGround;
        [SerializeField] private Damageable _damageable;
        private Vector3 _offset;

        private void Start()
        {
            _damageable.OnHit += UpdateHealthBar;
            _damageable.OnDeath += Death;
            _offset = transform.position - _damageable.transform.position;

            SetColors();
        }

        private void Update()
        {
            transform.position = _damageable.transform.position + _offset;
        }

        private void SetColors()
        {
            if( GetComponent<PhotonView>().IsMine)
            {
                _bar.color = Color.green;
                _barBackGround.color = Color.white;
            }
            else
            {
                _bar.color = Color.red;
                _barBackGround.color = Color.black;
            }
        }

        private void UpdateHealthBar()
        {
            Debug.Log(_damageable.GetHealthPointsLeft());
            _bar.fillAmount = _damageable.GetHealthPointsLeft();
        }

        private void Death()
        {
            _damageable.OnHit -= UpdateHealthBar;
            _damageable.OnDeath -= Death;
            Destroy(gameObject);
        }
    }
}