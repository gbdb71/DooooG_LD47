namespace Deirin.Utilities {
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu( menuName = "Deirin/Utilities/Global Game Events/Vector2" )]
    public class GameEvent_Vector2 : ScriptableObject {
        [SerializeField] private Vector2 value;
        public System.Action<Vector2> OnInvoke;

        private List<GameEventListener_Vector2> listeners = new List<GameEventListener_Vector2>();

        public void Subscribe ( GameEventListener_Vector2 listener ) {
            listeners.Add( listener );
        }

        public void Unsubscribe ( GameEventListener_Vector2 listener ) {
            listeners.Remove( listener );
        }

        public void Invoke () {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( value );
            }
        }

        public void Invoke ( Vector2 value ) {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( value );
            }
            OnInvoke?.Invoke( value );
        }
    }
}