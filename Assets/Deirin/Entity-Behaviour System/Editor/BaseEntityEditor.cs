namespace Deirin.EB {
    using UnityEngine;
    using UnityEditor;

    [CustomEditor( typeof( BaseEntity ), true )]
    public class BaseEntityEditor : Editor {
        private BaseEntity be;

        private void OnEnable () {
            be = target as BaseEntity;
        }

        public override void OnInspectorGUI () {
            base.OnInspectorGUI();

            serializedObject.Update();

            int count = be.Behaviours.Length;

            if ( count == 0 )
                EditorGUILayout.HelpBox( "No Behaviours found!", MessageType.Warning );
            else
                EditorGUILayout.HelpBox( count + " Behaviours found!", MessageType.Info );

            if ( GUILayout.Button( "Fetch Behaviours" ) ) {
                be.FetchBehaviours();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}