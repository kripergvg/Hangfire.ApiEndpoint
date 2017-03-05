namespace DynamicJob.Core
{
    public interface IJobWrapper
    {
        void Run(string name, string arguments);
    }
}
