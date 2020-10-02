namespace Deirin.EB {
    using UnityEngine;

    public class Mover : BaseBehaviour {
        public Transform target;

        [Header("Params")]
        public float speed;
        public Vector3 orientation;
        public Space space;

        public override void OnAwake () {
            if ( target == null )
                target = transform;
        }

        public override void OnUpdate () {
            target.Translate( orientation * speed * Time.deltaTime, space );
        }

        public void SetSpeed ( float value ) {
            speed = value;
        }
    }
}