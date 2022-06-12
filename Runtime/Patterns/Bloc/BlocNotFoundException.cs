using UnityEngine;
using System;

namespace DodoWorkshop.GameKit
{
    public class BlocNotFoundException : Exception
    {
        public BlocNotFoundException(GameObject source, Type blocType) 
            : base($"No bloc of type {blocType.Name} found in {source.name}") { }
    }
}
