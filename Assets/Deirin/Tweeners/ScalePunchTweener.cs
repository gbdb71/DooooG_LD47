namespace Deirin.Tweeners {
    using UnityEngine;
    using DG.Tweening;

    public class ScalePunchTweener : BaseTweener {
        [Header("Specific Params")]
        public Transform target;
        public Vector3 scalePunch;
        public int vibrato = 10;
        [Range(0,1)] public float elasticity = 1;

        protected override void AssignTween () {
            tween = target.DOPunchScale( scalePunch, duration, vibrato, elasticity );
        }
    }
}