namespace Deirin.Utilities {
    using UnityEngine;

    public class IntVariableGetter : MonoBehaviour {
        public IntVariable variable;

        public UnityEvent_Int OnValueChange;
        public UnityEvent_Int OnGet;

        private void OnEnable () {
            variable.OnValueChanged += ValueChangeHandler;
        }

        private void OnDisable () {
            variable.OnValueChanged -= ValueChangeHandler;
        }

        private void ValueChangeHandler ( int obj ) {
            OnValueChange.Invoke( obj );
        }

        public int Get () {
            OnGet.Invoke( variable.Value );
            return variable.Value;
        }

        public void InvokeGet () {
            OnGet.Invoke( variable.Value );
        }
    }
}