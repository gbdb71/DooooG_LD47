namespace Deirin.EB {
    using UltEvents;
    using UnityEngine;

    public class Vector3Randomizer : BaseBehaviour {
        public float xMin,xMax;
        public float yMin,yMax;
        public float zMin,zMax;

        public UltEvent<Vector3> RandomVector3;

        public void GetRandomVector3 () {
            RandomVector3.Invoke( new Vector3( Random.Range( xMin, xMax ), Random.Range( yMin, yMax ), Random.Range( zMin, zMax ) ) );
        }
    }
}