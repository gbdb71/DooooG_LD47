namespace Deirin.EB {
    using UltEvents;
    using UnityEngine;

    [RequireComponent( typeof( Collider ) )]
    public class DamageDealer : BaseBehaviour {
        [Header("Params")]
        [SerializeField] private float damage;

        [Header("Events")]
        public UltEvent<BaseEntity> OnDamageDealt;

        private DamageReceiver damageReceiver;
        private float startDamage;

        protected override void CustomSetup () {
            startDamage = damage;
        }

        public float Damage => damage;

        public void DealDamage () {
            damageReceiver?.Damage( damage );
        }

        public void DealDamage ( float value ) {
            damageReceiver?.Damage( value );
        }

        public void SetDamage ( float value ) {
            damage = value;
        }

        public void ResetDamage () {
            damage = startDamage;
        }

        private void OnTriggerEnter ( Collider other ) {
            damageReceiver = other.GetComponentInChildren<DamageReceiver>();
            if ( damageReceiver ) {
                DealDamage();
                OnDamageDealt.Invoke( damageReceiver.Entity );
            }
        }
    }
}