namespace Deirin.Utilities {
    using UltEvents;
    using UnityEngine;

    public class StartCall : MonoBehaviour {
        public UltEvent OnStart;
        private void Start () {
            OnStart.Invoke();
        }
    }
}