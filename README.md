## Building the MathAPI

This repo shows the building of the MathAPI in VS.

This API:
* Reading from a SQL DB
* Serving JSON results
* Swagger documentation

Consult the following documentation as it gives an idea of what we will be doing:
* https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio

### Creating your project

1. Build a new project called `MathAPI` in Visual Studio of type `ASP.net Core Web API` (not app). Ensure that you enable Swagger and do not choose Docker (we will cover Docker later).
1. You will notice that this API has a `Controllers` folder but no views or models.
1. Since we are serving back JSON and not a view, we will not need a `Views` folder. We will need a `Models` folder which you can go ahead and create.
1. Run your app and check out the `/WeatherForecast` endpoint which serves JSON weather forecast data.

## Adding the DBContext

1. Follow the instructions from the [Building the DB context](https://github.com/PROG7311-VCDN-2024/MathApp/blob/master/Guides/BuildingDBContext.md) section of the `MathApp` to build the DBContext in this app.
Note: it is better to use environment variables.

## Adding the Error Model

1. Create a new class called `Error.cs` which will handle any error messages from the API.
	```
    public class Error
    {
        public string? ErrorMessage { get; set; }

        public Error(string? errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
	```

1. By this point, the Models folder must have a `MathDBContext.cs`, `Error.cs` and `MathCalculation.cs` class.

### Updating the Calculate() to return JSON

1. Reuse the original [MathCalculation.cs](https://github.com/PROG7311-VCDN-2024/MathApp/blob/master/MathApp/Controllers/MathController.cs) from the original project.
1. Amend class to not return a `View` or anything to the front-end.
1. Add in the following Attributes to the controller heading
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
    ...
    ```
1. Rename the `Calculate()` to the `PostCalculate()` and add in an attribute to the method `[HttpPost("PostCalculate")]`
    ```
    [HttpPost("PostCalculate")]
    public async Task<IActionResult> PostCalculate(MathCalculation mathCalculation)
    {
    ```
1. Modify the token check if to return an Unauthorised message (Error 401).
    ```
        if (mathCalculation.FirebaseUuid == null)
        {
            return Unauthorized(new Error("Token missing!"));
        }
    ```
1. Add in a check for a complete object and return a Bad Request message (Error 400) if incomplete.
    ```
        if (mathCalculation.FirstNumber == null || mathCalculation.SecondNumber == null || mathCalculation.Operation == 0) {
            return BadRequest(new Error("Math equation not complete!"));
        }
    ```
1. Recreate the MathCalculation object using the factory and throw an exception if not valid. 
    ```
        try
        {
            mathCalculation = MathCalculation.Create(mathCalculation.FirstNumber, mathCalculation.SecondNumber, mathCalculation.Operation, Result, mathCalculation.FirebaseUuid);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    ```
1. Modify the return to return a Success (Status 200) message with the Calculation object 
    ```
            
        return Created(mathCalculation.CalculationId.ToString(), mathCalculation);
    }
    ```
1. Test your API endpoint in Postman with a POST request at `/api/Math/PostCalculate` and pass in the following JSON in the body
    ```
   {
       "FirstNumber": 5,
       "SecondNumber": 5,
       "Operation": 1,
       "FirebaseUuid": "{insert token/uuid here}"
    }
    ```
1. You should get back the following JSON:
    ```
    {
        "calculationId": {id},
        "firstNumber": 5,
        "secondNumber": 5,
        "operation": 1,
        "result": 10,
        "firebaseUuid": "{token/uuid}"
    }
    ```

Troubleshooting: Your PostCalculation method should match [this method](https://github.com/PROG7311-VCDN-2024/MathAPI/blob/master/MathAPI/Controllers/MathController.cs#L47) (summary and other attributes explanation coming soon)


### Updating the History() to return JSON

### Updating the Clear() to return JSON

### Swagger Documentation

Consult the following documentation as it gives an idea of what we will be doing here:
* https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=visual-studio
* *https://mattfrear.com/2020/04/21/request-and-response-examples-in-swashbuckle-aspnetcore/