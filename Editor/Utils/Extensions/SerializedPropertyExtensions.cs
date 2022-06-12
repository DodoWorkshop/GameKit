using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DodoWorkshop.GameKit.Editor
{
	public static class SerializedPropertyExtensions
	{
        /// <summary>
        /// Removes every null referece in the provided serialize array and update its size
        /// </summary>
	    public static void ClearNullReferences(this SerializedProperty property)
        {
            if (!property.isArray)
            {
                throw new Exception($"The provided property is not an array {property.propertyType}");
            }

            List<UnityEngine.Object> cleanValues = new List<UnityEngine.Object>();
            for (int i = 0; i < property.arraySize; i++)
            {
                SerializedProperty element = property.GetArrayElementAtIndex(i);
                if(element != null && element.objectReferenceValue != null)
                {
                    cleanValues.Add(element.objectReferenceValue);
                }
            }

            property.arraySize = cleanValues.Count();

            for (int i = 0; i < property.arraySize; i++)
            {
                SerializedProperty element = property.GetArrayElementAtIndex(i);
                element.objectReferenceValue = cleanValues[i];
            }
        }

        /// <summary>
        /// Gets an enumerable of inner <see cref="SerializedProperty"/> to iterate on
        /// </summary>
        public static IEnumerable<SerializedProperty> GetEnumerable(this SerializedProperty property)
        {
            if (!property.isArray)
            {
                throw new Exception($"The provided property is not an array {property.propertyType}");
            }

            List<SerializedProperty> values = new List<SerializedProperty>();
            for (int i = 0; i < property.arraySize; i++)
            {
                values.Add(property.GetArrayElementAtIndex(i));
            }

            return values.AsEnumerable();
        }
    }
}
