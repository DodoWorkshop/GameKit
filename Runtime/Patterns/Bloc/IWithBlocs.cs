using System;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// Add this interace to a class that has some <see cref="InjectBlocAttribute"/>s. You
    /// can now use the this.LoadBlocs() method to inject blocs.
    /// </summary>
    public interface IWithBlocs : IComponent
    {
    }

    public static class IWithBlocsExtensions
    {
        public static void LoadBlocs(this IWithBlocs target)
        {
            BlocProvider blocProvider = target.GetNearestProvider();
            if (blocProvider != null)
            {
                blocProvider.InjectBlocs(target, target.GetType());
            }
            else
            {
                throw new Exception($"No provider has been found from the object {target.gameObject.name}");
            }
        }

        public static BlocProvider GetNearestProvider(this IWithBlocs target)
        {
            BlocProvider provider = target.gameObject.GetComponentInParent<BlocProvider>();
            if (provider != null)
            {
                return provider;
            }
            else if(GlobalBlocProvider.HasInstance)
            {
                return GlobalBlocProvider.BlocProvider;
            }

            return null;
        }
    }
}
