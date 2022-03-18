using System;
using System.Collections.Generic;
using System.Text;

namespace BNK
{
    internal class BankAccount
    {
        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }
                return balance;
            }
        }

        private static int accountNumberSeed = 1234567890;

        //readonly를 주면 동일 클래스의 생성자에서만 발생 가능.
        private readonly decimal _minimumBalance;

        private List<Transaction> allTransactions = new List<Transaction>();

        //생성자는 new를 사용하여 개체를 만들 때 호출된다.
        public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0)
        {

        }
        public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
        {
            this.Owner = name;
            this.Number = accountNumberSeed.ToString();
            //MakeDeposit(initialBalance, DateTime.Now, "초기 잔액");
            accountNumberSeed++;
            _minimumBalance = minimumBalance;
            if(initialBalance > 0)
            {
                MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
            }
        }

        //입금
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "입금금액은 0원 이상이어야 합니다.");
            }
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }
        //출금
        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "출금금액은 0원 이상이어야 합니다.");
            }
            //if (Balance - amount < _minimumBalance)
            //{
            //    throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            //}
            //var withdrawal = new Transaction(-amount, date, note);
            //allTransactions.Add(withdrawal);
            Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
            //74번째 줄 new(-amount, date, note)로 되어있음.. 이유 알아낼 것!
            Transaction? withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
            if(overdraftTransaction != null)
            {
                allTransactions.Add(overdraftTransaction);
            }
        }

        public string GetAccountHistory()
        {
            var report = new StringBuilder();

            //HEADER
            report.AppendLine("Date\t\tAmmount\t\tNote");
            foreach (var item in allTransactions)
            {
                //ROWS
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount} \t{item.Notes}");
            }
            //StringBuilder를 string으로 바꾸는 것이 ToString()
            return report.ToString();
        }

        public virtual void PerformMonthEndTransactions()
        {

        }

        protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if(isOverdrawn)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            else
            {
                return default;
            }
        }
    }
}
