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
    /*
        We take the input, separate it into an array of substrings with Space chars as the delimiter,
        then take the first two and call them the "verb" and "noun", and pass them as arguments
        to the constructor for the Command class along with all other words in the command as
        remaining arguments.
    */ 
    string verb = "undefined";
    string noun = "undefined";
    string[] commandRaw = {""};
    
    //single word command, such as "help". Only needs a verb, the noun defaults to "undefined".
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
    //Instruction stores the verb and noun as a string, separated by a single space. 
    public string Instruction { get; set; }
    
    public List<string> Arguments { get; set; }
    
    public Command(string verb, string noun, List<string> arguments){

        //Trim ALL the stuff! No whitespace allowed! 
        verb = verb.Trim();
        noun = noun.Trim();
        for (int i=0;i < arguments.Count(); i++)
        {
            arguments[i] = arguments[i].Trim();
        }
        
        //See what I said about Instruction? Right here, dude. 
        Instruction = verb + " " + noun;
        //

        /*
            Can I just say that I kinda wish the standard naming convention for method input and output params
            were _inputParam and outputParam_ respectively? I just love the idea of a leading underscore denoting 
            input and trailing underscrore denoting output. 
        */
        Arguments = arguments;
    }
    public Command(string verb, string noun){
        //Overload for if the command is two words only. Like, Show Budget.
        verb = verb.Trim();
        noun = noun.Trim();
        Instruction = verb + " " + noun;
        Arguments = new List<string>();
    }
    public Command(string verb){
        //Lastly an overload for a single word command. Mainly just Exit, reset, and help. 
        Instruction = verb;
        Arguments = new List<string>();
    }
}




public class Budget
{
    public Budget(int total)
    {   //This constructor isn't actually used to set the total in practice. 
        //Leaving it alone for now cuz it don't break nothing. 
        BudgetedAmount = total;
    }
    public int BudgetedAmount { get; set; }
    private int _remainingAmount = 0;
    public int RemainingAmount
    { //Readonly property! attempting to read it recalculates total on an as-needed basis! 
        //I love it! Shoulda switched to C# a decade ago!

        get
        {
            _remainingAmount = UpdateTotals();
            return _remainingAmount;
        }
    }
    // important to note that transactions are signed, but user input loses sign. 
    //  Use income/expense to make pos/neg.
    public class Transaction {
        public int Amount { get; set; }
        public Transaction(int amount)
        {
            Amount = amount; 
        }

    }
    public List<Transaction> transactions = new List<Transaction>();
    public int UpdateTotals()
    {
        int transactionsTotal = 0;
        foreach (Transaction t in transactions)
        {
            transactionsTotal += t.Amount;
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
    {//Always makes input value positive
        if (arguments.Count == 1){
            Transaction expense = new Transaction(Math.Abs(Convert.ToInt32(arguments[0])));
                this.transactions.Add(expense);
            }
    }
    public void AddIncome(List<string> arguments)
    { //always makes input value negative
        if (arguments.Count == 1){
            Transaction income = new Transaction(-1*Math.Abs(Convert.ToInt32(arguments[0])));
            this.transactions.Add(income);
            }
    }
    public bool Reset(List<string> arguments)
    {
        Console.WriteLine("Invalid Input. Try again.");
        return false;
    }
    public bool Reset()
    { //Confirmation prompt handles Yes/no AND y/n, and is case insensitive! 
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
        Console.WriteLine("Invalid Input. Try again.");
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

