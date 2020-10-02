namespace Deirin.Utilities {
    using UnityEngine;
    using UnityEngine.Events;

    public class GameEventListener_Enum : MonoBehaviour {
        public GameEvent_Enum gameEvent;
        public UnityEvent_Enum response;

        private void OnEnable () {
            gameEvent.Subscribe( this );
        }

        private void OnDisable () {
            gameEvent.Unsubscribe( this );
        }

        virtual public void OnInvoke ( System.Enum beat ) {
            response.Invoke( beat );
        }

    }
}