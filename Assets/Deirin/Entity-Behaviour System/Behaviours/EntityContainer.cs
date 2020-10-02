namespace SweetRage {
    using UnityEngine;
    using Deirin.EB;
    using UltEvents;

    public class EntityContainer : BaseBehaviour {
        [Header("Parameters")]
        public BaseEntity entity;

        [Header("Events")]
        public UltEvent<BaseEntity> OnEntitySet;

        #region API
        public void SetEntity ( BaseEntity entity ) {
            if ( entity == null )
                return;

            this.entity = entity;
            OnEntitySet.Invoke( entity );
        }

        public void SendEntityToContainer ( BaseEntity targetEntity ) {
            EntityContainer ec;
            if ( targetEntity.TryGetBehaviour( out ec ) ) {
                ec.SetEntity( entity );
            }
        }

        public void SendEntityToContainer ( BaseEntity targetEntity, BaseEntity entityToSend ) {
            SetEntity( entityToSend );
            SendEntityToContainer( targetEntity );
        }
        #endregion
    }
}