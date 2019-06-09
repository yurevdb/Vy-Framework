namespace Vy
{
    public interface IFrameworkEnvironment
    {
        string Configuration { get; }

        bool IsDevelopment { get; }
    }
}
