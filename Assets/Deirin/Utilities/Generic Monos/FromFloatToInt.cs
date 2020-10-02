namespace Deirin.Utilities {
    using UltEvents;
    using UnityEngine;

    public class FromFloatToInt : MonoBehaviour {
        public UltEvent<int> OnConversion;

        public void Convert ( float value ) => OnConversion.Invoke( ( int ) value );
    }
}