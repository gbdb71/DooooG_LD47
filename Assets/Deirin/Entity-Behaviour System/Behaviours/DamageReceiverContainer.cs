namespace Deirin.EB {
    using UnityEngine;
    using UltEvents;

    public class DamageReceiverContainer : BaseBehaviour {
        [Header("Parameters")]
        public DamageReceiver damageReceiver;
        [SerializeField] private float defaultDamage;

        [Header("Events")]
        public UltEvent OnDamageDealerSet;
        public UltEvent<BaseEntity> OnDamageDealt;

        private float startDamage;

        protected override void CustomSetup () {
            startDamage = defaultDamage;
        }

        public float Damage => defaultDamage;

        public void DealDamage ( float damage ) {
            if ( damageReceiver == null )
                return;
            OnDamageDealt.Invoke( damageReceiver.Entity );
            damageReceiver.Damage( damage );
        }

        public void DealDamage () {
            if ( damageReceiver == null )
                return;
            OnDamageDealt.Invoke( damageReceiver.Entity );
            damageReceiver.Damage( defaultDamage );
        }

        public void SetDamage ( float value ) {
            defaultDamage = value;
        }

        public void ResetDamage () {
            defaultDamage = startDamage;
        }

        #region API
        public void GetDamageDealer ( BaseEntity entity ) {
            if ( entity.TryGetBehaviour( out damageReceiver ) ) {
                OnDamageDealerSet.Invoke();
            }
        }
        #endregion
    }
}