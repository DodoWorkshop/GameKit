using System;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// Put this attribute on a field or a property of type <see cref="IBloc"/> to
    /// make it injectable. To inject blocs in a class call the method <see cref="BlocProvider.InjectBlocs{T}(T)"/>
    /// or add the <see cref="IWithBlocs"/> interface to the calss and call the LoadBlocs() method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class InjectBlocAttribute : Attribute
	{
        /// <summary>
        /// If true, the <see cref="GlobalBlocProvider"/> will be searched
        /// </summary>
        public bool searchGlobal = false;
	}
}
