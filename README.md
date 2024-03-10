## Building the MathAPI

This repo shows the building of the MathAPI in VS. A starting point for this API is the [MathApp](https://github.com/PROG7311-VCDN-2024/MathApp). I recommend building that first so you understand how it all fits together before you break it up :-)

This API covers aspects like:
* Reading from a SQL DB using EF Core
* Serving JSON results from the API
* Serving different status codes (200, 201, 400, 401, 404, etc.)
* Swagger documentation

Before commencing, please consult the following API documentation as it gives an idea of what we will be doing:
* https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio

**If you notice any errors or need to suggest improvements, please reach out to me!! I will be grateful**

It is recommended that you follow these steps in order:

1. [Creating your project](/Guides/CreatingYourProject.md)
1. [Adding the DBContext](/Guides/AddingtheDBContext.md)
1. [Adding the Error Model](/Guides/AddingTheErrorModel.md)
1. [Reusing the MathController class](/Guides/ReusingMathController.md)
1. [Updating the Calculate() to return JSON](/Guides/UpdatingCalculate.md)
1. [Updating the History() to return JSON](/Guides/UpdatingHistory.md)
1. [Updating the Clear() to return JSON](/Guides/UpdatingClear.md)
1. [Swagger and Documentation of the API](/Guides/SwaggerAndDocs.md)
