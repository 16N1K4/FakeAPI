using FakeAPI.Models;

namespace FakeAPI.Repository
{
    public interface IToDoRepo
    {
        public Task<List<ToDo>> ViewAllTodos();
        public Task<ToDo> ViewOneToDo(int id);
        public Task<ToDo> AddToDo(ToDo newTodo);
        public Task<ToDo> UpdateTodo(int id, ToDo newTodo);
        public Task DeleteToDo(int id);
    }
}
