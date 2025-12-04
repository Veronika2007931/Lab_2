namespace Interfaces
{
     public delegate void WorkEnded(string message);
    public interface IDeveloperWork
    {
        void Create();
    }

    public interface IManagerWork
    {
        void Create();
    }

    public interface Report
    {
        void Report();
    }
}