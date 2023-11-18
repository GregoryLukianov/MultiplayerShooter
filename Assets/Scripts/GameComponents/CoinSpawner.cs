using System;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameComponents
{
    public class CoinSpawner: MonoBehaviourPunCallbacks
    {
        [SerializeField] private float _coinRespawnTime;

        private bool _ready;

        private void Start()
        {
            // if (PhotonNetwork.LocalPlayer.IsMasterClient)
            // {
            //     StartCoroutine(SpawnCoins());
            // }
        }

        private void Update()
        {
            if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                return;
            
            if (_ready)
                return;

            if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
            {
                _ready = true;
            }
            else
            {
                return;
            }
                

            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                StartCoroutine(SpawnCoins());
                Debug.Log(1);
            }
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
            {
                StartCoroutine(SpawnCoins());
            }
        }


        private IEnumerator SpawnCoins()
        {
            while (true)
            {
                yield return new WaitForSeconds(_coinRespawnTime);
                
                PhotonNetwork.InstantiateRoomObject("Bonuses/CoinPrefab", 
                    new Vector2(Random.Range(-7,7),Random.Range(-4,4)), 
                    Quaternion.identity);
            }
        }
    }
}