namespace getAppAndGo.Repositories;

public class TodosRepository
{
  private readonly IDbConnection _db;

  public TodosRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Todo CreateTodo(Todo newTodo)
  {
    string sql = @"
    INSERT INTO todos
    (title, description, completed, creatorId)
    VALUES
    (@Title, @Description, @Completed, @CreatorId);
    SELECT
    todo.*,
    acct.*
    FROM todos todo
    JOIN accounts acct ON acct.id = todo.creatorId
    WHERE todo.id = LAST_INSERT_ID()
    ;";

    Todo todo = _db.Query<Todo, Account, Todo>(sql, (todo, account) =>
    {
      todo.Creator = account;
      return todo;
    }, newTodo).FirstOrDefault();
    return todo;
  }

  internal List<Todo> GetAllTodos()
  {
    string sql = @"
    SELECT
    todos.*,
    accts.*
    FROM todos
    JOIN accounts accts ON todos.creatorId = accts.id
    LEFT JOIN todos t ON todos.id = t.id
    GROUP BY (todos.id)
    ;";

    List<Todo> todos = _db.Query<Todo, Account, Todo>(sql, (todo, account) =>
    {
      todo.Creator = account;
      return todo;
    }, splitOn: "id").ToList();
    return todos;
  }

  internal Todo GetTodoById(int todoId)
  {
    string sql = @"
    SELECT
    todos.*,
    accts.*
    FROM todos
    JOIN accounts accts ON accts.id = todos.creatorId
    WHERE todos.id = @todoId
    ;";

    Todo todo = _db.Query<Todo, Account, Todo>(sql, (todo, account) =>
    {
      todo.Creator = account;
      return todo;
    }, new { todoId }).FirstOrDefault();
    return todo;
  }

  internal void EditTodo(Todo original)
  {
    string sql = @"
    UPDATE todos
    SET
    title = @Title,
    description = @Description,
    completed = @Completed
    WHERE id = @Id
    ;";

    _db.Execute(sql, original);
  }

  internal void DeleteTodoById(int todoId)
  {
    string sql = @"
    DELETE FROM todos
    WHERE id = @todoId
    ;";

    _db.Execute(sql, new { todoId });
  }
}