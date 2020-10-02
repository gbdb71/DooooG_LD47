namespace Deirin.Utilities {
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu( menuName = "Deirin/Utilities/Global Game Events/Enum" )]
    public class GameEvent_Enum : ScriptableObject {
        [SerializeField] private System.Enum value;
        public System.Action<System.Enum> OnInvoke;

        private List<GameEventListener_Enum> listeners = new List<GameEventListener_Enum>();

        public void Subscribe ( GameEventListener_Enum listener ) {
            listeners.Add( listener );
        }

        public void Unsubscribe ( GameEventListener_Enum listener ) {
            listeners.Remove( listener );
        }

        /*
        public void Invoke () {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( value );
            }
        }*/

        public void Invoke ( System.Enum value ) {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( value );
            }
            OnInvoke?.Invoke( value );
        }

    }
}