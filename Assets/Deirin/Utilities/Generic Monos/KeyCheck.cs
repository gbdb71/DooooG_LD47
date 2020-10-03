namespace Deirin.Utilities {
    using UltEvents;
    using UnityEngine;

    public class KeyCheck : MonoBehaviour {
        public KeyCode key;
        public UltEvent OnDown;
        public UltEvent OnUp;

        private void Update () {
            if ( Input.GetKeyDown( key ) )
                OnDown.Invoke();

            if ( Input.GetKeyUp( key ) )
                OnUp.Invoke();
        }
    }
}