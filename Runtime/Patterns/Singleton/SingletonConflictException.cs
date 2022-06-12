using System;
using UnityEngine;

namespace DodoWorkshop.GameKit
{
	public class SingletonConflictException : Exception 
    {
        public SingletonConflictException(Type singletonType, GameObject foundInScene) 
            : this(singletonType.Name, foundInScene.name){ }

        public SingletonConflictException(string singletonTypeName, string foundInSceneName)
            : base($"Trying to instanciate {singletonTypeName} but there is already one instance of this singleton in the scene attached to {foundInSceneName}") { }
    }
}
