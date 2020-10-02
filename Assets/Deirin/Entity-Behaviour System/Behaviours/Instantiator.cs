namespace Deirin.EB {
    using UltEvents;
    using UnityEngine;

    public class Instantiator : BaseBehaviour {
        [Header("Prefab")]
        public GameObject objPrefab;

        [Header("Params")]
        public Transform referenceTransform;
        public bool setAsParent;
        public bool usePosition;
        public Vector3 position;
        public bool useRotation;
        public Vector3 rotationEulers;

        [Header("Events")]
        public UltEvent OnInstantiate;

        public void InstantiateObject () {
            GameObject obj = Instantiate( objPrefab, usePosition ? referenceTransform.position : position, useRotation ? referenceTransform.rotation : Quaternion.Euler( rotationEulers) );
            if ( setAsParent && referenceTransform != null )
                obj.transform.SetParent( referenceTransform );
            OnInstantiate.Invoke();
        }
    }
}