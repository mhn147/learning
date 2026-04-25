using Microsoft.AspNetCore.Mvc;

namespace HttpApiControllers.Controllers;

[ApiController]
//[Route("[controller]")]
public class FruitController : ControllerBase
{
    [HttpGet("fruit")]
    public IEnumerable<string> Get()
    {
        return ["Banana", "Apple", "Pear"];
    }

    [HttpGet("fruit/{id}")]
    public ActionResult<string> GetOne(int id)
    {
        if (id <= 0)
        {
            return NotFound();
        }
        return "foo";
    }
}
