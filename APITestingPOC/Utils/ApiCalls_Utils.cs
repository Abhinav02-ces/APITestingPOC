namespace APITestingPOC.Utils
{
    public class ApiCalls_Utils
    {

        private readonly RestClient _client;

        public ApiCalls_Utils(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public async Task<RestResponse<T>> GetAsync<T>(string resource, int id)
        {
            var request = CreateRestRequest(resource, Method.Get);
            request.AddUrlSegment("id", id);

            var response = await _client.ExecuteAsync<T>(request);
            return response;
        }

        public async Task<RestResponse<T>> PostAsync<T>(string resource)
        {
            var request = CreateRestRequest(resource, Method.Post);
            var requestBody = new User_PostApiResponse
            {
                title = "Updated Title",
                body = "Updated Body",
                userId = 1
            };

            request.AddJsonBody(requestBody);
            var response = await _client.ExecuteAsync<T>(request);
            return response;
        }


        public async Task<RestResponse<T>> PutAsync<T>(string resource)
        {
            var request = CreateRestRequest(resource, Method.Put);
            var requestBody = new User_PutApiResponse
            {
                id = 101,
                title = "Updated Title",
                completed = true
            };

            request.AddJsonBody(requestBody);
            var response = await _client.ExecuteAsync<T>(request);
            return response;
        }

        public async Task<RestResponse> DeleteAsync(string resource, int id)
        {
            var request = CreateRestRequest(resource, Method.Delete);
            request.AddUrlSegment("id", id);
            var response = await _client.ExecuteAsync(request);
            return response;
        }

        public static RestRequest CreateRestRequest(string resource, Method method)
        {
            return new RestRequest(resource, method);
        }
    }
}