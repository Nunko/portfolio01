using UnityEngine;

namespace Fruit.ServiceLocator
{
    public class ClientServiceLocator : MonoBehaviour
    {
        void Awake() 
        {
            RegisterServices();
        }

        private void RegisterServices() 
        {
            ILoggerService logger = new Logger();
            ServiceLocator.RegisterService(logger);
        }
    }
}

