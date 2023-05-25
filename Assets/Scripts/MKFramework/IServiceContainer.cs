namespace MKFramework
{
    public interface IServiceContainer
    {
        public T GetService<T>() where T : IService;

        public IService[] GetAllServices();

    }
}