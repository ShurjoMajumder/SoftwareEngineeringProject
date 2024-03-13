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
        
        Parse(args);
        ValidateInOutPaths();
    }

    /// <summary>
    /// Iterates through the passed arguments. State-based parser that iterates through arguments once.
    /// </summary>
    /// <param name="args">Enumerable containing the cmd arguments.</param>
    private void Parse(IEnumerable<string> args)
    {
        foreach (var arg in args)
        {
            switch (arg)
            {
                case "--nl":
                case "--nlg":
                case "--no-log":
                    Logging = false;
                    break;
                case "-o":
                case "-out":
                case "-output":
                {
                    _inputState = InputState.InputOutputPath;
                    break;
                }
                case "-p":
                case "-prod":
                case "-prods":
                case "-products":
                {
                    _inputState = InputState.InputProductsPath;
                    break;
                }
                case "-s":
                case "-sup":
                case "-sups":
                case "-suppliers":
                {
                    _inputState = InputState.InputSuppliersPath;
                    break;
                }
                default:
                {
                    ConsumeInput(arg);
                    _inputState = InputState.InputDefault; // reset input state to default
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Consumes an input type based on the parser state.
    /// </summary>
    /// <param name="arg"></param>
    /// <exception cref="ArgumentException">Invalid argument was passed to the program.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Invalid parser state was entered.</exception>
    private void ConsumeInput(string arg)
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
                ProductsPath = arg;
                return;
            case InputState.InputSuppliersPath:
                SuppliersPath = arg;
                return;
            case InputState.InputOutputPath:
                OutputPath = arg;
                return;
            default:
                // this **should** never happen.
                throw new ArgumentOutOfRangeException(arg, "Invalid parser state.");
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
