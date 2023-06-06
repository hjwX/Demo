using UnityEngine;

namespace MKFramework
{
    public class Driver : MonoBehaviour
    {
        private readonly ServiceContainer _container = new ServiceContainer();
    
        void Start()
        {
            IdService idService = new IdService();
            _container.RegisterService(idService);
        }

        private void Update()
        {
            var services = _container.GetAllServices();
            for (var i = services.Length - 1; i >= 0; i--)
            {
                services[i].OnUpdate();
            }
        }

        private void LateUpdate()
        {
        
        }
    }
}
