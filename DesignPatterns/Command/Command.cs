namespace Command;

public class Command {
    public class BankAccount {
        private int balance;
        private int overdraftLimit = -500;

        public void Deposit(int amount) {
            balance += amount;
            Console.WriteLine($"Deposited {amount}, balance is now {balance}");
        }

        public void Withdraw(int amount) {
            if (balance - amount >= overdraftLimit) {
                balance -= amount;
                Console.WriteLine($"Withdrew {amount}, balance is now {balance}");
            }
        }

        public override string ToString() {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public interface ICommand {
        void Call();
    }

    public class BankAccountCommand : ICommand {
        private BankAccount account;
        private Action action;
        private int amount;

        public BankAccountCommand(BankAccount account, Action action, int amount) {
            this.account = account;
            this.action = action;
            this.amount = amount;
        }

        public enum Action {
            Deposit, Withdraw
        }
        
        public void Call() {
            switch (action) {
                case Action.Deposit:
                    account.Deposit(amount);
                    break;
                case Action.Withdraw:
                    account.Withdraw(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public static void main() {
        BankAccount ba = new BankAccount();
        var commands = new List<BankAccountCommand> {
            new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100),
            new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 50)
        };
        Console.WriteLine(ba);

        foreach (var c in commands)
            c.Call();

        Console.WriteLine(ba);
    }
}