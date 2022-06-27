using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace DodoWorkshop.GameKit
{
    /// <summary>
    /// Use this component to bind a class events to a scriptable object.
    /// Using this object prevents hard references between objects.
    /// </summary>
    /// <typeparam name="T">The type of the source</typeparam>
    [DefaultExecutionOrder(DefaultExecutionOrders.EVENT_CONTAINER)]
    public abstract class EventContainer<T> : ScriptableObject
    {
        [Header("Info")]
        [SerializeField]
        [TextArea(3, 10)]
        [Tooltip("The description of the event container, just to remember the goal of this container.")]
        private string description;

        /// <summary>
        /// This event is called when a new source has been bound
        /// </summary>
        public UnityAction<T> OnSourceBound { get; set; }

        /// <summary>
        /// This event is called when a source has been unbound
        /// </summary>
        public UnityAction<T> OnSourceUnbound { get; set; }

        /// <summary>
        /// The description of the <see cref="EventContainer{T}"/>, just to remember the goal of this container.
        /// </summary>
        public string Description => description;


        /// <summary>
        /// Binds a source to this container.
        /// </summary>
        /// <param name="source">The source to bind</param>
        /// <exception cref="Exception">Thrown if the provided source is already bound to this container</exception>
        public void Bind(T source)
        {
            OnBind(source);
            OnSourceBound?.Invoke(source);
        }

        /// <summary>
        /// Unbinds a source from this container.
        /// </summary>
        /// <param name="source">The source to unbind</param>
        /// <exception cref="Exception">Thrown if the provided source is not bound to this container</exception>
        public void Unbind(T source)
        {
            OnUnbind(source);
            OnSourceUnbound.Invoke(source);
        }


        protected abstract void OnBind(T source);

        protected abstract void OnUnbind(T source);
    }
}
