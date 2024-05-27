namespace getAppAndGo.Models;

public class Todo
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public bool Completed { get; set; }
  public string CreatorId { get; set; }
  public Account Creator { get; set; }
}