namespace getAppAndGo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
  private readonly TodosService _tds;
  private readonly Auth0Provider _auth;

  public TodosController(TodosService tds, Auth0Provider auth)
  {
    _tds = tds;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]

  public async Task<ActionResult<Todo>> CreateTodo([FromBody] Todo newTodo)
  {
    try
    {
      Account UserInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      newTodo.CreatorId = UserInfo.Id;
      Todo todo = _tds.CreateTodo(newTodo);
      return Ok(todo);
    }
    catch (Exception e)
    {

      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<List<Todo>>> GetAllTodos()
  {
    try
    {
      List<Todo> todos = _tds.GetAllTodos();
      return Ok(todos);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{todoId}")]
  public ActionResult<Todo> GetTodoById(int todoId)
  {
    try
    {
      Todo todo = _tds.GetTodoById(todoId);
      return Ok(todo);
    }
    catch (Exception e)
    {
      
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{todoId}")]
  [Authorize]
  public async Task<ActionResult<Todo>> EditTodo([FromBody] Todo updatedTodo, int todoId)
  {
    try
    {
      Account UserInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string userId = UserInfo.Id;
      Todo todo = _tds.EditTodo(userId, todoId, updatedTodo);
      return Ok(todo);
    }
    catch (Exception e)
    {

      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{todoId}")]
  [Authorize]
  public async Task<ActionResult<Todo>> DeleteTodoById(int todoId)
  {
    try
    {
      Account UserInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string userId = UserInfo.Id;
      string deleted = _tds.DeleteTodoById(userId, todoId);
      return Ok(deleted + " Todo Deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}