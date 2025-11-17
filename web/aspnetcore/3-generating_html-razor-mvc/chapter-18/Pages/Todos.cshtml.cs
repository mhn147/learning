using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TagHelpersExample.Pages;

public class TodosModel : PageModel
{
    [BindProperty]
    public InputModel Input { get; set; } = new InputModel();

    public TodosModel()
    {
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        return RedirectToPage("./Index");
    }

    public class InputModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length is {1}")]
        public string Content { get; set; } = string.Empty;

        [Required]
        [Range(5, 30, ErrorMessage = "Duration should be between 5 and 30")]
        public int Duration { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTimeOffset DueTime { get; set; }
        
        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}

