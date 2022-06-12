using UnityEngine;

namespace DodoWorkshop.GameKit
{
    public interface IComponent
    {
        GameObject gameObject { get; }

        Transform transform { get; }
    }

    public static class IObjectExtensions
    {
        public static Object AsObject(this IComponent obj)
        {
            return obj as Object;
        }

        public static Component AsComponent(this IComponent obj)
        {
            return obj as Component;
        }
    }
}
