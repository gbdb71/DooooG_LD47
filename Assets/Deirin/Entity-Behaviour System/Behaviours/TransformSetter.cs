namespace Deirin.EB {
    using UnityEngine;

    public class TransformSetter : BaseBehaviour {
        public Transform target;

        public void SetEulers ( Vector3 eulers ) {
            transform.eulerAngles = eulers;
        }

        public void SetLocalEulers ( Vector3 eulers ) {
            transform.localEulerAngles = eulers;
        }

        public void SetRotation ( Quaternion rotation ) {
            transform.rotation = rotation;
        }

        public void SetLocalRotation ( Quaternion rotation ) {
            transform.localRotation = rotation;
        }

        public void SetPosition ( Vector3 position ) {
            transform.position = position;
        }
    }
}