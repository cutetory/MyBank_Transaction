using System;
using System.Collections.Generic;
using System.Text;

namespace BNK
{
    internal class GiftCardAccount : BankAccount
    {
        private readonly decimal _monthlyDeposit = 0m;

        public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0) : base(name, initialBalance)
            => _monthlyDeposit = monthlyDeposit;

        //0이 아닌 값으로 설정된 경우 월별 예치금 추가
        public override void PerformMonthEndTransactions()
        {
            if (_monthlyDeposit != 0)
            {
                MakeDeposit(_monthlyDeposit, DateTime.Now, "Add monthly deposit");
            }
        }
    }
}
