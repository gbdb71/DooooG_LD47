namespace Deirin.Tweeners {
    using UnityEngine;
    using DG.Tweening;
    using UltEvents;

    public abstract class BaseTweener : MonoBehaviour {
        public bool ignoresContainer;
        [Min(0)] public float duration;
        public bool ignoresTimescale;
        [Min(0)] public float delay;
        public bool relative;
        public bool speedBased;
        public Ease ease;
        [Min(-1)] public int loops;
        public LoopType loopType;
        public bool useCurve = false;
        public AnimationCurve tweenCurve;
        public bool autoKill = false;
        public bool autoPlay = false;

        [Header("Events")]
        public UltEvent OnPlay;
        public UltEvent OnRewind;
        public UltEvent OnPlayEnd;
        public UltEvent OnRewindEnd;
        public UltEvent OnKill;

        protected Tween tween;

        private void Awake () => TweenSetup();

        protected abstract void AssignTween ();

        private void TweenSetup () {
            AssignTween();

            if ( tween == null )
                return;

            tween.SetUpdate( ignoresTimescale );
            tween.SetDelay( delay );

            if ( !useCurve )
                tween.SetEase( ease );
            else
                tween.SetEase( tweenCurve );

            tween.SetLoops( loops, loopType );
            tween.SetAutoKill( autoKill );
            tween.SetRelative( relative );
            tween.SetSpeedBased( speedBased );

            tween.onComplete += OnComplete;
            tween.onRewind += OnRewindEnd.Invoke;

            if ( autoPlay )
                Play();
        }

        #region API
        public void Play () {
            if ( tween.IsActive() == false )
                AssignTween();

            OnPlay.Invoke();
            tween.PlayForward();
        }

        public void Rewind () {
            if ( tween.IsActive() == false )
                AssignTween();

            OnRewind.Invoke();
            tween.PlayBackwards();
        }

        public void RewindFromEnd () {
            if ( tween.IsActive() == false )
                AssignTween();

            tween.Goto( duration );
            Rewind();
        }

        public void ResetTween () {
            tween.Rewind();
        }

        public void Kill () {
            OnKill.Invoke();
            tween.Kill();
        }
        #endregion

        private void OnComplete () {
            OnPlayEnd.Invoke();
        }
    }
}