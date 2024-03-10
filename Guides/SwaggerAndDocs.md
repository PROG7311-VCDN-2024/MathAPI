## Swagger and Documentation of the API

Before commencing, consult the following documentation as it gives an idea of what we will be doing here:
* https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=visual-studio
* https://mattfrear.com/2020/04/21/request-and-response-examples-in-swashbuckle-aspnetcore/

Note: When you run your API, becuase you already added Swagger, you may have noticed documentation loading up. Hopefully, it will now make sense and you can customise it.

### Adding in Swagger to your project

1. You would have added Swagger to your project when you created it. If not, install these nuget packages:
    * `Swashbuckle.AspNetCore`
    * `Swashbuckle.AspNetCore.Annotations`

1.  In `Program.cs`, add in the following service definition (before `builder.Services.AddDbContext`). This defines the properties of our Swagger documentation and allows any annotations and remarks we add to the code to appear in the documentation as well:
    ```
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Math API",
            Description = "An ASP.NET Core Web API for managing MathCalculations"
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
    ```

1. Make following amendment further down in the Program.cs class:
    ```
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger(); // Add this
        app.UseSwaggerUI(); // Add this
    }
    ```
1. Run you app, it should load the documentation.

### Customising documentation for PostCalculate()

1. Add in the following attributes to `PostCalculate()`:

    ```
    [ProducesResponseType(typeof(MathCalculation), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [Produces("application/json")]
    ```
    This is then read by Swagger to ascertain the type of data returned is json and allows us to set the return types based on the status codes.

1. Add in the following XML before the attributes of `PostCalculate()`:

    ```
    /// <summary>Creates and performs a MathCalculation</summary>
    /// <param name="mathCalculation">a MathCalculation object for processing</param>
    /// <returns>A MathCalculation object with the result</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /PostCalulate
    ///     {
    ///        "FirstNumber": 5,
    ///        "SecondNumber": 5,
    ///        "Operation": 1,
    ///        "FirebaseUuid": "{insert token here}"
    ///     }
    /// </remarks>
    /// <response code="201">Returns the newly created calculation</response>
    /// <response code="400">Returns if a request is missing details or fails</response>
    /// <response code="401">Returns if a request is missing a token</response>
    ````

    What is happening here:
    * <summary>: this the summary show in documentation
    * <param>: allows to define parameters needed, in this case a MathCalculation object
    * <returns>: allow us to define a return type, in this case a MathCalculation object with the result
    * <remarks>: allows us to add remarks into the documentation with a sample request.
    * <response>: allows us to specify the status codes and explanation of each.

1. Run the API and explore the Swagger docs.

### Customising documentation for GetHistory()
1. Add in the following attributes to `GetHistory()`:

    ```
    [ProducesResponseType(typeof(List<MathCalculation>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [Produces("application/json")]
    ```
    This is then read by Swagger to ascertain the type of data returned is json and allows us to set the return types based on the status codes.
1. Add in the following XML before the attributes of `GetHistory()`:

    ```
    /// <summary>Gets the MathCalculation history for a user</summary>
    /// <param name="Token">Token of the current user.</param>
    /// <returns>A list of MathCalcuation objects</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /GetHistory
    ///     {
    ///        "Token": "{Insert token here}"
    ///     }
    /// </remarks>
    /// <response code="200">Returns the list of calculations for a user</response>
    /// <response code="400">Returns if a request is missing details or fails</response>
    /// <response code="401">Returns if a request is missing a token</response>
    /// <response code="404">Returns if no history found</response>
    ````

    What is happening here:
    * <summary>: this the summary show in documentation
    * <param>: allows to define parameters needed, in this case a Token/FirebaseUuid
    * <returns>: allow us to define a return type, in this case a collection of MathCalculation objects
    * <remarks>: allows us to add remarks into the documentation with a sample request.
    * <response>: allows us to specify the status codes and explanation of each.
1. Run the API and explore the Swagger docs.

### Customising documentation for DeleteHistory()

1. Add in the following attributes to `DeleteHistory()`:
    ```
    [ProducesResponseType(typeof(List<MathCalculation>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
    [Produces("application/json")]
    ```
    This is then read by Swagger to ascertain the type of data returned is json and allows us to set the return types based on the status codes.

1. Add in the following XML before the attributes of `DeleteHistory()`:
    ```
    /// <summary>
    /// Deletes the MathCalculation history for a user
    /// </summary>
    /// <param name="Token">Token of the current user.</param>
    /// <returns>List of deleted items</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /DeleteHistory
    ///     {
    ///        "Token": "{Insert token here}"
    ///     }
    /// </remarks>
    /// <response code="200">Returns the list of calculations deleted for a user</response>
    /// <response code="400">Returns if a request is missing details or fails</response>
    /// <response code="401">Returns if a request is missing a token</response>
    /// <response code="404">Returns if no history found</response>
    ```
1. Run the API and explore the Swagger docs.
