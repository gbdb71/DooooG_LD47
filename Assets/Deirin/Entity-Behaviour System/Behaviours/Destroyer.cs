namespace Deirin.EB {
    using UltEvents;
    using UnityEngine;

    public class Destroyer : BaseBehaviour {
        [Header("Params")]
        public GameObject target;
        public float delay;

        [Header("Events")]
        public UltEvent OnDestruction;

        public void Destroy () {
            Destroy( target.gameObject );
            OnDestruction.Invoke();
        }
    }
}