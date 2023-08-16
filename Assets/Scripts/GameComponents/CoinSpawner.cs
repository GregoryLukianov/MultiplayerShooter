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

        private void Start()
        {
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                StartCoroutine(SpawnCoins());
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
                yield return new WaitForSeconds(3);
                
                PhotonNetwork.InstantiateRoomObject("Bonuses/CoinPrefab", 
                    new Vector2(Random.Range(-7,7),Random.Range(-4,4)), 
                    Quaternion.identity);
            }
        }
    }
}