namespace Deirin.Utilities {
    using UltEvents;
    using UnityEngine;

    public class OnEnableCall : MonoBehaviour {
        public UltEvent OnEnableEvent;

        private void OnEnable () {
            OnEnableEvent.Invoke();
        }
    }
}