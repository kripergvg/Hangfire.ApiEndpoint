namespace DynamicJob
{
    public interface IJob<T>
    {
        void Run(T arguments);
    }

    public interface IJob
    {
        void Run();
    }
}
