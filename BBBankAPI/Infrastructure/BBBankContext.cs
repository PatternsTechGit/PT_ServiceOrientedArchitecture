using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class BBBankContext : DbContext
    {
        public BBBankContext()
        {
            this.Accounts = new List<Account>();
            var tomTransactions = new List<Transaction>();
            tomTransactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = 3000M,
                TransactionDate = DateTime.Now.AddDays(1),
                TransactionType = TransactionType.Deposit
            });
            tomTransactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = -500M,
                TransactionDate = DateTime.Now.AddYears(-1),
                TransactionType = TransactionType.Withdraw
            });
            tomTransactions.Add(new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                TransactionAmount = 1000M,
                TransactionDate = DateTime.Now.AddYears(-2),
                TransactionType = TransactionType.Deposit
            });
            this.Accounts.Add(new Account
            {
                Id = "37846734-172e-4149-8cec-6f43d1eb3f60",
                AccountNumber = "0001-1001",
                AccountTitle = "Tom Hanks",
                CurrentBalance = 6000M,
                AccountStatus = AccountStatus.Active,
                Transactions = tomTransactions
            }); 
        }
        public List<Account> Accounts { get; set; }
        //public DbSet<Account> Accounts { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }
    }
}
