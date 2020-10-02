namespace Deirin.Utilities {
    using UnityEngine;

    public class GameEventListener_Transform : MonoBehaviour {
        public GameEvent_Transform gameEvent;
        public UnityEvent_Transform response;

        private void OnEnable () {
            gameEvent.Subscribe( this );
        }

        private void OnDisable () {
            gameEvent.Unsubscribe( this );
        }

        public void OnInvoke ( Transform value ) {
            response.Invoke( value );
        }
    }
}