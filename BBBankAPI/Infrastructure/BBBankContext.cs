﻿using Entities;

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
                Id = "aa45e3c9-261d-41fe-a1b0-5b4dcf79cfd3",    // Unique GUID of the User
                FirstName = "Raas",                              // FirstName
                LastName = "Masood",                               // LastName
                Email = "rassmasood@hotmail.com",              // Email ID
                ProfilePicUrl = "https://res.cloudinary.com/demo/image/upload/w_400,h_400,c_crop,g_face,r_max/w_200/lady.jpg"
            });

            // creating the collection for account list
            this.Accounts = new List<Account>();

            // initializing a new account 
            this.Accounts.Add(new Account
            {
                Id = "37846734-172e-4149-8cec-6f43d1eb3f60",
                AccountNumber = "0001-1001",
                AccountTitle = "Raas Masood",
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
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Deposit,
                Account = this.Accounts[0]
            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = -100M,
                TransactionDate = DateTime.UtcNow.AddMonths(-1),
                TransactionType = TransactionType.Withdraw,
                Account = this.Accounts[0]
            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = -45M,
                TransactionDate = DateTime.UtcNow.AddMonths(-2),
                TransactionType = TransactionType.Withdraw,
                Account = this.Accounts[0]
            });
            this.Transactions.Add(new Transaction()
            {


                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = -200M,
                TransactionDate = DateTime.UtcNow.AddMonths(-4),
                TransactionType = TransactionType.Withdraw

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = 500M,
                TransactionDate = DateTime.UtcNow.AddMonths(-5),
                TransactionType = TransactionType.Deposit

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = 200M,
                TransactionDate = DateTime.UtcNow.AddMonths(-6),
                TransactionType = TransactionType.Deposit

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = -300M,
                TransactionDate = DateTime.UtcNow.AddMonths(-7),
                TransactionType = TransactionType.Withdraw

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = -100M,
                TransactionDate = DateTime.UtcNow.AddMonths(-8),
                TransactionType = TransactionType.Withdraw

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = 200M,
                TransactionDate = DateTime.UtcNow.AddMonths(-9),
                TransactionType = TransactionType.Deposit

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = -500M,
                TransactionDate = DateTime.UtcNow.AddMonths(-10),
                TransactionType = TransactionType.Withdraw

            });
            this.Transactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                Account = this.Accounts[0],
                TransactionAmount = 900M,
                TransactionDate = DateTime.UtcNow.AddMonths(-11),
                TransactionType = TransactionType.Deposit

            });
        }

        public List<Transaction> Transactions { get; set; }
        public List<Account> Accounts { get; set; }
        public List<User> Users { get; set; }
    }
}
