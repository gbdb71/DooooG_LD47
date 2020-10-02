namespace Deirin.Utilities {
    using UltEvents;
    using UnityEngine;

    public class FromIntToFloat : MonoBehaviour {
        public UltEvent<float> OnConversion;

        public void Convert ( int value ) => OnConversion.Invoke( value );
    }
}