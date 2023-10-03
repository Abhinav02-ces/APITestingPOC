using APITestingPOC.Constants;
using APITestingPOC.Utils;

namespace APITestingPOC
{
    public class TestSuite
    {

        private readonly ITestOutputHelper output;
        private readonly ApiCalls_Utils apiHelper;

        public TestSuite(ITestOutputHelper output)
        {
            this.output = output;
            apiHelper = new ApiCalls_Utils(ApiConstants.BASE_URL);
        }

        [Fact]
        public async Task GetUserApiTest()
        {
            //Arrange
            //Rest Client

            var response = await apiHelper.GetAsync<User_GetApiResponse>("todos/{id}", 3);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Content);

            output.WriteLine("API response content: {0}", response.Content);

            if (!string.IsNullOrEmpty(response.Content))
            {
                var jsonResponse = JsonConvert.DeserializeObject<User_GetApiResponse>(response.Content);
                Assert.NotNull(jsonResponse);
                Assert.Equal(1, jsonResponse.userId);
                Assert.Equal(3, jsonResponse.id);
                Assert.Equal("fugiat veniam minus", jsonResponse.title);
                Assert.False(jsonResponse.Completed);
                output.WriteLine($"User ID: {jsonResponse.userId}, User title:{jsonResponse.title}");
            }
            else
            {
                output.WriteLine("Request was not successful. Status code: {0} " + response.StatusCode);
            }
        }

        [Fact]
        public async Task PostUserApiTest()
        {
            //Rest Client

            var response = await apiHelper.PostAsync<User_PostApiResponse>("posts");
            var jsonResponse = JsonConvert.DeserializeObject<User_PostApiResponse>(response.Content);


            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(jsonResponse);
            Assert.Equal("Updated Title", jsonResponse.title);
            Assert.Equal("Updated Body", jsonResponse.body);
            Assert.Equal(1, jsonResponse.userId);

            int createdId = jsonResponse.id;
            output.WriteLine("Request was successful. createdId: {0} " + createdId);
        }

        [Fact]
        public async Task PutUserApiTest()
        {
            //Rest Client

            var response = await apiHelper.PostAsync<User_PutApiResponse>("posts");
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var jsonResponse = JsonConvert.DeserializeObject<User_PutApiResponse>(response.Content);

            Assert.NotNull(jsonResponse);
            Assert.Equal(101, jsonResponse.id);
            Assert.Equal("Updated Title", jsonResponse.title);
            Assert.True(jsonResponse.completed);

        }
        [Fact]
        public async Task DeleteUserApiTest()
        {
            //Rest Client
            var response = await apiHelper.DeleteAsync("todos/{id}", 13);

            // Assertions for DELETE request
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}