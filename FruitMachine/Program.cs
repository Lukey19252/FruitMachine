using System;

namespace FruitMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            string Name = "Machine One";
            string[] Symbols = new string[] { "A", "B", "C", "D", "E", "F"};
            int Cost = 9;

            FruitMachine MachineOne = new FruitMachine(Name, Symbols, Cost);
            int Coins = 100;
            Coins = MachineOne.UseMachine(Coins);
        }
    }
}
