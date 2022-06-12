using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;
using System.Linq;

namespace DodoWorkshop.GameKit.Editor
{
	public static class EditorGUIUtils
	{
	    public static void WrapSerializedPropertyField(SerializedProperty property, GUIContent label = null, GUILayoutOption[] options = null)
        {

            if (property.isArray && property.propertyType != SerializedPropertyType.String)
            {
                if (label != null)
                {
                    GUILayout.Label(label);
                }

                for (int i = 0; i < property.arraySize; i++)
                {
                    SerializedProperty child = property.GetArrayElementAtIndex(i);

                    EditorGUILayout.PropertyField(child, new GUIContent(child.displayName), options);
                }
            }
            else if (property.hasVisibleChildren) {

                if (label != null) {
                    GUILayout.Label(label);
                }

                IEnumerator enumerator = property.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    EditorGUILayout.PropertyField((SerializedProperty)enumerator.Current, new GUIContent(property.displayName), options);
                }
            }
            else
            {
                EditorGUILayout.PropertyField(property, label ?? GUIContent.none, options);
            }
        }

        public static SerializedProperty[] GetChildrenProperties(SerializedObject serializedObject, Object target)
        {
            return target.GetType()
                .GetFields(BindingFlags.DeclaredOnly
                | BindingFlags.Instance 
                | BindingFlags.Public
                | BindingFlags.NonPublic)
                .Where(fieldInfo => fieldInfo.IsPublic || fieldInfo.GetCustomAttribute(typeof(SerializeField)) != null)
                .Select(fieldInfo => serializedObject.FindProperty(fieldInfo.Name))
                .ToArray();
        }
    }
}
