namespace Deirin.Utilities {
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu( menuName = "Deirin/Utilities/Global Game Events/Transform" )]
    public class GameEvent_Transform : ScriptableObject {
        [SerializeField] private Transform value;
        public System.Action<Transform> OnInvoke;

        private List<GameEventListener_Transform> listeners = new List<GameEventListener_Transform>();

        public void Subscribe ( GameEventListener_Transform listener ) {
            listeners.Add( listener );
        }

        public void Unsubscribe ( GameEventListener_Transform listener ) {
            listeners.Remove( listener );
        }

        public void Invoke () {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( value );
            }
        }

        public void Invoke ( Transform value ) {
            for ( int i = 0; i < listeners.Count; i++ ) {
                listeners[i].OnInvoke( value );
            }
            OnInvoke?.Invoke( value );
        }
    }
}