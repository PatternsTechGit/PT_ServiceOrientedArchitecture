using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Account : BaseEntity
    {
        public string AccountNumber { get; set; }
        public string AccountTitle { get; set; }
        public decimal CurrentBalance { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
    public enum AccountStatus
    {
        Active = 0,
        InActive = 1
    }
}
