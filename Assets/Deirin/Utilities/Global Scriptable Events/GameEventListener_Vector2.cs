namespace Deirin.Utilities {
    using UnityEngine;

    public class GameEventListener_Vector2 : MonoBehaviour {
        public GameEvent_Vector2 gameEvent;
        public UnityEvent_Vector2 response;

        private void OnEnable () {
            gameEvent.Subscribe( this );
        }

        private void OnDisable () {
            gameEvent.Unsubscribe( this );
        }

        public void OnInvoke ( Vector2 value ) {
            response.Invoke( value );
        }
    }
}