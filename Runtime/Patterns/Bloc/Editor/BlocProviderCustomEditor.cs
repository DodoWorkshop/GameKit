using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace DodoWorkshop.GameKit.Editors
{
    [CustomEditor(typeof(BlocProvider))]
    public class BlocProviderCustomEditor : Editor
    {
        private BlocProvider blocProvider;
        private SerializedProperty scanChildrenProperty;

        private void OnEnable()
        {
            blocProvider = target as BlocProvider;
            scanChildrenProperty = serializedObject.FindProperty("scanChildren");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            if (Application.isPlaying)
            {
                List<IBloc> blocs = blocProvider.Blocs;
                EditorGUILayout.LabelField($"Registered blocs ({blocs.Count})", EditorStyles.boldLabel);
                foreach (IBloc bloc in blocs)
                {
                    EditorGUILayout.LabelField($"- { bloc.GetType().Name } ({bloc.gameObject.name})");
                }
            }
            else
            {
                IBloc[] blocs;
                if (scanChildrenProperty.boolValue)
                {
                    blocs = blocProvider.GetComponentsInChildren<IBloc>();
                }
                else {
                    blocs = blocProvider.GetComponents<IBloc>();
                }

                EditorGUILayout.LabelField($"Registerable blocs ({blocs.Length})", EditorStyles.boldLabel);
                foreach (IBloc bloc in blocs)
                {
                    EditorGUILayout.LabelField($"- { bloc.GetType().Name } ({bloc.gameObject.name})");
                }

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("More details available during runtime");
            }


            EditorGUILayout.EndVertical();

            DrawDefaultInspector();
        }
    }
}
