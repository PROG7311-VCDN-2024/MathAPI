namespace MathAPI.Models;

public partial class MathCalculation
{
    public int CalculationId { get; set; }

    public decimal? FirstNumber { get; set; }

    public decimal? SecondNumber { get; set; }

    public int? Operation { get; set; }

    public decimal? Result { get; set; }

    public string? FirebaseUuid { get; set; }

    public static MathCalculation Create(decimal? firstNumber, decimal? secondNumber, int? operation, decimal? result, string? firebaseUuid)
    {
        if (operation == 4 && secondNumber == 0)
        {
            throw new ArgumentException("Cannot divide by zero.");
        }

        if (firstNumber == null || secondNumber == null || operation == 0 ||  firebaseUuid == null) {
            throw new ArgumentException("Missing values!");
        }

        return new MathCalculation
        {
            FirstNumber = firstNumber,
            SecondNumber = secondNumber,
            Operation = operation,
            Result = result,
            FirebaseUuid = firebaseUuid
        };
    }
}
