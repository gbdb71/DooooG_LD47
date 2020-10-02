namespace Deirin.EB {
    using UltEvents;

    public class OnEnableBehaviour : BaseBehaviour {
        public UltEvent onEnable;

        public override void OnOnEnable () {
            onEnable.Invoke();
        }
    }
}