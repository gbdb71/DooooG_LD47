namespace Deirin.Tweeners {
    using UnityEngine;
    using System.Collections.Generic;
    using UltEvents;
    using NaughtyAttributes;

    public class TweenersContainer : MonoBehaviour {
        public bool fetchOnAwake = true;
        [ReorderableList]
        public List<BaseTweener> tweeners = new List<BaseTweener>();

        [Header("Events")]
        public UltEvent OnPlay;
        public UltEvent OnAllPlayEnd;
        public UltEvent OnRewind;
        public UltEvent OnAllRewindEnd;

        private int playEndCount, rewindEndCount;

        private void Awake () {
            if ( fetchOnAwake )
                FetchTweeners();
        }

        private void OnEnable () {
            foreach ( var tweener in tweeners ) {
                tweener.OnPlayEnd.DynamicCalls += TweenerPlayEndHandler;
                tweener.OnRewindEnd.DynamicCalls += TweenerRewindEndHandler;
            }
        }

        private void OnDisable () {
            foreach ( var tweener in tweeners ) {
                tweener.OnPlayEnd.DynamicCalls -= TweenerPlayEndHandler;
                tweener.OnRewindEnd.DynamicCalls -= TweenerRewindEndHandler;
            }
        }

        #region API
        public void FetchTweeners () {
            tweeners.Clear();
            foreach ( var item in GetComponentsInChildren<BaseTweener>() ) {
                if ( item.ignoresContainer )
                    continue;
                tweeners.Add( item );
            }
        }

        public void Play () {
            foreach ( var tweener in tweeners )
                tweener.Play();
            OnPlay.Invoke();
        }

        public void Rewind () {
            foreach ( var tweener in tweeners )
                tweener.Rewind();
            OnRewind.Invoke();
        }

        public void ResetTweens () {
            foreach ( var tweener in tweeners )
                tweener.ResetTween();
        }

        public void Stop () {
            for ( int i = 0; i < tweeners.Count; i++ ) {
                tweeners[i].Kill();
            }
        }
        #endregion

        private void TweenerPlayEndHandler () {
            playEndCount++;
            if ( playEndCount == tweeners.Count ) {
                OnAllPlayEnd.Invoke();
            }
        }

        private void TweenerRewindEndHandler () {
            rewindEndCount++;
            if ( rewindEndCount == tweeners.Count ) {
                OnAllRewindEnd.Invoke();
            }
        }
    }
}