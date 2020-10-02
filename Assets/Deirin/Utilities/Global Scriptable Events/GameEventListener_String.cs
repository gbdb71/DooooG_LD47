namespace Deirin.Utilities {
    using UnityEngine;
    using UnityEngine.Events;

    public class GameEventListener_String : MonoBehaviour {
        public GameEvent_String gameEvent;
        public UnityEvent_String response;

        private void OnEnable () {
            gameEvent.Subscribe( this );
        }

        private void OnDisable () {
            gameEvent.Unsubscribe( this );
        }

        public void OnInvoke ( string value ) {
            response.Invoke( value );
        }
    }
}