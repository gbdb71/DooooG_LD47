namespace Deirin.Utilities {
    using UnityEngine;
    using UnityEngine.Events;

    public class GameEventListener_Bool : MonoBehaviour {
        public GameEvent_Bool gameEvent;
        public UnityEvent_Bool response;

        private void OnEnable () {
            gameEvent.Subscribe( this );
        }

        private void OnDisable () {
            gameEvent.Unsubscribe( this );
        }

        public void OnInvoke ( bool value ) {
            response.Invoke( value );
        }
    }
}