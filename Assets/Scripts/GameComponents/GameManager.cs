using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Player = PlayerComponents.Player;

namespace GameComponents
{
    public class GameManager: MonoBehaviourPunCallbacks
    {
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private Image _winPannel;

        public List<Player> _players;
        
        
        private IEnumerator EndOfGame(string winner, int score)
        {
            float timer = 5.0f;
            GetComponent<AudioSource>().Play();
            _winPannel.gameObject.SetActive(true);
            while (timer > 0.0f)
            {
                
                _infoText.text = string.Format("Player {0} won with {1} points.\n\n\nReturning to login screen in {2} seconds.", winner, score, timer.ToString("n2"));

                yield return new WaitForEndOfFrame();

                timer -= Time.deltaTime;
            }

            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Lobby");
        }
        



        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount==1)
            {
                foreach (var VARIABLE in _players)
                {
                    if (VARIABLE.PhotonView.IsMine & VARIABLE.IsAlive)
                    {
                        StartCoroutine(EndOfGame(VARIABLE.PlayerName, VARIABLE.CoinsCollected));
                    }
                }
            }
        }

        public void CheckEnd()
        {
            var counter = _players.Count;
            foreach (var VARIABLE in _players)
            {
                if (VARIABLE.IsAlive ==false)
                {
                    counter -= 1;
                }
            }

            if (counter ==1)
            {
                foreach (var VARIABLE in _players)
                {
                    if (VARIABLE.IsAlive)
                    {
                        StartCoroutine(EndOfGame(VARIABLE.PlayerName, VARIABLE.CoinsCollected));
                    }
                }
            }
        }
    }
}