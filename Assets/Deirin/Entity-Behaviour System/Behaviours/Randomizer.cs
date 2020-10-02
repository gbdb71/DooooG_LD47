namespace Deirin.EB {
    using UltEvents;
    using UnityEngine;

    public class Randomizer : BaseBehaviour {
        public float floatMin, floatMax;
        public int intMin, intMax;

        public UltEvent<float> RandomFloat;
        public UltEvent<int> RandomInt;

        public void GetRandomFloat () {
            RandomFloat.Invoke( Random.Range( floatMin, floatMax ) );
        }
        
        public void GetRandomInt () {
            RandomInt.Invoke( Random.Range( intMin, intMax ) );
        }
    }
}