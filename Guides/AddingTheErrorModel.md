### Adding the Error Model

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
