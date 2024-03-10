### Reusing the MathController class (at least significant parts of it)

1. Reuse the original [MathController.cs](https://github.com/PROG7311-VCDN-2024/MathApp/blob/master/MathApp/Controllers/MathController.cs) from the original project. Add this to the `Controllers ` folder in your new project.
1. Add in the following Attributes to the controller heading which sets the controller to respond to requests made to `/api/Math`

    ```
    [Route("api/[controller]")]
    [ApiController]
    public class MathController : Controller
    {
        private readonly MathDbContext _context;

        public MathController(MathDbContext context)
        {
            _context = context;
        }
    ... // more code is normally here
    }
    ```
1. We will amend the controller further in the next section.
