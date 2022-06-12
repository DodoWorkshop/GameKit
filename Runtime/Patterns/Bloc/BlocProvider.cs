using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;


namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// You can provide <see cref="IBloc"/>s to other objects by adding this
    /// component to your scene.
    /// You can either get a  bloc with the method <see cref="GetBloc{T}"/> or inject 
    /// them in an object with its blocs marked with the <see cref="InjectBlocAttribute"/>
    /// by using the method <see cref="InjectBlocs{T}(T)"/>.
    /// You can also add the <see cref="IWithBlocs"/> interface and and call the
    /// LoadBlocs() method.
    /// </summary>
    [AddComponentMenu("Dodo Workshop/Blocs/Bloc Provider")]
    [DefaultExecutionOrder(DEFAULT_EXECUTION_ORDER)]
    public class BlocProvider : MonoBehaviour
    {
        public const int DEFAULT_EXECUTION_ORDER = -90;
        

        [Header("Auto Bloc Registering")]
        [SerializeField]
        [Tooltip("If true, children will be scanned for blocs")]
        private bool scanChildren;

        [SerializeField]
        [Tooltip("If true, the desired bloc will be searched in the Global Bloc Provider if not found in this provider")]
        private bool callGlobalForMissingBlocs;
            

        private List<IBloc> blocs = new List<IBloc>();


        public List<IBloc> Blocs => blocs;


        /// <summary>
        /// Gets a bloc of type <typeparamref name="T"/> and returns null
        /// if there is not bloc delared with the provided type.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="IBloc"/></typeparam>
        /// <returns>The requested bloc</returns>
        public T GetBloc<T>() where T : IBloc
        {
            return blocs
                .Where(bloc => bloc.GetType().Equals(typeof(T)))
                .Cast<T>()
                .FirstOrDefault();
        }

        /// <summary>
        /// True if a bloc of type <typeparamref name="T"/> has been registered.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="IBloc"/></typeparam>
        public bool IsBlocRegistered<T>() where T : IBloc
        {
            return IsBlocRegistered(typeof(T));
        }

        /// <summary>
        /// True if a bloc of type <typeparamref name="T"/> has been registered.
        /// </summary>
        /// <param name="type">The type of the <see cref="IBloc"/></param>
        /// <returns></returns>
        public bool IsBlocRegistered(Type type)
        {
            return blocs.Any(entry => entry.GetType().Equals(type));
        }

        /// <summary>
        /// Registers a bloc. A registered bloc can be retrieved from its type
        /// by using the method <see cref="GetBloc{T}"/>. You can't register
        /// two blocs with the same type (Bloc provider should be used for global
        /// states)
        /// </summary>
        /// <param name="bloc">The bloc to register</param>
        public void RegisterBloc(IBloc bloc)
        {
            if (IsBlocRegistered(bloc.GetType()))
            {
                throw new Exception($"Failed to register bloc: There is alreay an entry with the type {bloc.GetType().Name}");
            }

            blocs.Add(bloc);
        }

        /// <summary>
        /// Registers multiple <see cref="IBloc"/>s by using the <see cref="RegisterBloc(IBloc)"/> method.
        /// </summary>
        /// <param name="blocs">Blocs to register</param>
        public void RegisterBlocs(IEnumerable<IBloc> blocs)
        {
            foreach (IBloc bloc in blocs)
            {
                RegisterBloc(bloc);
            }
        }

        /// <summary>
        /// Removes a registered bloc.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="IBloc"/></typeparam>
        public void UnregisterBloc<T>() where T : IBloc
        {
            IBloc bloc = GetBloc<T>();

            if (bloc == null)
            {
                throw new Exception($"Failed to unregister bloc: There is no entry with the type {typeof(T).Name}");
            }

            blocs.Remove(bloc);
        }

        /// <summary>
        /// Removes every registered blocs
        /// </summary>
        public void Clear()
        {
            blocs = new List<IBloc>();
        }

        /// <summary>
        /// Injects corresponding <see cref="IBloc"/> in every field or property
        /// marked with the <see cref="InjectBlocAttribute"/> in the object of
        /// type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the target object</typeparam>
        /// <param name="target">The target object</param>
        public void InjectBlocs<T>(T target)
        {
            // Get the type of the target object
            Type targetType = typeof(T);

            InjectBlocs(target, targetType);
        }

        /// <summary>
        /// Injects corresponding <see cref="IBloc"/> in every field or property
        /// marked with the <see cref="InjectBlocAttribute"/> in the provided object.
        /// </summary>
        /// <param name="target">The target object</param>
        public void InjectBlocs(object target)
        {
            // Get the type of the target object
            Type targetType = target.GetType();

            InjectBlocs(target, targetType);
        }

        /// <summary>
        /// Injects corresponding <see cref="IBloc"/> in every field or property
        /// marked with the <see cref="InjectBlocAttribute"/> in the provided object.
        /// </summary>
        /// <param name="target">The target object</param>
        /// <param name="targetType">The desired type of the target object (the type that holds the <see cref="InjectBlocAttribute"/>s)</param>
        public void InjectBlocs(object target, Type targetType)
        {
            // Retrieve member infos
            List<MemberInfo> memberInfos = new List<MemberInfo>();
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            memberInfos.AddRange(targetType.GetProperties(bindingFlags));
            memberInfos.AddRange(targetType.GetFields(bindingFlags));

            foreach (MemberInfo memberInfo in memberInfos)
            {
                InjectBlocAttribute memberAttribute = memberInfo.GetCustomAttribute<InjectBlocAttribute>();
                if (memberAttribute != null)
                {
                    try
                    {
                        InjectBloc(target, memberInfo);
                    }
                    catch (BlocNotFoundException e)
                    {
                        if (GlobalBlocProvider.HasInstance && (callGlobalForMissingBlocs || memberAttribute.searchGlobal))
                        {
                            GlobalBlocProvider.BlocProvider.InjectBloc(target, memberInfo);
                        }
                        else
                        {
                            Debug.LogError($"Failed to inject the bloc in the field {memberInfo.Name}: {e.Message}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Inject a bloc in the provided member
        /// </summary>
        /// <typeparam name="T">The type of the target</typeparam>
        /// <param name="target">The target object</param>
        /// <param name="memberInfo">The member to inject in</param>
        /// <exception cref="BlocNotFoundException">Raises this exception if no matching bloc is found in this provider</exception>
        public void InjectBloc<T>(T target, MemberInfo memberInfo)
        {
            Type targetType = typeof(T);

            // Build method
            MethodInfo method = typeof(BlocProvider).GetMethod("GetBloc");
            MethodInfo genericMethod;
            object result;

            switch (memberInfo)
            {
                case PropertyInfo propertyInfo:
                    if (!typeof(IBloc).IsAssignableFrom(propertyInfo.PropertyType))
                    {
                        throw new Exception(
                            $"Target of type {targetType.Name} used the InjectBloc attribute on the " +
                            $"property {propertyInfo.Name} of type {propertyInfo.PropertyType} that is" +
                            $" not an IBloc: that is not allowed"
                        );
                    }

                    genericMethod = method.MakeGenericMethod(propertyInfo.PropertyType);
                    result = genericMethod.Invoke(this, null);
                    if(result == null)
                    {
                        throw new BlocNotFoundException(gameObject, propertyInfo.PropertyType);
                    }

                    propertyInfo.SetValue(target, result);
                    break;  
                case FieldInfo fieldInfo:
                    if (!typeof(IBloc).IsAssignableFrom(fieldInfo.FieldType))
                    {
                        throw new Exception(
                            $"Target of type {targetType.Name} used the InjectBloc attribute on the " +
                            $"field {fieldInfo.Name} of type {fieldInfo.FieldType} that is not an " +
                            $"IBloc: that is not allowed");
                    }

                    genericMethod = method.MakeGenericMethod(fieldInfo.FieldType);
                    result = genericMethod.Invoke(this, null);
                    if (result == null)
                    {
                        throw new BlocNotFoundException(gameObject, fieldInfo.FieldType);
                    }

                    fieldInfo.SetValue(target, result);
                    break;
                default:
                    throw new Exception($"Unrecognized member {memberInfo.Name} of type {memberInfo.MemberType}");
            }
        }

        private void Initialize()
        {   
            if (scanChildren)
            {
                RegisterBlocs(GetComponentsInChildren<IBloc>());
            }
            else
            {
                RegisterBlocs(GetComponents<IBloc>());
            }
        }

        protected virtual void Awake()
        {
            Initialize();
        }
    }
}
