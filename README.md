# PT - Service Oriented Architecture

## CQRS
CQRS stands for Command and Query Responsibility Segregation, a pattern that separates read and update operations for a data store. Read more [details](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs#:~:text=CQRS%20stands%20for%20Command%20and,operations%20for%20a%20data%20store).

## DDD
Domain-driven design is a software design approach focusing on modelling software to match a domain according to input from that domain's experts. In terms of OOP it means that the structure and language of software code should match the business domain. Read more [details](https://martinfowler.com/tags/domain%20driven%20design.html)

## SOA
Service-oriented architecture (SOA) is a software development model that allows services to communicate across different platforms and languages. It defines a way to make software components reusable and interoperable via service interfaces. Read more [details](https://www.ibm.com/cloud/learn/soa#:~:text=What%20is%20SOA%2C%20or%20service,rapidly%20incorporated%20into%20new%20applications).


There are different design pattens to architect monolithic APIs few of famous patterns are;
- Service Oriented Architecture 
- 
- 
- 

In this project we are going to use Service Oriented Architecture

## Step 1: Create Repositry for API Code
To create .. see

## Step 2: Initiating .net project
Create Empty Asp.net core Api project using .net 6. 
<image> project name "BBBankAPI"

## Step 3: Project strtucturing
there are going to be essentially 4 types of projects in our application 

. Api Project : Controllers & Middlewares (already created)
. Entities : Will contain datrabase models along with Requests and Repsonse models
. Infrastructure: Will contain Data Access logic, data seeding configuragtions and some data access design patterns like Repository DP and UOW,
. Servoces : Maintaing concept of Speration of Concern this layer will contain the business logic of the application distributes in Services as per their purpose.

Create .Net class librayr projects for Entities, Infrasturture, and Services
Delete Class1.cs files created by default.

## Step 4: Enties Project 
Then we will create folder for requests and Response models
<image>


## Step 5: Creating Base Model
In enttie sproject First model will be BaseEntity which will have an ID property . All other database entities will derive from this class.

public class BaseEntity
{
        [Key] //Comment about Key
        public string Id { get; set; }
}


## Step 6: Account Model
Next create a model to store accounts information 

  public class Account : BaseEntity //Inheriting ID to uniquly identify account in database
    {
        // String that uniquely identifies the account
        public string AccountNumber { get; set; }
        //Tittle of teh account
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
        Active = 0, //When account can perform transactions
        InActive = 1 // When account cannot perform transaction
    }

## Step 7 Transaction Model
Next create a model to store Transactions related to an Account

 public class Transaction : BaseEntity // 
    {
//Treansaction type
        public TransactionType TransactionType { get; set; }
         //When transaction was recorded
        public DateTime TransactionDate { get; set; }
       //Amount of transaction
        public decimal TransactionAmount { get; set; }
       // Associcated acocunt of that transaction
        public Account Account { get; set; }
    }
    // Two posible types of an Trasaction
    public enum TransactionType
    {
        Deposit = 0,  //When money is added  
        Withdraw = 1    //When money is subtracted 
    }

after these steps the over all project sturcture should look like this 
<expanded image>

## Step 8: Adding DB Context. 

accessing real database and creating seed data is beyond scope of this exersise. so create a custom db conetxt with some hard coded data.

create a c# class called "BBBankContext" and drive it from  DbContext class. To use DbContext install a nugect package 

Install-Package Microsoft.EntityFrameworkCore

## Step 9: Hard coding some data
in the constructor of BBBankContext we will initialize Accounts and will add some transactions to this account  so we can return some data .



    public class BBBankContext : DbContext //inherting from Asp.net core's dbcontext class
    {
        public BBBankContext()
        {
            this.Accounts = new List<Account>(); // intilizing emoty accounts

           / intilizing some transactions
            var tomTransactions = new List<Transaction>();
            tomTransactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(), // Auto generating Id
                TransactionAmount = 3000M, //Transaction of 3000$
                TransactionDate = DateTime.Now.AddDays(1), // Tranaction happed yesterday
                TransactionType = TransactionType.Deposit  // ammount was added
            });
            tomTransactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(), // Auto generating Id
                TransactionAmount = -500M, //Transaction of 500$
                TransactionDate = DateTime.Now.AddYears(-1), // Transaction happend one year ago
                TransactionType = TransactionType.Withdraw  // amount was subtracted
            });
            tomTransactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = 1000M,
                TransactionDate = DateTime.Now.AddYears(-2), // Transaction happend two year ago
                TransactionType = TransactionType.Deposit  // amount was added
            });
            this.Accounts.Add(new Account
            {
                Id = "37846734-172e-4149-8cec-6f43d1eb3f60", //Unique GUID of the account
                AccountNumber = "0001-1001", //account number
                AccountTitle = "Tom Hanks", //Account Title
                CurrentBalance = 3500M,  //Account balance matches the transaction
                AccountStatus = AccountStatus.Active, // Accoutn status
                Transactions = tomTransactions // associating above transactions with the account
            }); 
        }
        public List<Account> Accounts { get; set; }
          // When working with real database we will seed data using DbSet
        //public DbSet<Account> Accounts { get; set; }  
        //public DbSet<Transaction> Transactions { get; set; }
    }


http://localhost:5070/api/Transaction/GetLastThreeYearsBalancesById/37846734-172e-4149-8cec-6f43d1eb3f60
