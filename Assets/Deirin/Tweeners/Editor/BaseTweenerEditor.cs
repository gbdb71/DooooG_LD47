namespace Deirin.Tweeners {
    using UnityEngine;
    using UnityEditor;

    [CustomEditor( typeof( BaseTweener ), true )]
    [CanEditMultipleObjects]
    public class BaseTweenerEditor : Editor {
        private BaseTweener baseTweener;

        private SerializedProperty ignoresContainer;
        private SerializedProperty duration;
        private SerializedProperty ignoresTimescale;
        private SerializedProperty delay;
        private SerializedProperty relative;
        private SerializedProperty speedBased;
        private SerializedProperty ease;
        private SerializedProperty loops;
        private SerializedProperty loopType;
        private SerializedProperty useCurve;
        private SerializedProperty tweenCurve;
        private SerializedProperty autoKill;
        private SerializedProperty autoPlay;

        private SerializedProperty OnPlay;
        private SerializedProperty OnRewind;
        private SerializedProperty OnPlayEnd;
        private SerializedProperty OnRewindEnd;
        private SerializedProperty OnKill;

        private void OnEnable () {
            baseTweener = target as BaseTweener;

            ignoresContainer = serializedObject.FindProperty( "ignoresContainer" );
            duration = serializedObject.FindProperty( "duration" );
            ignoresTimescale = serializedObject.FindProperty( "ignoresTimescale" );
            delay = serializedObject.FindProperty( "delay" );
            relative = serializedObject.FindProperty( "relative" );
            speedBased = serializedObject.FindProperty( "speedBased" );
            ease = serializedObject.FindProperty( "ease" );
            loops = serializedObject.FindProperty( "loops" );
            loopType = serializedObject.FindProperty( "loopType" );
            useCurve = serializedObject.FindProperty( "useCurve" );
            tweenCurve = serializedObject.FindProperty( "tweenCurve" );
            autoKill = serializedObject.FindProperty( "autoKill" );
            autoPlay = serializedObject.FindProperty( "autoPlay" );

            OnPlay = serializedObject.FindProperty( "OnPlay" );
            OnRewind = serializedObject.FindProperty( "OnRewind" );
            OnPlayEnd = serializedObject.FindProperty( "OnPlayEnd" );
            OnRewindEnd = serializedObject.FindProperty( "OnRewindEnd" );
            OnKill = serializedObject.FindProperty( "OnKill" );
        }

        public override void OnInspectorGUI () {
            base.OnInspectorGUI();

            //serializedObject.Update();

            //using ( new GUILayout.VerticalScope( EditorStyles.helpBox ) ) {
            //    using ( new GUILayout.HorizontalScope() ) {
            //        GUILayout.Label( "Generics", EditorStyles.boldLabel, GUILayout.MaxWidth( 70 ) );
            //        ignoresContainer.boolValue = EditorGUILayout.Toggle( ignoresContainer.boolValue, GUILayout.MaxWidth( 10 ) );
            //        GUILayout.Label( "Hidden" );
            //    }

            //    using ( new GUILayout.HorizontalScope() ) {
            //        autoKill.boolValue = EditorGUILayout.Toggle( "Auto Kill", autoKill.boolValue, EditorStyles.miniButton );
            //    }

            //    using ( new GUILayout.HorizontalScope() ) {
            //        GUILayout.Label( "  Duration", GUILayout.MaxWidth( 60 ) );
            //        duration.floatValue = EditorGUILayout.FloatField( duration.floatValue, GUILayout.MaxWidth( 40 ) );
            //        GUILayout.Label( "Delay", GUILayout.MaxWidth( 40 ) );
            //        delay.floatValue = EditorGUILayout.FloatField( delay.floatValue, GUILayout.MaxWidth( 40 ) );
            //    }
            //}

            //serializedObject.ApplyModifiedProperties();
        }
    }
}