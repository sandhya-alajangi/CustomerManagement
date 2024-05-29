using Newtonsoft.Json;
using RestSharp;

namespace CustomerManagement.Application
{
    public class APIClient : IAPIClient, IDisposable
    {
        private readonly RestClient restClient;
        private readonly string _baseurl;

        public APIClient(string baseurl)
        {
            var options = new RestClientOptions();
            restClient = new RestClient(options);
            _baseurl = baseurl;
            restClient = new RestClient(_baseurl);
        }

        /// <summary>
        /// Method to create a customer.
        /// </summary>
        /// <typeparam name="T">Type of the response data.</typeparam>
        /// <param name="payLoad">Data to be sent in the request payload.</param>
        /// <returns>The response data of type T.</returns>
        public async Task<RestResponse> Create<T>(T payLoad)
        {
            var request = new RestRequest("", Method.Post);
            request.AddBody(payLoad);
            var response = await restClient.ExecuteAsync<T>(request);
            return response;

        }

        /// <summary>
        /// Method to delete a customer by ID.
        /// </summary>
        /// <typeparam name="T">Type of the response data.</typeparam>
        /// <param name="id">ID of the customer to be deleted.</param>
        /// <returns>The response data of type T.</returns>
        public async Task<RestResponse> Delete(Guid id)
        {
            var request = new RestRequest("{id}", Method.Delete);
            request.AddHeader("Accept", "application/json");
            request.AddUrlSegment("id", id);
            var response = restClient.ExecuteAsync(request).Result;
            if (response.ErrorException != null)
            {
                throw new Exception($"Error while retrieving customers: {response.ErrorMessage}", response.ErrorException);
            }
            return response;
        }

        /// <summary>
        /// Method to dispose resources.
        /// </summary>
        public void Dispose()
        {
            restClient?.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Method to get a list of customers.
        /// </summary>
        /// <typeparam name="T">Type of the response data.</typeparam>
        /// <returns>The list of customers of type T.</returns>
        public async Task<List<T>> GetList<T>()
        {
            var request = new RestRequest("", Method.Get);
            request.AddHeader("Accept", "application/json");
            return await ExecuteRequest<List<T>>(request);           
        }

        /// <summary>
        /// Method to get a customer by ID.
        /// </summary>
        /// <typeparam name="T">Type of the response data.</typeparam>
        /// <param name="id">ID of the customer.</param>
        /// <returns>The customer data of type T.</returns>
        public async Task<T> GetById<T>(Guid id)
        {
            var request = new RestRequest("{id}", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddUrlSegment("id", id);
            return await ExecuteRequest<T>(request);
        }


        /// <summary>
        /// Updates an existing resource identified by the specified ID with the provided payload using the update method.
        /// </summary>
        /// <typeparam name="T">The type of the payload and response data.</typeparam>
        /// <param name="payload">The payload containing the properties to update.</param>
        /// <returns>The updated resource of type T.</returns>
        public async Task<T> Update<T>(T payLoad,Guid id)
        {
            var request = new RestRequest("{id}", Method.Put);
            request.AddHeader("Accept", "application/json");
            request.AddUrlSegment("id", id);
            request.AddBody(payLoad);
            return await ExecuteRequest<T>(request);
        }

        private async Task<T> ExecuteRequest<T>(RestRequest request)
        {
            var response = await restClient.ExecuteAsync<T>(request);
            if (response.ErrorException != null)
            {
                throw new Exception($"Error while executing request: {response.ErrorMessage}", response.ErrorException);
            }
            var content = JsonConvert.DeserializeObject<T>(response.Content);
            return content;
        }
        
    }
    
}


