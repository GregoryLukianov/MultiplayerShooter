﻿using Photon.Pun;
using UnityEngine;


namespace GameComponents
{
    public class PlayerSpawner: MonoBehaviour
    {
        [SerializeField] private GameObject[] _characters;
        [SerializeField] private Transform[] _startPositions;

        private void Start()
        {
            var number = PhotonNetwork.CurrentRoom.PlayerCount-1;
            PhotonNetwork.Instantiate("Player/"+ _characters[number].name,_startPositions[number].position,Quaternion.identity);
        }
    }
}