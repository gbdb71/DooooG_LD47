namespace Deirin.Utilities {
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu( menuName = "Deirin/Utilities/Global Game Events/Int" )]
    public class GameEvent_Int : ScriptableObject {
        [SerializeField] private int value;
        public System.Action<float> OnInvoke;

        private List<GameEventListener_Int> listeners = new List<GameEventListener_Int>();

        public void Subscribe ( GameEventListener_Int listener ) {
            listeners.Add( listener );
        }

        public void Unsubscribe ( GameEventListener_Int listener ) {
            listeners.Remove( listener );
        }

        public void Invoke () {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( value );
            }
        }

        public void Invoke ( int value ) {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( value );
            }
            OnInvoke?.Invoke( value );
        }
    }
}