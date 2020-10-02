namespace Deirin.EB {
    using UnityEngine;
    using UltEvents;

    public class DamageReceiver : BaseBehaviour {
        [Header("Params")]
        public bool useGlobalVars;

        public FloatVariable MaxLife;
        public FloatVariable CurrentLife;

        public float maxLife;

        public bool maxLifeOnSetup = true;

        [Header("Events")]
        public UltEvent<float> OnLifeChanged;
        public UltEvent<float> OnLifeChangedPercent;
        public UltEvent OnLifeDepeleated;

        private float currentLife;

        protected override void CustomSetup () {
            if ( maxLifeOnSetup )
                if ( useGlobalVars ) {
                    CurrentLife.Value = MaxLife.Value;
                }
                else {
                    currentLife = maxLife;
                }
        }

        public void Damage ( float value ) {
            if ( useGlobalVars ) {
                CurrentLife.Value -= value;
                OnLifeChanged.Invoke( CurrentLife.Value );
                OnLifeChangedPercent.Invoke( CurrentLife.Value / MaxLife.Value );
                if ( CurrentLife.Value <= 0 )
                    OnLifeDepeleated.Invoke();
            }
            else {
                currentLife -= value;
                OnLifeChanged.Invoke( currentLife );
                OnLifeChangedPercent.Invoke( currentLife / maxLife );
                if ( currentLife <= 0 )
                    OnLifeDepeleated.Invoke();
            }
        }
    }
}