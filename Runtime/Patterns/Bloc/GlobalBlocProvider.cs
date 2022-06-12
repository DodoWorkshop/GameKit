using UnityEngine;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// Add this component to a GameObject with a <see cref="GameKit.BlocProvider"/> to
    /// make this provider the global one. You can have only have one <see cref="GlobalBlocProvider"/>
    /// in the scene, but it can be accesses from anywhere.
    /// </summary>
    [AddComponentMenu("Dodo Workshop/Blocs/Global Bloc Provider")]
    [RequireComponent(typeof(BlocProvider))]
    public class GlobalBlocProvider : Singleton<GlobalBlocProvider>
	{
        private BlocProvider blocProvider;


        public static BlocProvider BlocProvider => Instance.blocProvider;
        

        protected override void Awake()
        {
            base.Awake();

            blocProvider = GetComponent<BlocProvider>();
        }
    }
}
