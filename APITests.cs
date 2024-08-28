using HW30.Builders;
using HW30.Models.Responses;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using NUnit.Allure.Core;

namespace HW30
{
    [AllureNUnit]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private const string BaseUrl = "https://reqres.in/api/";

        [Test]
        public void GetUserTest()
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
        public void CreateUserTest()
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
            Assert.That(modelResponse.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(5)));
        }

        [Test]
        public void GetListUsersTest()
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
        public void GetUserNotFoundTest()
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
        public void GetListResourceTest()
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
        public void PathUpdateUserTest()
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
        public void PutUpdateUserTest()
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
        public void PostRegisterSuccessfulTest()
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
            var modelResponse = JsonConvert.DeserializeObject<RegisterResponse>(response.Content);

            Assert.That(modelResponse.id, Is.EqualTo(4));
            Assert.That(modelResponse.token, Is.EqualTo("QpwL5tke4Pnpja7X4"));
        }
        [Test]
        public void PostRegisterUnuccessfulTest()
        {
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("register", Method.Post);

            var userRequest = new RegisterRequestBuilder()
                                  .Email("sydney@fife")
                                  .Build();
            request.AddJsonBody(userRequest);
            var response = clinet.Execute(request);
            Assert.That((int)response.StatusCode, Is.EqualTo(400), "Status code is not 400 OK");
            var modelResponse = JsonConvert.DeserializeObject<RegisterResponse>(response.Content);

            Assert.That(modelResponse.error, Is.EqualTo("Missing password"));
        }
        [Test]
        public void LoginSuccefullTest()
        {
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("login", Method.Post);

            var userRequest = new RegisterRequestBuilder()
                                  .Email("eve.holt@reqres.in")
                                  .Password("cityslicka")
                                  .Build();
            request.AddJsonBody(userRequest);
            var response = clinet.Execute(request);
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Status code is not 200 OK");
            var modelResponse = JsonConvert.DeserializeObject<RegisterResponse>(response.Content);

            Assert.That(modelResponse.token, Is.EqualTo("QpwL5tke4Pnpja7X4"));
        }
        [Test]
        public void LoginUnuccessfulTest()
        {
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("register", Method.Post);

            var userRequest = new RegisterRequestBuilder()
                                  .Email("peter@klaven")
                                  .Build();
            request.AddJsonBody(userRequest);
            var response = clinet.Execute(request);
            Assert.That((int)response.StatusCode, Is.EqualTo(400), "Status code is not 400 OK");
            var modelResponse = JsonConvert.DeserializeObject<RegisterResponse>(response.Content);

            Assert.That(modelResponse.error, Is.EqualTo("Missing password"));
        }
        [Test]
        public void GetSingleResourceTest()
        {
            // Создание запроса
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("unknown/2", Method.Get);

            // Выполнить запрос
            var response = clinet.Execute(request);

            // Проверка статуса ответа
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Status code is not 200 OK");

            // Парсинг JSON ответа
            var modelResponse = JsonConvert.DeserializeObject<SingleResourceResponse>(response.Content);

            // Проврерка данных                                              
            Assert.That(modelResponse.data.id, Is.EqualTo(2));
            Assert.That(modelResponse.data.name, Is.EqualTo("fuchsia rose"));
            Assert.That(modelResponse.data.year, Is.EqualTo(2001));
            Assert.That(modelResponse.data.color, Is.EqualTo("#C74375"));
            Assert.That(modelResponse.data.pantone_value, Is.EqualTo("17-2031"));
            Assert.That(modelResponse.support.url, Is.EqualTo("https://reqres.in/#support-heading"));
            Assert.That(modelResponse.support.text, Is.EqualTo("To keep ReqRes free, contributions towards server costs are appreciated!"));
        }
        [Test]
        public void GetSingleResourceNotFoundTest()
        {
            // Создание запроса
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("unknown/23", Method.Get);

            // Выполнить запрос
            var response = clinet.Execute(request);

            // Проверка статуса ответа
            Assert.That((int)response.StatusCode, Is.EqualTo(404), "Status code is not 200 OK");
        }

        [Test]
        public void GetDelayedResponseTest()
        {
            // Создание запроса
            var clinet = new RestClient(BaseUrl);
            var request = new RestRequest("users?delay=3", Method.Get);
            // Выполнить запрос
            var response = clinet.Execute(request);
            // Парсинг JSON ответа
            var jsonResponse = JObject.Parse(response.Content);
            // Проверка статуса ответа
            Assert.That((int)response.StatusCode, Is.EqualTo(200), "Status code is not 200");
            // Парсинг JSON ответа
            var modelResponse = JsonConvert.DeserializeObject<DelayedResponse>(response.Content);
            // Проврерка данных                      
            Assert.That(modelResponse.page, Is.EqualTo(1));
            Assert.That(modelResponse.per_page, Is.EqualTo(6));
            Assert.That(modelResponse.total, Is.EqualTo(12));
            Assert.That(modelResponse.total_pages, Is.EqualTo(2));

            Assert.That(modelResponse.data[0].id, Is.EqualTo(1));
            Assert.That(modelResponse.data[0].email, Is.EqualTo("george.bluth@reqres.in"));
            Assert.That(modelResponse.data[0].first_name, Is.EqualTo("George"));
            Assert.That(modelResponse.data[0].last_name, Is.EqualTo("Bluth"));
            Assert.That(modelResponse.data[0].avatar, Is.EqualTo("https://reqres.in/img/faces/1-image.jpg"));

            Assert.That(modelResponse.data[1].id, Is.EqualTo(2));
            Assert.That(modelResponse.data[1].email, Is.EqualTo("janet.weaver@reqres.in"));
            Assert.That(modelResponse.data[1].first_name, Is.EqualTo("Janet"));
            Assert.That(modelResponse.data[1].last_name, Is.EqualTo("Weaver"));
            Assert.That(modelResponse.data[1].avatar, Is.EqualTo("https://reqres.in/img/faces/2-image.jpg"));

            Assert.That(modelResponse.data[2].id, Is.EqualTo(3));
            Assert.That(modelResponse.data[2].email, Is.EqualTo("emma.wong@reqres.in"));
            Assert.That(modelResponse.data[2].first_name, Is.EqualTo("Emma"));
            Assert.That(modelResponse.data[2].last_name, Is.EqualTo("Wong"));
            Assert.That(modelResponse.data[2].avatar, Is.EqualTo("https://reqres.in/img/faces/3-image.jpg"));

            Assert.That(modelResponse.data[3].id, Is.EqualTo(4));
            Assert.That(modelResponse.data[3].email, Is.EqualTo("eve.holt@reqres.in"));
            Assert.That(modelResponse.data[3].first_name, Is.EqualTo("Eve"));
            Assert.That(modelResponse.data[3].last_name, Is.EqualTo("Holt"));
            Assert.That(modelResponse.data[3].avatar, Is.EqualTo("https://reqres.in/img/faces/4-image.jpg"));

            Assert.That(modelResponse.data[4].id, Is.EqualTo(5));
            Assert.That(modelResponse.data[4].email, Is.EqualTo("charles.morris@reqres.in"));
            Assert.That(modelResponse.data[4].first_name, Is.EqualTo("Charles"));
            Assert.That(modelResponse.data[4].last_name, Is.EqualTo("Morris"));
            Assert.That(modelResponse.data[4].avatar, Is.EqualTo("https://reqres.in/img/faces/5-image.jpg"));

            Assert.That(modelResponse.data[5].id, Is.EqualTo(6));
            Assert.That(modelResponse.data[5].email, Is.EqualTo("tracey.ramos@reqres.in"));
            Assert.That(modelResponse.data[5].first_name, Is.EqualTo("Tracey"));
            Assert.That(modelResponse.data[5].last_name, Is.EqualTo("Ramos"));
            Assert.That(modelResponse.data[5].avatar, Is.EqualTo("https://reqres.in/img/faces/6-image.jpg"));

            Assert.That(modelResponse.support.url, Is.EqualTo("https://reqres.in/#support-heading"));
            Assert.That(modelResponse.support.text, Is.EqualTo("To keep ReqRes free, contributions towards server costs are appreciated!"));
        }
    }
}