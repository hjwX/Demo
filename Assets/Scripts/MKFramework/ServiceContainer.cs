using System;
using System.Collections.Generic;
using System.Linq;

namespace MKFramework
{
    public class ServiceContainer : IServiceContainer
    {
        private readonly Dictionary<Type, IService> _allServices = new Dictionary<Type, IService>();

        public void RegisterService(IService service, bool overWriteExisting = true)
        {
            if (service.GetType().GetInterfaces().All(t => t != typeof(IService))) return;
            var type = service.GetType();
            if (!_allServices.ContainsKey(type))
            {
                _allServices.Add(type, service);
            }
            else if (overWriteExisting)
            {
                _allServices[type] = service;
            }
        }


        public T GetService<T>() where T : IService
        {
            var key = typeof(T);
            if (_allServices.ContainsKey(key))
                return (T) _allServices[key];
            else
                return default(T);
        }

        public IService[] GetAllServices()
        {
            return _allServices.Values.ToArray();
        }
    }
}