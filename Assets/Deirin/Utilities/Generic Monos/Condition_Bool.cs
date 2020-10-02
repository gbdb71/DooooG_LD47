namespace Deirin.Utilities {
    using UltEvents;
    using UnityEngine;

    public class Condition_Bool : MonoBehaviour {
        public bool value;

        [Header("Events")]
        public UltEvent True;
        public UltEvent False;

        public void CheckCondition ( bool value ) {
            if ( value )
                True.Invoke();
            else
                False.Invoke();
        }

        public void CheckCondition () {
            if ( value )
                True.Invoke();
            else
                False.Invoke();
        }
    }
}