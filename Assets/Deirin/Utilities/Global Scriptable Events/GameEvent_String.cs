namespace Deirin.Utilities {
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu( menuName = "Deirin/Utilities/Global Game Events/String" )]
    public class GameEvent_String : ScriptableObject {
        [SerializeField] private string value;
        public System.Action<string> OnInvoke;

        private List<GameEventListener_String> listeners = new List<GameEventListener_String>();

        public void Subscribe ( GameEventListener_String listener ) {
            listeners.Add( listener );
        }

        public void Unsubscribe ( GameEventListener_String listener ) {
            listeners.Remove( listener );
        }

        public void Invoke () {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( value );
            }
        }

        public void Invoke ( string value ) {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( value );
            }
            OnInvoke?.Invoke( value );
        }
    }
}