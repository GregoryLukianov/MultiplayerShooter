using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LobbyComponents
{
    public class Lobby: MonoBehaviourPunCallbacks
    {
        [Header("Room creation")]
        [SerializeField] private TMP_InputField _newRoomInputField;
        [SerializeField] private Button _createButton;
        
        [Header("Joining existing room")]
        [SerializeField] private TMP_InputField _existingRoomInputField;
        [SerializeField] private Button _joinButton;

        private void Start()
        {
            _createButton.onClick.AddListener(CreateRoom);
            _joinButton.onClick.AddListener(JoinRoom);
        }

        public void CreateRoom()
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            PhotonNetwork.CreateRoom(_newRoomInputField.text, roomOptions);
        }

        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(_existingRoomInputField.text);
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("Game");
        }
        
        


    }
}