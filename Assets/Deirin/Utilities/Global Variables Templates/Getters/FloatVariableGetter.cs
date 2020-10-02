namespace Deirin.Utilities {
    using UnityEngine;

    public class FloatVariableGetter : MonoBehaviour {
        public FloatVariable variable;

        public UnityEvent_Float OnValueChange;
        public UnityEvent_Float OnGet;

        private void OnEnable () {
            variable.OnValueChanged += ValueChangeHandler;
        }

        private void OnDisable () {
            variable.OnValueChanged -= ValueChangeHandler;
        }

        private void ValueChangeHandler ( float obj ) {
            OnValueChange.Invoke( obj );
        }

        public float Get () {
            OnGet.Invoke( variable.Value );
            return variable.Value;
        }

        public void InvokeGet () {
            OnGet.Invoke( variable.Value );
        }
    }
}