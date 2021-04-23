using System;
using System.Collections.Generic;
using System.Text;

namespace FruitMachine
{
    class FruitMachine
    {

        private string[] _Symbols;

        private int _Cost;

        private int[] _PrizeMoney;

        private int[] _SymbolChance;

        private string _Name;

        public FruitMachine(string Name, string[] Symbols, int Cost)
        {
            int[] SymbolChance = new int[] { 20, 20, 70, 70, 100, 100 };
            int[] PrizeMoney = CreatePrizes(Cost);
            Createmachine(Name, Symbols, SymbolChance, Cost, PrizeMoney);
        }

        public FruitMachine(string Name, string[] Symbols, int Cost, int[] PrizeMoney)
        {
            int[] SymbolChance = new int[] { 5, 5, 15, 15, 30, 30 };
            Createmachine(Name, Symbols, SymbolChance, Cost, PrizeMoney);
        }

        public FruitMachine(string Name, string[] Symbols, int[] SymbolChance, int Cost)
        {
            int[] PrizeMoney = CreatePrizes(Cost);
            Createmachine(Name, Symbols, SymbolChance, Cost, PrizeMoney);
        }

        public FruitMachine(string Name, string[] Symbols, int[] SymbolChance, int Cost, int[] PrizeMoney)
        {
            Createmachine(Name, Symbols, SymbolChance, Cost, PrizeMoney);
        }

        public int UseMachine(int Coins)
        {
            string[] Grid = new string[9];

            while (true)
            {

                Console.WriteLine($"**********{_Name}**********");
                Console.WriteLine($"Coins: {Coins}\nEnter 'Spin' to play ({_Cost} Coins)\n" +
                    $"Enter 'Winnings' to collect coins\nEnter 'Help' to view the help screen\n");
                string UserInput = Console.ReadLine();
                Console.Clear();

                if (UserInput.ToLower() == "spin")   //Spin
                {
                    if (Coins > 6)
                    {

                        Coins -= 6;
                        Spin(Grid);
                        DisplayGrid(Grid);
                        for (int i = 0; i < _Symbols.Length; i++)
                        {
                            Coins += CheckWin(Grid, i);
                        }
                        Console.WriteLine("\n");


                    }
                    else
                    {
                        Console.WriteLine("You don't have enough coins.");
                    }
                }
                else if (UserInput.ToLower() == "winnings")     //Winnings
                {
                    Console.WriteLine($"Thank you for playing have now have {Coins} coins.");
                    break;
                }
                else if (UserInput.ToLower() == "help")
                {
                    Help();
                }

            }

            return Coins;
        }

        private int[] CreatePrizes(int Cost)
        {
            int Prize1 = Cost * 8;
            int Prize2 = Cost * 4;
            int Prize3 = Cost * 2;
            return new int[] { Prize1, Prize1, Prize2, Prize2, Prize3, Prize3 };
        }

        private void Createmachine(string Name, string[] Symbols, int[] SymbolChance, int Cost, int[] PrizeMoney)
        {
            _Name = Name;
            _Symbols = Symbols;
            _SymbolChance = SymbolChance;
            _Cost = Cost;
            _PrizeMoney = PrizeMoney;

        }

        private void Spin(string[] Grid)
        {
            for (int i = 0; i < Grid.Length; i++)
            {

                Grid[i] = SymbolChosen();
            }

        }

        private string SymbolChosen()
        {
            Random R = new Random();
            int Index = R.Next(100);
            int RNum = R.Next(50);
            string Symbol;
            

            for (int i = 0; i < _Symbols.Length; i+=2)
            {
                if(Index <= _SymbolChance[i])
                {
                    if(RNum < 25)
                    {
                        return _Symbols[i];
                    }
                    return _Symbols[i + 1];                   

                    
                }
            }

            return _Symbols[_Symbols.Length - 1];
        }

        private void DisplayGrid(string[] Grid)
        {
            Console.WriteLine($"{Grid[0]}   {Grid[1]}   {Grid[2]}\n{Grid[3]}   {Grid[4]}   {Grid[5]}\n" +
                $"{Grid[6]}   {Grid[7]}   {Grid[8]}");
        }

        private int CheckWin(string[] Grid,int SymbolIndex)
        {
            int TimesWon = 0;
            string Symbol = _Symbols[SymbolIndex];

            int j = 3;
            int k = 6;
            for (int i = 0; i < 3; i++)
            {
                if(Grid[i] == Symbol && Grid[j] == Symbol && Grid[k] == Symbol)
                {
                    TimesWon++;
                }

                j++;
                k++;

            }

            j = 1;
            k = 2;
            for (int i = 0; i < 6; i+=3)
            {
                if (Grid[i] == Symbol && Grid[j] == Symbol && Grid[k] == Symbol)
                {
                    TimesWon++;
                }

                j+=3;
                k+=3;
            }

            j = 4;
            k = 8;
            for (int i = 0; i < 2; i+=2)
            {
                if (Grid[i] == Symbol && Grid[j] == Symbol && Grid[k] == Symbol)
                {
                    TimesWon++;
                }

                
                k-=2;
            }

            return TimesWon * _PrizeMoney[SymbolIndex];
        }

        private void Help()
        {
            Console.WriteLine("**********Help**********");
            Console.WriteLine($"It costs {_Cost} coins to spin.");
            Console.WriteLine("If you get three symbols in a row then your earn coins.\n");
            for (int i = _Symbols.Length-1; i >= 0; i--)
            {
                Console.WriteLine($"{_Symbols[i]} x3 = {_PrizeMoney[i]}");
            }
            Console.WriteLine("");
        }


    }
}
