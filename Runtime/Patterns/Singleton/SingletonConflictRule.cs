namespace DodoWorkshop.GameKit
{
	public enum SingletonConflictRule
	{
        /// <summary>
        /// If another <see cref="Singleton{T}"/> exists when instanciating a new one, a <see cref="SingletonConflictException"/> is thrown.
        /// </summary>
		THROW_EXCEPTION,

        /// <summary>
        /// If another  <see cref="Singleton{T}"/> exists when instanciating a new one, the new one is destroyed
        /// </summary>
        KEEP_OLD,

        /// <summary>
        /// If another  <see cref="Singleton{T}"/> exists when instanciating a new one, the old one is destroyed
        /// </summary>
        KEEP_NEW
    }
}
