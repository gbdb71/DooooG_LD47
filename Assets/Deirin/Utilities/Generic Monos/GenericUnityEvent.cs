namespace Deirin.Utilities {
    using UltEvents;
    using UnityEngine;

    public class GenericUnityEvent : MonoBehaviour {
        public UltEvent OnInvoke;

        public void Invoke () => OnInvoke.Invoke();
    }
}