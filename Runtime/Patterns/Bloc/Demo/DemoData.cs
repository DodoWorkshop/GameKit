using UnityEngine;
using System;

namespace DodoWorkshop.GameKit.Demos.Bloc
{
    [Serializable]
	public struct DemoData
	{
        [SerializeField]
        private int count;

        [SerializeField]
        private string name;

        public int Count { get => count; set => count = value; }

        public string Name { get => name; set => name = value; }
	}
}
