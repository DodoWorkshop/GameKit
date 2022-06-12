using UnityEngine;
using System;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// The base class for every singletons in the game.
    /// </summary>
    public abstract class Singleton : MonoBehaviour
    {
        public const int DEFAULT_EXECUTION_ORDER = -100;
    }

    /// <summary>
    /// The base class for every singletons in the game.
    /// </summary>
    /// <typeparam name="T">This class child's type (ex: class MyClass : Singleton&lt;MyClass&gt;)</typeparam>
    [DefaultExecutionOrder(Singleton.DEFAULT_EXECUTION_ORDER)]
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;


        public static T Instance {
            get {
                if(!HasInstance)
                {
                    throw new Exception($"Tried to access an instance of {Type.Name}, but none has been found: No instance in the scene or not instanciated yet");
                }
                return instance;
            }
            private set {
                instance = value;
            }
        }

        /// <summary>
        /// If true, the singleton won't be destroyed when another scene is loaded.
        /// </summary>
        protected virtual SingletonConflictRule ConflictRule => SingletonConflictRule.THROW_EXCEPTION;

        /// <summary>
        /// Sets the way singleton conflicts has to be handled.
        /// </summary>
        protected virtual bool IsNotDestroyedOnLoad => false;

        public static Type Type => typeof(T);

        public static bool HasInstance => instance != null;


        protected virtual void Awake()
        {
            InitSingleton();
        }

        private void InitSingleton()
        {
            T previous = instance;

            if (previous != null)
            {
                switch (ConflictRule)
                {
                    case SingletonConflictRule.THROW_EXCEPTION:
                        throw new SingletonConflictException(typeof(T), previous.gameObject);
                    case SingletonConflictRule.KEEP_OLD:
                        Debug.LogWarning($"Singleton conflict detected for {Type.Name}. The old one has been kept, but try to avoid doing this.");
                        Destroy(this);
                        return;
                    case SingletonConflictRule.KEEP_NEW:
                        Debug.LogWarning($"Singleton conflict detected for {Type.Name}. The new one has been kept, but try to avoid doing this.");
                        Destroy(previous);
                        break;
                }
            }

            Instance = GetComponent<T>();

            if (IsNotDestroyedOnLoad)
            {
                // Put this object at the root of the scene
                gameObject.transform.parent = null;
                DontDestroyOnLoad(this);
            }
        } 
    }
}
