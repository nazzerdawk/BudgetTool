# BudgetTool


This will be the general outline for this project. 

In an effort to avoid feature creep and to keep the project completable, I'm planning it in 3 stages, and plan to keep the main branch "Always Shippable" 
so to speak. 

Stage 1: Basic Functionality. [Complete]
* This program will be a simple console app. 
* Budget will contain a list of Ints called Transactions, an Int called BudgetedAmount, and an Int called RemainingAmount
* There will be a Method named Update() that will call each time any other command is called. 
* Interaction will be command line based and will allow user to input following commands:
    - Show Budget - Will output the BudgetedAmount, then the transactions list, and then the remaining total. 
    - Set Budget nnnn - Updates BudgetedAmount to the specified number nnnn
    - Add Expense nnnn- Adds a negative Int to the Transactions list with value nnnn
    - Add Income nnnn - Adds a positive Int to the Transactions list with value nnnn
    - Reset - Resets all values after a confirmation dialogue
    - Help - Lists the above commands.

        
Stage 2: Expected Features 
* This will expand the budgeting tool to allow for categorization and naming of expenses. 
* This will be done by replacing the Transactions list type (Int) with an class called Transaction. 
* There will also be a list called Categories and a new class called Categories. 
* Transaction instances will contain a Name, Value, and Category. 
* New categories are added if a specified category does not already exist, or manually. 
* The budget class will have a list of categories and a list of transactions in the category
* If a new category is made, it will have its own BudgetedAmount.     
    - Show Budget - Will output the BudgetedAmount, then the transactions list, and then the remaining total. 
    - Set Budget (name) (value) (categoryname)- Updates BudgetedAmount to the specified number "value". Optional string "categoryname" can be a category name to change category instead. 
    - Add Expense (name) (value) (categoryname) - Adds a negative Int to the Transactions list with value "value". Optional string "categoryname" can be a category name to change category instead. 
    - Add Income nnnn (name) (value) (categoryname)  - Adds a positive Int to the Transactions list with value nnnn. Optional string "categoryname" can be a category name to change category instead. 
    - Add category (categoryname) - Adds a category with name "categoryname". 
    - Remove Expense 
    - Reset - Resets values, leaving categories intact but removing all transactions. 
    - Reset all - resets all values and categories. 



Stage 3: Optional Features
* This list will remain empty until stage 2 is complete. 
        