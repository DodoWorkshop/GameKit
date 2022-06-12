using UnityEngine;
using UnityEngine.UI;

namespace DodoWorkshop.GameKit
{
	public class SingletonConsumerDemo : MonoBehaviour
	{
        [SerializeField]
        private Text resultDisplayer;

        private void Start()
        {
            resultDisplayer.text = "Trying to use singletons:";

            try
            {
                string message1 = SingletonDemo.Message;
                resultDisplayer.text += "\nSingleton responsed with: " + message1;
            }
            catch
            {
                resultDisplayer.text += "\nNo response from singleton";
            }
        }
    }
}
