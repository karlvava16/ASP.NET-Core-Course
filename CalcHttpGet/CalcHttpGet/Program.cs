var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(static async (HttpContext context) =>
{
    var query = context.Request.Query;
    bool isValid = true;
    string result = "";

    int firstNumber = 0, secondNumber = 0;

    if (!query.ContainsKey("firstNumber") || !int.TryParse(query["firstNumber"].First(), out firstNumber))
    {
        isValid = false;
        result += "Invalid input for 'firstNumber'.\n";
    }

    if (!query.ContainsKey("secondNumber") || !int.TryParse(query["secondNumber"].First(), out secondNumber))
    {
        isValid = false;
        result += "Invalid input for 'secondNumber'.\n";
    }

    if (!query.ContainsKey("operation") || query["operation"].First() == null || !IsValidOperation(query["operation"].First()))
    {
        isValid = false;
        result += "Invalid input for 'operation'.\n";
    }

    if (isValid)
    {
        context.Response.StatusCode = 200;
        int calculationResult = Calculate(query["operation"].First(), firstNumber, secondNumber);
        result = $"Result: {calculationResult}";
    }
    else
    {
        context.Response.StatusCode = 400;
    }

    await context.Response.WriteAsync(result);
});

app.Run();

static bool IsValidOperation(string? op)
{
    return op == "add"
        || op == "subtract"
        || op == "multiply"
        || op == "divide"
        || op == "remainder";
}

static int Calculate(string? operation, int firstNumber, int secondNumber)
{
    return operation switch
    {
        "add" => firstNumber + secondNumber,
        "subtract" => firstNumber - secondNumber,
        "multiply" => firstNumber * secondNumber,
        "divide" => firstNumber / secondNumber,
        "remainder" => firstNumber % secondNumber,
        _ => 0,
    };
}
