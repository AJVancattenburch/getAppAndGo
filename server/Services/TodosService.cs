namespace getAppAndGo.Services;

public class TodosService
{
  private readonly TodosRepository _repo;
  public TodosService(TodosRepository repo)
  {
    _repo = repo;
  }

  internal Todo CreateTodo(Todo newTodo)
  {
    Todo todo = _repo.CreateTodo(newTodo);
    return newTodo;
  }

  internal List<Todo> GetAllTodos()
  {
    List<Todo> todos = _repo.GetAllTodos();
    return todos;
  }

  internal Todo GetTodoById(int todoId)
  {
    Todo todo = _repo.GetTodoById(todoId);
    if (todo == null)
    {
      throw new Exception("Invalid Todo Id");
    }
    return todo;
  }

  internal Todo EditTodo(string userId, int todoId, Todo updatedTodo)
  {
    Todo original = GetTodoById(todoId);
    if (original.CreatorId == userId)
    {
      original.Title = updatedTodo.Title ?? original.Title;
      original.Description = updatedTodo.Description ?? original.Description;

      _repo.EditTodo(original);
      return original;
    }
      else
    {
      throw new Exception("You cannot edit a Todo that you did not create");
    }
  }

  internal string DeleteTodoById(string userId, int todoId)
  {
    Todo todo = GetTodoById(todoId);
    if (todo.CreatorId == userId)
    {
      _repo.DeleteTodoById(todoId);
      return $"Todo #{todoId} has been successfully deleted";
    }
    else
    {
      throw new Exception("You cannot delete a Todo that you did not create!");
    }
  }
}