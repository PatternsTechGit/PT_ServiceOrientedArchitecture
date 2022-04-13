using Entities;
using Entities.Responses;
using Infrastructure;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BBBankContext _bbBankContext;
        public TransactionService(BBBankContext BBBankContext)
        {
            _bbBankContext = BBBankContext;
        }
        public async Task<LineGraphData> GetLast3MonthBalances(string? accountId)
        {
            var lineGraphData = new LineGraphData();

            var allTransactions = new List<Transaction>();
            if (accountId == null)
            {
                allTransactions = _bbBankContext.Transactions.ToList();
            }
            else
            {
                allTransactions = _bbBankContext.Transactions.Where(x => x.Account.Id == accountId).ToList();
            }
            if (allTransactions.Count() > 0)
            {
                var totalBalance = allTransactions.Sum(x => x.TransactionAmount);
                lineGraphData.TotalBalance = totalBalance;
                decimal lastMonthTotal = 0;
                for (int i = 3; i > 0; i--)
                {

                    var runningTotal = allTransactions.Where(x => x.TransactionDate >= DateTime.Now.AddMonths(-i) &&
                       x.TransactionDate < DateTime.Now.AddMonths(-i + 1)).Sum(y => y.TransactionAmount) + lastMonthTotal;
                    lineGraphData.Labels.Add(DateTime.Now.AddMonths(-i + 1).ToString("MMM yyyy"));
                    lineGraphData.Figures.Add(runningTotal);
                    lastMonthTotal = runningTotal;
                }
            }
            return lineGraphData;
        }
    }
}