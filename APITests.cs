using HW30.Builders;
using HW30.Models.Responses;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;

namespace HW30
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private const string BaseUrl = "https://reqres.in/api/";

        [Test]
        public void GetUser()
        {
            // Создание запроса
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("users/2", Method.Get);

            // Выполнить запрос
            var response = clinet.Execute(request);

            // Парсинг JSON ответа
            var jsonResponse = JObject.Parse(response.Content);

            // Проверка статуса ответа
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Status code is not 200 OK");

            // Доступ к данным
            var data = jsonResponse["data"];
            var support = jsonResponse["support"];

            // Проврерка данных
            Assert.That((int)data["id"], Is.EqualTo(2), "Id is no 2");
            Assert.That((string)data["email"], Is.EqualTo("janet.weaver@reqres.in"));
            Assert.That((string)data["first_name"], Is.EqualTo("Janet"));
            Assert.That((string)data["last_name"], Is.EqualTo("Weaver"));

            Assert.That((string)support["text"], Is.EqualTo("To keep ReqRes free, contributions towards server costs are appreciated!"));
        }

        [Test]
        public void CreateUser()
        {
            // Создание запроса
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("users", Method.Post);
            // Создание тела запроса
            var userRequest = new UserRequestBuilder()
                                   .Name("morpheus")
                                   .Job("leader")
                                   .Build();
            request.AddJsonBody(userRequest);
            // Выполнить запрос
            var response = clinet.Execute(request);
            // Проверка статуса ответа
            Assert.That((int)response.StatusCode, Is.EqualTo(201), "Status code is not 201 OK");
                        
            var modelResponse = JsonConvert.DeserializeObject<UserResponse>(response.Content);
            
            Assert.That(modelResponse.Name, Is.EqualTo("morpheus"));
            Assert.That(modelResponse.Job, Is.EqualTo("leader"));
            Assert.That(modelResponse.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(2)));
        }      

        [Test]
        public void GetListUsers()
        {
            // Создание запроса
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("users?page=2", Method.Get);

            // Выполнить запрос
            var response = clinet.Execute(request);

            // Парсинг JSON ответа
            //var jsonResponse = JObject.Parse(response.Content);

            // Проверка статуса ответа
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Status code is not 200 OK");

            // Доступ к данным
            //var data = jsonResponse["data"];
            //var support = jsonResponse["support"];

            // Парсинг JSON ответа
            var modelResponse = JsonConvert.DeserializeObject<ListUsersResponse>(response.Content);

            // Проврерка данных                      
            Assert.That(modelResponse.page, Is.EqualTo(2));
            Assert.That(modelResponse.per_page, Is.EqualTo(6));
            Assert.That(modelResponse.total, Is.EqualTo(12));
            Assert.That(modelResponse.total_pages, Is.EqualTo(2));
            Assert.That(modelResponse.data[0].id, Is.EqualTo(7));
            Assert.That(modelResponse.data[0].email, Is.EqualTo("michael.lawson@reqres.in"));
            Assert.That(modelResponse.data[0].first_name, Is.EqualTo("Michael"));
            Assert.That(modelResponse.data[0].last_name, Is.EqualTo("Lawson"));
            Assert.That(modelResponse.data[0].avatar, Is.EqualTo("https://reqres.in/img/faces/7-image.jpg"));

        }
        [Test]
        public void GetUserNotFound()
        {
            // Создание запроса
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("users/23", Method.Get);

            // Выполнить запрос
            var response = clinet.Execute(request);

            // Парсинг JSON ответа
            //var jsonResponse = JObject.Parse(response.Content);

            // Проверка статуса ответа
            Assert.That((int)response.StatusCode, Is.EqualTo(404), "Status code is not 404");

        }
        [Test]
        public void GetListResource() 
        {
            // Создание запроса
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("unknow", Method.Get);
            // Выполнить запрос
            var response = clinet.Execute(request);
            // Парсинг JSON ответа
            var jsonResponse = JObject.Parse(response.Content);
            // Проверка статуса ответа
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Status code is not 200");
            // Парсинг JSON ответа
            var modelResponse = JsonConvert.DeserializeObject<ListResourceResponse>(response.Content);
            // Проврерка данных                      
            Assert.That(modelResponse.page, Is.EqualTo(1));
            Assert.That(modelResponse.per_page, Is.EqualTo(6));
            Assert.That(modelResponse.total, Is.EqualTo(12));
            Assert.That(modelResponse.total_pages, Is.EqualTo(2));
            Assert.That(modelResponse.data[0].id, Is.EqualTo(1));
            Assert.That(modelResponse.data[0].name, Is.EqualTo("cerulean"));
            Assert.That(modelResponse.data[0].year, Is.EqualTo(2000));
            Assert.That(modelResponse.data[0].color, Is.EqualTo("#98B2D1"));
            Assert.That(modelResponse.data[0].pantone_value, Is.EqualTo("15-4020"));
        }
        [Test]
        public void PathUpdateUser()
        {            
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("users/2", Method.Patch);

            // Создание тела запроса
            var userRequest = new UserRequestBuilder()
                                   .Name("morpheus")
                                   .Job("zion resident")
                                   .Build();
            request.AddJsonBody(userRequest);
            var response = clinet.Execute(request);                       
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Status code is not 200 OK");

            // Парсинг JSON ответа
            var modelResponse = JsonConvert.DeserializeObject<UserUpdateResponse>(response.Content);

            // Проврерка данных
            Assert.That(modelResponse.Name, Is.EqualTo("morpheus"));
            Assert.That(modelResponse.Job, Is.EqualTo("zion resident"));
            Assert.That(modelResponse.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(6)));
        }
        [Test]
        public void PutUpdateUser()
        {
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("users/2", Method.Put);

            // Создание тела запроса
            var userRequest = new UserRequestBuilder()
                                   .Name("morpheus")
                                   .Job("zion resident")
                                   .Build();
            request.AddJsonBody(userRequest);
            var response = clinet.Execute(request);
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Status code is not 200 OK");

            // Парсинг JSON ответа
            var modelResponse = JsonConvert.DeserializeObject<UserUpdateResponse>(response.Content);

            // Проврерка данных
            Assert.That(modelResponse.Name, Is.EqualTo("morpheus"));
            Assert.That(modelResponse.Job, Is.EqualTo("zion resident"));
            Assert.That(modelResponse.UpdatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(6)));
        }
        [Test]
        public void DeleteUser()
        {
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("users/2", Method.Delete);                      
            var response = clinet.Execute(request);
            Assert.That((int)response.StatusCode, Is.EqualTo(204), "Status code is not 204 OK");
        }
        [Test]
        public void PostRegisterSuccessful()
        {
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("register", Method.Post);
            
            var userRequest = new RegisterRequestBuilder()
                                  .Email("eve.holt@reqres.in")
                                  .Password("pistol")
                                  .Build();
            request.AddJsonBody(userRequest);
            var response = clinet.Execute(request);
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Status code is not 200 OK");           
        }
    }
}