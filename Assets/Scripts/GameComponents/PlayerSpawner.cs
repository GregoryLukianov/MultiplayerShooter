using System;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameComponents
{
    public class PlayerSpawner: MonoBehaviour
    {
        [SerializeField] private GameObject[] _characters;
        [SerializeField] private Transform[] _startPositions;

        private void Start()
        {
            var number = Random.Range(0, _characters.Length);
            PhotonNetwork.Instantiate(_characters[number].name,_startPositions[number].position,Quaternion.identity);
        }
    }
}