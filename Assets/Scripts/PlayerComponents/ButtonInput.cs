using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerComponents
{
    public class ButtonInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
 
        public bool IsDown { get; private set; }
        
     
        public void OnPointerDown(PointerEventData eventData) {
            IsDown = true;
        }
     
        public void OnPointerUp(PointerEventData eventData) {
            IsDown = false;
        }
    }
}

