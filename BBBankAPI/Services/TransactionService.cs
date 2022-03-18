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
        public async Task<LineGraphData> GetLastThreeYearsBalancesById(string accountId)
        {
            var lineGraphData = new LineGraphData();
            
            var account = _bbBankContext.Accounts.FirstOrDefault(x => x.Id == accountId);

            if (account != null)
            {
                var totalBalance = account.Transactions.Sum(x => x.TransactionAmount);
                lineGraphData.TotalBalance = totalBalance;

                var twoYearOldSum = account.Transactions.Where(
                        x => x.TransactionDate >= DateTime.Now.AddYears(-3) &&
                        x.TransactionDate < DateTime.Now.AddYears(-2)).Sum(y => y.TransactionAmount);
                lineGraphData.Labels.Add(DateTime.Now.AddYears(-2).ToString("yyyy"));
                lineGraphData.Figures.Add(twoYearOldSum);

                var oneYearOldSum = account.Transactions.Where(
                        x => x.TransactionDate >= DateTime.Now.AddYears(-2) &&
                        x.TransactionDate < DateTime.Now.AddYears(-1)).Sum(y => y.TransactionAmount) + twoYearOldSum;
                lineGraphData.Labels.Add(DateTime.Now.AddYears(-1).ToString("yyyy"));
                lineGraphData.Figures.Add(oneYearOldSum);

                var thisYearSum = account.Transactions.Where(
                        x => x.TransactionDate >= DateTime.Now.AddYears(-1)).Sum(y => y.TransactionAmount) + oneYearOldSum;
                lineGraphData.Labels.Add(DateTime.Now.ToString("yyyy"));
                lineGraphData.Figures.Add(thisYearSum);

            }
            return lineGraphData;
        }
    }
}
