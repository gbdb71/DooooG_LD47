namespace Deirin.EB {
    using UnityEngine;
    using UltEvents;

    public class OverlapDamageDealer : BaseBehaviour {
        public enum GetComponentMode { self, parent, children }

        [Header("Parameters")]
        [SerializeField] private float damage;
        public float radius;
        public GetComponentMode getComponentMode;

        [Header("Events")]
        public UltEvent<BaseEntity> OnEntityHit;

        private Collider[] hitObjs;
        private BaseEntity tempEntity;
        private DamageReceiver tempDamageReceiver;
        private float startDamage;

        protected override void CustomSetup () {
            startDamage = damage;
        }

        public float Damage => damage;

        public void DealDamage () {
            tempDamageReceiver?.Damage( damage );
        }

        public void DealDamage ( float value ) {
            tempDamageReceiver?.Damage( value );
        }

        public void SetDamage ( float value ) {
            damage = value;
        }

        public void ResetDamage () {
            damage = startDamage;
        }

        public void UpdateOverlap () {
            hitObjs = Physics.OverlapSphere( transform.position, radius );

            CheckObjects();
        }

        private void CheckObjects () {
            switch ( getComponentMode ) {
                case GetComponentMode.self:
                for ( int i = 0; i < hitObjs.Length; i++ ) {
                    if ( hitObjs[i].TryGetComponent( out tempEntity ) ) {
                        OnEntityHit.Invoke( tempEntity );
                        if ( tempEntity.TryGetBehaviour( out tempDamageReceiver ) ) {
                            DealDamage();
                        }
                    }
                }
                break;
                case GetComponentMode.parent:
                for ( int i = 0; i < hitObjs.Length; i++ ) {
                    tempEntity = hitObjs[i].GetComponentInParent<BaseEntity>();
                    if ( tempEntity ) {
                        OnEntityHit.Invoke( tempEntity );
                        if ( tempEntity.TryGetBehaviour( out tempDamageReceiver ) ) {
                            DealDamage();
                        }
                    }
                }
                break;
                case GetComponentMode.children:
                for ( int i = 0; i < hitObjs.Length; i++ ) {
                    tempEntity = hitObjs[i].GetComponentInChildren<BaseEntity>();
                    if ( tempEntity ) {
                        OnEntityHit.Invoke( tempEntity );
                        if ( tempEntity.TryGetBehaviour( out tempDamageReceiver ) ) {
                            DealDamage();
                        }
                    }
                }
                break;
            }
        }
    }
}