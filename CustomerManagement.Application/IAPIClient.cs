using RestSharp;

namespace CustomerManagement.Application
{
    public interface IAPIClient
    {
        Task<List<T>> GetList<T>();
        Task<T> GetById<T>(Guid id);
        Task<RestResponse> Create<T>(T payLoad);
        Task<T> Update<T>(T payLoad, Guid id);
        Task<RestResponse> Delete(Guid id);

    }
}
