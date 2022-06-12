using UnityEngine;
using UnityEditor;

namespace DodoWorkshop.GameKit.Editors
{
    [CustomEditor(typeof(Bloc<,>), true)]
	public class BlocCustomEditor : Editor
	{
        private IBloc bloc;
        private SerializedProperty initializeStateProperty;
        private SerializedProperty initialStateProperty;
        private SerializedProperty stateProperty;
        private SerializedProperty[] childrenProperties;

        private void OnEnable()
        {
            bloc = target as IBloc;

            initializeStateProperty = serializedObject.FindProperty("initializeState");
            initialStateProperty = serializedObject.FindProperty("initialState");
            stateProperty = serializedObject.FindProperty("state");

            // Get children properties
            childrenProperties = EditorGUIUtils.GetChildrenProperties(serializedObject, target);
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.LabelField(new GUIContent("State"), EditorStyles.boldLabel);

            if(Application.isPlaying)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                if(stateProperty != null)
                {
                    GUI.enabled = false;
                    EditorGUILayout.PropertyField(stateProperty, new GUIContent("Current State Value"), true);
                    GUI.enabled = true;
                }
                else
                {
                    EditorGUILayout.LabelField(new GUIContent("Can't visualize not serializable states"));
                }

                if (GUILayout.Button("Reset State"))
                {
                    bloc.ResetState();
                }

                EditorGUILayout.EndVertical();
            }

            if (initialStateProperty != null) {
                initializeStateProperty.boolValue = EditorGUILayout.ToggleLeft(initializeStateProperty.displayName, initializeStateProperty.boolValue);

                GUI.enabled = initializeStateProperty.boolValue;
                EditorGUILayout.PropertyField(initialStateProperty, true);
                GUI.enabled = true;
            }
            else
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField(new GUIContent("Can't initialize not serializable states"));
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndVertical();

            // Draw children fields
            foreach (SerializedProperty childrenProperty in childrenProperties)
            {
                EditorGUILayout.PropertyField(childrenProperty);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
