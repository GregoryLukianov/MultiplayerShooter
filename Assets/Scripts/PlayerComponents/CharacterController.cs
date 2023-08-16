using System;
using Photon.Pun;
using UnityEngine;

namespace PlayerComponents
{
    public class CharacterController: MonoBehaviour
    {
        [SerializeField]private Player _player;
        private Animator _animator;
        private PhotonView _photonView;
        private SpriteRenderer _spriteRenderer;
        private SpriteRenderer _gunSpriteRenderer;
        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _gunSpriteRenderer = _player.GetComponentInChildren<Gun>().GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            transform.position = _player.transform.position;

            if (_player.Rigidbody.velocity.x >0)
            {
                _spriteRenderer.flipX = false;
                _gunSpriteRenderer.flipY = false;
            }

            if (_player.Rigidbody.velocity.x <0)
            {
                _spriteRenderer.flipX = true;
                _gunSpriteRenderer.flipY = true;
            }
            
            
            if (_player.Rigidbody.velocity != Vector2.zero)
            {
                _animator.SetBool("Run",true);
            }
            else
            {
                _animator.SetBool("Run",false);
            }
        }
    }
}