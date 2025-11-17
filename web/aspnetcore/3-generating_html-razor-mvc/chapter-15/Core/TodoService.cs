using Chapter15.Data;
using Microsoft.EntityFrameworkCore;

public class TodoService
{
    private AppDbContext _dbContext;

    public TodoService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Todo>> GetAll()
    {
        return await _dbContext.Todos.ToListAsync();
    }

    public async Task AddOne(Todo todo)
    {
        _dbContext.Todos.Add(todo);
        await _dbContext.SaveChangesAsync();
    }
}