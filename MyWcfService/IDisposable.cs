
namespace MyWcfService
{
    public interface IDisposable : System.IDisposable
    {
        bool IsDisposed { get; }

        void Dispose(bool disposing);
    }
}
