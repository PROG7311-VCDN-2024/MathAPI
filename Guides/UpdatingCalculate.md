### Updating the Calculate() to return JSON

1. Amend `Calculate()` method to not return a `View` or anything to the front-end (like a ViewBag or ViewData). Instead, all the method must return is an object or a collection of objects.

1. Rename `Calculate()` to `PostCalculate()` and add in an attribute to the method `[HttpPost("PostCalculate")]`
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

Troubleshooting: If you get any errors, ensure that your PostCalculation method should match [this method](https://github.com/PROG7311-VCDN-2024/MathAPI/blob/master/MathAPI/Controllers/MathController.cs#L47) (summary and other attributes explanation coming soon)