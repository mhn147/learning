using Chapter15.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace chapter_15.Pages;

public class ToDoModel : PageModel
{
    private readonly TodoService todoService;

    public ToDoModel(TodoService todoService)
    {
        this.todoService = todoService;
    }

    [BindProperty]
    public string Todo { get; set; } = string.Empty;

    public IEnumerable<Todo> Todos { get; set; } = new List<Todo>();

    public async Task OnGetAsync()
    {
        Todos = await todoService.GetAll();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // save to db..
        await todoService.AddOne(new Todo
        {
            Content = Todo
        });

        return Page();
    }
}