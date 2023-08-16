using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerComponents
{
    public class ButtonInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
 
        protected bool _isDown;

        public bool IsDown => _isDown;
     
        public void OnPointerDown(PointerEventData eventData) {
            _isDown = true;
        }
     
        public void OnPointerUp(PointerEventData eventData) {
            _isDown = false;
        }
    }
}

