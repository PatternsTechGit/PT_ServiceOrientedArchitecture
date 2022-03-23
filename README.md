# PT - Service Oriented Architecture

There are different design pattens to architect monolithic APIs few of famous patterns are;
- Service Oriented Architecture (SOA) 
- Command Query Responsibility Segregation (CQRS)
- Domain Driven Design (DDD)

In this project we are going to use Service Oriented Architecture (SOA)

## Step 1: Create Repository for API Code
To create the repository. See ...

## Step 2: Initiating .NET Project
Create Empty ASP.NET Core API Project using .NET 6.

![image](https://user-images.githubusercontent.com/100778209/159008965-44adcb56-913f-4ca3-a45c-3f6f69f7b2d2.png)

## Step 3: Project Strtucturing
There are going to be essentially four (4) types of projects in our application 

- API Project : Controllers & Middlewares (already created through scaffolding)
- Entities : Will contain database models along with Requests and Repsonse models
- Infrastructure: Will contain Data Access logic, data seeding configuragtions and some data access design patterns like Repository DP and UOW
- Services : Maintaining concept of Speration of Concern (SOC) this layer will contain the business logic of the application distributes in Services as per their purpose.

Create .Net class librayr projects for Entities, Infrasturture, and Services
Delete Class1.cs files created by default.

## Step 4: Entities Project 
Then we will create folder for requests and Response models.

![image](https://user-images.githubusercontent.com/100778209/159009869-d2e1d096-81c5-4a50-b5e0-b35dacaeab74.png)


## Step 5: Creating Base Model
In Entities Project we will create our first model **BaseEntity** which will have an **Id** property . All other database entities will derive from this class.
```csharp
public class BaseEntity
{
        [Key] //Primary Key
        public string Id { get; set; }
}
```

## Step 6: Account Model
Next create a model to store accounts information 

```csharp
public class Account : BaseEntity // Inheriting ID to uniquly identify account in database
{
        // String that uniquely identifies the account
        public string AccountNumber { get; set; }
        
        //Title of teh account
        public string AccountTitle { get; set; }
        
        //Available Balance of the account
        public decimal CurrentBalance { get; set; }
        
        //Account's status 
        public AccountStatus AccountStatus { get; set; }
        
        // One Account might have 0 or more Transactions (1:Many relationship)
        public ICollection<Transaction> Transactions { get; set; }
}
    
// Two posible statuses of an account
public enum AccountStatus
{
        Active = 0,     // When an account can perform transactions
        InActive = 1    // When an account cannot perform transaction
}
```
## Step 7 Transaction Model
Next create a model to store Transactions related to an Account

```csharp
public class Transaction : BaseEntity // Inheriting ID to uniquly identify account in database
{
        //Transaction type
        public TransactionType TransactionType { get; set; }
        
        //When transaction was recorded
        public DateTime TransactionDate { get; set; }
        
        //Amount of transaction
        public decimal TransactionAmount { get; set; }
        
        //Associcated acocunt of that transaction
        public Account Account { get; set; }
}
    
// Two posible types of an Trasaction
public enum TransactionType
{
        Deposit = 0,    // When money is added or pay in  
        Withdraw = 1    // When money is subtracted or with drawn
}
```
After these steps the over all project sturcture should look like as follows

![image](https://user-images.githubusercontent.com/100778209/159010704-a4bbc361-30fd-494f-8ddb-083ab03eb22e.png)

## Step 8: Adding Database Context (DbContext)

Accessing real database and creating seed data is beyond scope of this exersise. So we will create a custom database conetxt (DbContext) with some hard coded data.

Create a new C# class **BBBankContext** and drive it from  **DbContext** class. To use DbContext install a NuGet package 

```
Install-Package Microsoft.EntityFrameworkCore
```

## Step 9: Hard coding some data
In the constructor of BBBankContext we will initialize Accounts and will add some transactions to this account so we can return some data .


```csharp
public class BBBankContext : DbContext //inherting from Asp.net core's dbcontext class
{
        public BBBankContext()
        {
            this.Accounts = new List<Account>();                // intilizing empty accounts

           // intializing some transactions
            var tomTransactions = new List<Transaction>();
            tomTransactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),                 // Auto generating Id
                TransactionAmount = 3000M,                      // Transaction of 3000$
                TransactionDate = DateTime.Now.AddDays(1),      // Tranaction happed yesterday
                TransactionType = TransactionType.Deposit       // ammount was added
            });
            tomTransactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),                 // Auto generating Id
                TransactionAmount = -500M,                      // Transaction of 500$
                TransactionDate = DateTime.Now.AddYears(-1),    // Transaction happend one year ago
                TransactionType = TransactionType.Withdraw      // amount was subtracted
            });
            tomTransactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),                 // Auto generating Id
                TransactionAmount = 1000M,                      // Transaction of 100$
                TransactionDate = DateTime.Now.AddYears(-2),    // Transaction happend two year ago
                TransactionType = TransactionType.Deposit       // amount was added
            });
            this.Accounts.Add(new Account
            {
                Id = "37846734-172e-4149-8cec-6f43d1eb3f60",    // Unique GUID of the account
                AccountNumber = "0001-1001",                    // Account number
                AccountTitle = "Tom Hanks",                     // Account Title
                CurrentBalance = 3500M,                         // Account balance matches the transaction
                AccountStatus = AccountStatus.Active,           // Account status
                Transactions = tomTransactions                  // associating above transactions with the account
            }); 
        }
        public List<Account> Accounts { get; set; }
        
}

```

## Step 10: Creating Interface for Transaction Service
In **Services** project we create an interface (contract) to implement the seperation of concerns.
It will make our code testable and injectable as a dependency.

```csharp
public interface ITransactionService
{
        /// <summary>
        /// Generating dataset for last three years to display on Graph
        /// </summary>
        /// <param name="accountId">Unique Account Id</param>
        /// <returns>Data in object of LineGraphData</returns>
        Task<LineGraphData> GetLastThreeYearsBalancesById(string accountId);
}
```

## Step 11: Creating Transaction Service Implementation
In **Services** project we will create an implementation for our transaction service.

In this class we will be implementing **ITransactionService** interface.

```csharp
public async Task<LineGraphData> GetLastThreeYearsBalancesById(string accountId)
{
            // Object to contain the line graph data
            var lineGraphData = new LineGraphData();
            
            // Filter the bank account based on account id
            var account = _bbBankContext.Accounts.FirstOrDefault(x => x.Id == accountId);

            // Check whether the account exists
            if (account != null)
            {
                // Calculate the total balance till now
                var totalBalance = account.Transactions.Sum(x => x.TransactionAmount);
                lineGraphData.TotalBalance = totalBalance;

                // Calculate the total balance for last two years
                var twoYearOldSum = account.Transactions.Where(
                        x => x.TransactionDate >= DateTime.Now.AddYears(-3) &&
                        x.TransactionDate < DateTime.Now.AddYears(-2)).Sum(y => y.TransactionAmount);
                lineGraphData.Labels.Add(DateTime.Now.AddYears(-2).ToString("yyyy"));
                lineGraphData.Figures.Add(twoYearOldSum);

                // Calculate the total balance for last one year
                var oneYearOldSum = account.Transactions.Where(
                        x => x.TransactionDate >= DateTime.Now.AddYears(-2) &&
                        x.TransactionDate < DateTime.Now.AddYears(-1)).Sum(y => y.TransactionAmount) + twoYearOldSum;
                lineGraphData.Labels.Add(DateTime.Now.AddYears(-1).ToString("yyyy"));
                lineGraphData.Figures.Add(oneYearOldSum);

                // Calculate the total balance of current year 
                var thisYearSum = account.Transactions.Where(
                        x => x.TransactionDate >= DateTime.Now.AddYears(-1)).Sum(y => y.TransactionAmount) + oneYearOldSum;
                lineGraphData.Labels.Add(DateTime.Now.ToString("yyyy"));
                lineGraphData.Figures.Add(thisYearSum);

            }
            
            // returning the line graph data object
            return lineGraphData;
}
```

## Step 12: Creating Transaction API

Create a new API Controller named **TransactionController** and inject the **ITransactionService** using the constructor. 

```csharp
private readonly ITransactionService _transactionService;
public TransactionController(ITransactionService transactionService)
{
    _transactionService = transactionService;
}
```

Now we will create a method **GetLastThreeYearsBalancesById** to get last three years data for generating graph.

```csharp
[HttpGet]
[Route("GetLastThreeYearsBalancesById/{accountId}")]
public async Task<ActionResult> GetLastThreeYearsBalancesById(string accountId)
{
    try
    {
        // return OK status code along with LineGraphData object
        return new OkObjectResult(await _transactionService.GetLastThreeYearsBalancesById(accountId));
    }
    catch (Exception ex)
    {
        // return Bad Request status code along with exception data
        return new BadRequestObjectResult(ex); 
    }
}
```

