using Entities;

namespace Infrastructure
{
    public class BBBankContext
    {
        public BBBankContext()
        {
            // creating the collection for user list
            this.Users = new List<User>();
            
            // initializing a new user 
            this.Users.Add(new User
            {
                Id = "b6111852-a1e8-4757-9820-70b8c20e1ff0",
                FirstName = "Ali",
                LastName = "Taj",
                Email = "malitaj-dev@outlook.com",
                ProfilePicUrl = "https://res.cloudinary.com/demo/image/upload/w_400,h_400,c_crop,g_face,r_max/w_200/lady.jpg"
            });
            
            // creating the collection for account list
            this.Accounts = new List<Account>();
            
            // initializing a new account 
            this.Accounts.Add(new Account
            {
                Id = "37846734-172e-4149-8cec-6f43d1eb3f60",
                AccountNumber = "0001-1001",
                AccountTitle = "Ali Taj",
                CurrentBalance = 3500M,
                AccountStatus = AccountStatus.Active,
                User = this.Users[0]
            });
            
            // creating the collection for transaction list
            this.Transactions = new List<Transaction>();
            
            // initializing with some transactions 
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = 1000M,
                TransactionDate = DateTime.Now,
                TransactionType = TransactionType.Deposit,
                Account = this.Accounts[0]
            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = -100M,
                TransactionDate = DateTime.Now.AddMonths(-1),
                TransactionType = TransactionType.Withdraw,
                Account = this.Accounts[0]
            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = -45M,
                TransactionDate = DateTime.Now.AddMonths(-2),
                TransactionType = TransactionType.Withdraw,
                Account = this.Accounts[0]
            });
        }
        
        public List<Transaction> Transactions { get; set; }
        public List<Account> Accounts { get; set; }
        public List<User> Users { get; set; }
    }
}
