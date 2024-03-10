### Updating the History() to return JSON

1. Amend `History()` method to not return a `View` or anything to the front-end (like a ViewBag or ViewData). Instead, all the method must return is an object or a collection of objects.

1. Rename `History()` to `GetHistory()` and add in an attribute to the method `[HttpGet("GetHistory")]`

    ```
    [HttpGet("GetHistory")]
    public async Task<IActionResult> GetHistory(string Token)
    {
        ...  
    ```
1. Modify the token check if to return an Unauthorised message (Error 401).
    ```
        if (Token == null)
        {
            return Unauthorized(new Error("Token missing!"));
        }
    ```

1. Get values from the DB
    ```
        List<MathCalculation> historyItems = await _context.MathCalculations.Where(m => m.FirebaseUuid.Equals(Token)).ToListAsync();

    ```
1. Modify the return to return a Success (Status 200) message with the list of Calculations or not found if no calculations found for a user: 
    ```
        if (historyItems.Count > 0)
        {
            return Ok(historyItems);
        } else
        {
            return NotFound(new Error("No history found!"));
        }
    }
    ```
1. Test your API endpoint in Postman with a GET request at `/api/Math/GetHistory` and pass in the following parameter:
    ```
        token={insert token/uuid here}
    ```
1. You should get back the following JSON (or something similar):
    ```
    [
        {
            "calculationId": 25,
            "firstNumber": 5.00,
            "secondNumber": 5.00,
            "operation": 10,
            "result": 1.00,
            "firebaseUuid": "{token}"
        },
        {
            "calculationId": 26,
            "firstNumber": 5.00,
            "secondNumber": 5.00,
            "operation": 1,
            "result": 10.00,
            "firebaseUuid": "{token}"
        }
    ]
    ```
    Note that if you have no history, your API will pass back this JSON:
    ```
    {
        "errorMessage": "No history found!"
    }
    ```

Troubleshooting: If you get any errors, ensure that your GetHistory method matches [this method](https://github.com/PROG7311-VCDN-2024/MathAPI/blob/master/MathAPI/Controllers/MathController.cs#L120) (summary and other attributes explanation coming soon)
