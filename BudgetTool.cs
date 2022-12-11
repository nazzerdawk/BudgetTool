Budget budget = new Budget(0);
Program();



void Program()
{
    string? inputraw = "";
    string input = "";
    Console.WriteLine("Hello, welcome to my budgeting tool.");
    while (true)
    {
        Console.WriteLine("Please input a commmand");
        inputraw = Console.ReadLine();
        if (String.IsNullOrEmpty(inputraw))
        {
            inputraw = "undefined";
        }
        input = inputraw.ToLower();
        if (input != "undefined")
        {
            Command command = BuildCommand(input);
            DoCommand(command);
        }

    }

}
        

Command BuildCommand(string input){
    string verb = "undefined";
    string noun = "undefined";
    string[] commandRaw = {""};
    
    //single word command, such as "help"
    if (!input.Contains(' '))
    {
        verb = input;
        return new Command(verb);
    }

    commandRaw = input.Split(" ");

    //command contains arguments
    if (commandRaw.Length > 2)
    {
        verb = commandRaw[0];
        noun = commandRaw[1];
        List<string> arguments = new List<string>(commandRaw);
        arguments.RemoveAt(0);
        arguments.RemoveAt(0);
        return new Command(verb,noun,arguments);
    }

    //command is just a verb and a noun
    verb = commandRaw[0];
    noun = commandRaw[1];
    return new Command(verb,noun);

}

void DoCommand(Command command){
    string instruction = command.Instruction;
    List<string> arguments = command.Arguments;
    switch (instruction){
        case "show budget": budget.ShowBudget(arguments);break;
        case "set budget": budget.SetBudget(arguments);break;
        case "add expense": budget.AddExpense(arguments);break;
        case "add income": budget.AddIncome(arguments);break;
        case "reset": budget.Reset(arguments);break;
        case "help": budget.ShowHelp();break;
        case "exit": budget.Exit();break;
        default: budget.InvalidCommand(); break;
    }
}
    
public class Command{
    public string Instruction { get; set; }
    public List<string> Arguments { get; set; }
    
    public Command(string verb, string noun, List<string> arguments){
        verb = verb.Trim();
        noun = noun.Trim();
        
        for (int i=0;i < arguments.Count(); i++)
        {
            arguments[i] = arguments[i].Trim();
        }
        Instruction = verb + " " + noun;
        Arguments = arguments;
    }
    public Command(string verb, string noun){
        verb = verb.Trim();
        noun = noun.Trim();
        Instruction = verb + " " + noun;
        Arguments = new List<string>();

    }
    public Command(string verb){
        Instruction = verb;
        Arguments = new List<string>();
    }
}




public class Budget
{
    public Budget(int total)
    {
        BudgetedAmount = total;
    }
    public int BudgetedAmount { get; set; }
    private int _remainingAmount = 0;
    public int RemainingAmount
    {
        get
        {
            _remainingAmount = UpdateTotals();
            return _remainingAmount;
        }
    }
    public List<int> transactions = new List<int>();
    public int UpdateTotals()
    {
        int transactionsTotal = 0;
        foreach (int t in transactions)
        {
            transactionsTotal += t;
        }
        return this.BudgetedAmount - transactionsTotal;

    }
    public void ShowBudget(List<string> arguments)
    {
        Console.WriteLine("Budgeted Amount: {0} \nRemaining: {1}",BudgetedAmount,RemainingAmount);
    }

    public void SetBudget(List<string> arguments)
    {
        if (arguments.Count() == 0){
            SetBudget();
        }
        else
        {
            string argument0 = arguments[0];
            int budgetedAmount = 0;
            bool success = int.TryParse(argument0,out budgetedAmount);
            if (success)
                this.BudgetedAmount = budgetedAmount;
            else
            Console.WriteLine("Error: Unable to determine value of \"{0}\"",argument0); 
        }
    }
    public void SetBudget()
    {
        Console.WriteLine("No value given. Please try again.");
    }
    public void AddExpense(List<string> arguments)
    {
        if (arguments.Count == 1){
                this.transactions.Add(Math.Abs(Convert.ToInt32(arguments[0])));
            }
    }
    public void AddIncome(List<string> arguments)
    {
        if (arguments.Count == 1){
                this.transactions.Add(-1*Math.Abs(Convert.ToInt32(arguments[0])));
            }
    }
    public bool Reset(List<string> arguments)
    {
        Console.WriteLine("Invalid Input. Try again.");
        return false;
    }
    public bool Reset()
    {
        Console.WriteLine("Are you sure? Yes to reset budget and expenses, No to return.");
        string? input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
            input = "invalid";
        if (input == "invalid"){
            Console.WriteLine("Invalid Input. Try again.");
            return false;
        }
        input = input.ToLower();
        if (input == "yes" || input == "y"){
            Console.WriteLine("Resetting all values...");
            Console.WriteLine("Press any key to continue.");
            this.BudgetedAmount = 0; 
            this.transactions.Clear();
            Console.ReadKey();
            return true;
        }
        if (input == "no" || input == "n"){
            Console.WriteLine("Cancelled Reset Operation.");
            return false;
        }
        return false;

    }
    public void ShowHelp(List<string> arguments)
    {
        
    }
    public void ShowHelp()
    {
        WriteHorizontalLine();
        Console.WriteLine("List of Available Commands");
        WriteHorizontalLine();
        Console.WriteLine("show budget");
        Console.WriteLine("set budget");
        Console.WriteLine("add expense");
        Console.WriteLine("add income");
        Console.WriteLine("reset");
        Console.WriteLine("help");
        Console.WriteLine("exit");
        WriteHorizontalLine();
        Console.WriteLine("");
    }
    public bool Exit(List<string> arguments){
        Console.WriteLine("Invalid Input. Try again.");
        return false;
    }
    public bool Exit()
    {
        Console.WriteLine("Are you sure you wish to exit?");
        string? input = Console.ReadLine();
        if (string.IsNullOrEmpty(input))
            input = "invalid";
        if (input == "invalid"){
            Console.WriteLine("Invalid Input. Try again.");
            return false;
        }
        input = input.ToLower();
        if (input == "yes" || input == "y"){
            Console.WriteLine("Preparing to exit program...");
            Console.WriteLine("Press any key to continue.");
            Environment.Exit(0);
            return true;
        }
        if (input == "no" || input == "n"){
            Console.WriteLine("Cancelled Exit Operation.");
            return false;
        }
        return false;

    }
    public void InvalidCommand(){
        Console.WriteLine("Sorry, that appears to be an invalid command.");
    }
    public static void WriteHorizontalLine(){
        Console.WriteLine("_______________________");
    }
}

