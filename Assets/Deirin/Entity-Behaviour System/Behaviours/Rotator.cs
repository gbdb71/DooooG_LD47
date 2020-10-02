namespace Deirin.EB {
    using UnityEngine;

    public class Rotator : BaseBehaviour {
        [Header("Params")]
        public bool rotateOnAwake;
        public float speed;
        public Vector3 eulers;
        public Space space;

        private bool active;

        public override void OnAwake () {
            Active( rotateOnAwake );
        }

        public override void OnUpdate () {
            if ( active == false )
                return;
            transform.Rotate( eulers * speed * Time.deltaTime, space );
        }

        public void Active ( bool value ) {
            active = value;
        }

        public void SetEulers ( Vector3 eulers ) {
            this.eulers = eulers;
        }

        public void SetY ( float Y)
        { 
            this.eulers = new Vector3(0, Y, 0);
        }
            

        public void SetSpeed ( float speed ) {
            this.speed = speed;
        }
    }
}