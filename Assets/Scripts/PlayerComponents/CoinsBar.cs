using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerComponents
{
    public class CoinsBar: MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Image _bar;
        [SerializeField] private TextMeshProUGUI _counter;

        private void Start()
        {
            if (!_player.PhotonView.IsMine)
                gameObject.SetActive(false);
            _counter.text = "0";
            _player.OnCoinCollected += UpdateBar;
        }

        private void UpdateBar()
        {
            _bar.fillAmount = (float)_player.CoinsCollected / (float)_player.MaxCoins;
            _counter.text = _player.CoinsCollected.ToString();
        }
    }
}