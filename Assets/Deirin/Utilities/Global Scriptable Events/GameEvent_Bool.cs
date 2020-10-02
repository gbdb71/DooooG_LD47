namespace Deirin.Utilities {
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu( menuName = "Deirin/Utilities/Global Game Events/Bool" )]
    public class GameEvent_Bool : ScriptableObject {
        [SerializeField] private bool value;
        public System.Action<bool> OnInvoke;

        private List<GameEventListener_Bool> listeners = new List<GameEventListener_Bool>();

        public void Subscribe (GameEventListener_Bool listener ) {
            listeners.Add( listener );
        }

        public void Unsubscribe (GameEventListener_Bool listener ) {
            listeners.Remove( listener );
        }

        public void Invoke () {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( value );
            }
        }

        public void Invoke ( bool value ) {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( value );
            }
            OnInvoke?.Invoke( value );
        }
    }
}