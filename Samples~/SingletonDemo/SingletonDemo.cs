using UnityEngine;

namespace DodoWorkshop.GameKit
{
	public class SingletonDemo : Singleton<SingletonDemo>
	{
        [Header("Test data")]
        [SerializeField]
        private string message;

        public static string Message => Instance.message;
    }
}
