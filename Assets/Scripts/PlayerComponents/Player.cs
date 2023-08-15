using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerComponents
{
    public class Player: MonoBehaviour
    {
        [SerializeField] private Joystick _input;
        [SerializeField] private float _speed;
        
        private PhotonView _photonView;
        private Rigidbody2D _rigidbody;


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _photonView = GetComponent<PhotonView>();

            _input = FindObjectOfType<Joystick>();
            
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