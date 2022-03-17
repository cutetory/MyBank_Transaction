using System;

namespace BNK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var account = new BankAccount("junyeong", 100000000);
            account.Owner = "준영";
            Console.WriteLine($"계좌(Account) {account.Number}은(는) {account.Owner}이(가) \\{account.Balance}으로 개설함. ");

            //var account2 = new BankAccount("소정", 10000000);
            //Console.WriteLine($"계좌(Account) {account2.Number}은(는) {account2.Owner}이(가) \\{account2.Balance}으로 개설함. ");
            account.MakeWithdrawal(1000000, DateTime.Now, "노트북");
            account.MakeWithdrawal(500000, DateTime.Now, "Xbox Two");


            // Test that the initial balances must be positive.
            //try catch는 실제로 반영되지는 않고 테스트만 한다.
            BankAccount invalidAccount;
            try
            {
                invalidAccount = new BankAccount("invalid", 500);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Exception caught 금액이 음수");
                Console.WriteLine(e.ToString());
                return;
            }

            Console.WriteLine(account.GetAccountHistory());
            Console.WriteLine(account.Balance);
        }
    }
}
