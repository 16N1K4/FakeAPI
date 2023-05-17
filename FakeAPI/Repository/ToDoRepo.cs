using FakeAPI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace FakeAPI.Repository
{
    public class ToDoRepo : IToDoRepo
    {
        private readonly HttpClient _client;
        private readonly string baseURL = "https://jsonplaceholder.typicode.com/";

        public ToDoRepo()
        {
            _client = new HttpClient();
        }

        public async Task<List<ToDo>> ViewAllTodos()
        {
            var response = await _client.GetAsync(baseURL + "/todos");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var todos = JsonConvert.DeserializeObject<List<ToDo>>(content);
                return todos ?? new();
            }

            return new();
        }

        public async Task<ToDo> ViewOneToDo(int id)
        {
            var response = await _client.GetAsync(baseURL + $"/todos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var todo = JsonConvert.DeserializeObject<ToDo>(content);
                return todo;
            }
            return null;
        }
        public async Task<ToDo> AddToDo(ToDo newTodo)
        {
            var newTodoAsString = JsonConvert.SerializeObject(newTodo);
            var responseBody = new StringContent(newTodoAsString, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(baseURL + "/todos", responseBody);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var todo = JsonConvert.DeserializeObject<ToDo>(content);
                return todo;
            }

            return null;
        }

        public async Task<ToDo> UpdateTodo(int id, ToDo newTodo)
        {
            var newTodoAsString = JsonConvert.SerializeObject(newTodo);
            var responseBody = new StringContent(newTodoAsString, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(baseURL + $"/todos/{id}", responseBody);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var todo = JsonConvert.DeserializeObject<ToDo>(content);
                return todo;
            }

            return null;
        }

        public async Task DeleteToDo(int id)
        {
            var response = await _client.DeleteAsync(baseURL + $"/todos/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsByteArrayAsync();
                Console.WriteLine("Delete Todo Response: ", data);
            }
        }
    }
}
