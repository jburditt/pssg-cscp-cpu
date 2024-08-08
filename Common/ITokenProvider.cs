using System.Threading.Tasks;

    public interface ITokenProvider
    {
        Task<string> AcquireToken();
    }
