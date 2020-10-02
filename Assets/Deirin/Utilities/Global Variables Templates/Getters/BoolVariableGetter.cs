namespace Deirin.Utilities {
    using UnityEngine;

    public class BoolVariableGetter : MonoBehaviour {
        public BoolVariable variable;

        public UnityEvent_Bool OnValueChange;
        public UnityEvent_Bool OnGet;

        private void OnEnable () {
            variable.OnValueChanged += ValueChangeHandler;
        }

        private void OnDisable () {
            variable.OnValueChanged -= ValueChangeHandler;
        }

        private void ValueChangeHandler ( bool obj ) {
            OnValueChange.Invoke( obj );
        }

        public bool Get () {
            OnGet.Invoke( variable.Value );
            return variable.Value;
        }

        public void InvokeGet () {
            OnGet.Invoke( variable.Value );
        }
    }
}