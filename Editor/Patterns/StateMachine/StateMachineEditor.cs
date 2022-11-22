using UnityEngine;
using UnityEditor;

namespace DodoWorkshop.GameKit.Editor
{
    [CustomEditor(typeof(StateMachine), true)]
    public class StateMachineEditor : UnityEditor.Editor
    {
        private StateMachine stateMachine;

        private void OnEnable()
        {
            stateMachine = (StateMachine)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            if (Application.isPlaying)
            {
                EditorGUILayout.LabelField("State machine details", EditorStyles.boldLabel);

                // Current state line
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Current state", EditorStyles.boldLabel);
                EditorGUILayout.LabelField(stateMachine.CurrentState?.GetType().Name ?? "None");
                EditorGUILayout.EndHorizontal();

                // Started line
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Started", EditorStyles.boldLabel);
                EditorGUILayout.LabelField(stateMachine.IsStopped ? "false" : "true");
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.LabelField("More details during runtime");
            }

            EditorGUILayout.EndVertical();
            
            DrawDefaultInspector();
        }
    }
}
