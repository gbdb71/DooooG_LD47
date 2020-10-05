namespace Deirin.Utilities {
    using UltEvents;
    using UnityEngine;

    public class FromIntToString : MonoBehaviour {
        public UltEvent<string> OnConversion;

        public void Convert ( int value ) => OnConversion.Invoke( value.ToString() );
    }
}