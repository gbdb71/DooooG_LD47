namespace Deirin.Utilities {
    using UltEvents;
    using UnityEngine;

    public class AwakeCall : MonoBehaviour {
        public UltEvent OnAwake;
        private void Awake () {
            OnAwake.Invoke();
        }
    }
}