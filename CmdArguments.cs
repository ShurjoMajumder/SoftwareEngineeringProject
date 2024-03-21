namespace SoftwareEngineeringProject;

/// <summary>
/// Parses the command line arguments.
/// </summary>
public class CmdArguments
{
    /// <summary>
    /// Logging is enabled/disabled.
    /// </summary>
    public bool Logging { get; private set; }
    
    /// <summary>
    /// Path to products csv file.
    /// </summary>
    public string ProductsPath { get; private set; }
    
    /// <summary>
    /// Path to suppliers csv file.
    /// </summary>
    public string SuppliersPath { get; private set; }
    
    /// <summary>
    /// Path to output file (defaults to ./out.txt)
    /// </summary>
    public string OutputPath { get; private set; }
    
    private InputState _inputState;
    
    /// <summary>
    /// InputState enum. Each value represents one of the parser's possible states.
    /// </summary>
    private enum InputState
    {
        InputDefault = 0,
        InputProductsPath,
        InputSuppliersPath,
        InputOutputPath,
    }
    
    /// <summary>
    /// Parses command line arguments, and returns a CmdArguments object containing the input paths, output path,
    /// and whether logging is enabled.
    /// </summary>
    /// <param name="args"></param>
    public CmdArguments(in IEnumerable<string> args)
    {
        Logging = true;
        _inputState = InputState.InputDefault;
        ProductsPath = "";
        SuppliersPath = "";
        OutputPath = "./out.txt";

        if (!args.Any())
        {
            PrintHelpMessage();
            return;
        }
        
        Parse(args);
        ValidateInOutPaths();
    }

    /// <summary>
    /// Iterates through the passed arguments. State-based parser that iterates through arguments once.
    /// </summary>
    /// <param name="args">Enumerable containing the cmd arguments.</param>
    private void Parse(in IEnumerable<string> args)
    {
        foreach (var arg in args)
        {
            ReadArgument(arg);
        }
    }

    /// <summary>
    /// Reads an argument, and determines the state the parser is in for the next iteration.
    /// </summary>
    /// <param name="arg">Argument to read.</param>
    private void ReadArgument(string arg)
    {
        switch (arg)
        {
            case "--nl" or "--nlg" or "--no-log" or "--nolog":
            {
                Logging = false;
                return;
            }
            case "-o" or "-out" or "-output":
            {
                _inputState = InputState.InputOutputPath;
                return;
            }
            case "-p" or "-prod" or "-prods" or "-products":
            {
                _inputState = InputState.InputProductsPath;
                return;
            }
            case "-s" or "-sup" or "-sups" or "-suppliers":
            {
                _inputState = InputState.InputSuppliersPath;
                return;
            }
            case "help" or "-h" or "?":
            {
                PrintHelpMessage();
                break;
            }
            default:
            {
                ConsumeArgument(arg);
                _inputState = InputState.InputDefault; // reset input state to default
                return;
            }
        }
    }

    private static void PrintHelpMessage()
    {
        Console.WriteLine("Arguments:");
        Console.WriteLine(" -[products|prods|prod|p] - specifies the path to the product file.");
        Console.WriteLine(" -[suppliers|sups|sup|s] - specifies the path to the supplier file.");
        Console.WriteLine(" -[output|out|o] - output path. overwrites pre-existing file. (defaults to ./out.txt)");
        Console.WriteLine("--[no-log|nolog|nlg|nl] - disables logging.");
        Console.WriteLine("\nUsage:");
        Console.WriteLine("SoftwareEngineeringProject -p [path to products file] -s [path to suppliers file] -o [output path]");
        
        Environment.Exit(0);
    }

    /// <summary>
    /// Consumes an input type based on the parser state.
    /// </summary>
    /// <param name="arg"></param>
    /// <exception cref="ArgumentException">Invalid argument was passed to the program.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Invalid parser state was entered.</exception>
    private void ConsumeArgument(string arg)
    {
        if (arg.StartsWith("-"))
        {
            throw new ArgumentException($"Invalid argument {arg}, expected a path, got a flag instead");
        }
        
        switch (_inputState)
        {
            case InputState.InputDefault:
                return;
            case InputState.InputProductsPath:
            {
                ProductsPath = arg;
                return;
            }
            case InputState.InputSuppliersPath:
            {
                SuppliersPath = arg;
                return;
            }
            case InputState.InputOutputPath:
            {
                OutputPath = arg;
                return;
            }
            default:
            {
                // this *should* never happen.
                throw new ArgumentOutOfRangeException(arg, "Invalid parser state.");
            }
        }
    }

    /// <summary>
    /// Validates the paths are not null. Invalid paths will fail when attempting to read/write.
    /// </summary>
    /// <exception cref="Exception"></exception>
    private void ValidateInOutPaths()
    {
        if (ProductsPath == null)
        {
            throw new Exception("Products path is null.");
        }

        if (SuppliersPath == null)
        {
            throw new Exception("Suppliers path is null.");
        }
        
        if (!Logging) { return; }
        
        Console.WriteLine($"> Received products path: {ProductsPath}");
        Console.WriteLine($"> Received suppliers path: {SuppliersPath}");
        Console.WriteLine($"> Received output path: {OutputPath}");
    }
}
