namespace Deirin.Utilities {
    using UnityEngine;

    public class GameEventListener_Int : MonoBehaviour {
        public GameEvent_Int gameEvent;
        public UnityEvent_Int response;

        private void OnEnable () {
            gameEvent.Subscribe( this );
        }

        private void OnDisable () {
            gameEvent.Unsubscribe( this );
        }

        public void OnInvoke ( int value ) {
            response.Invoke( value );
        }
    }
}