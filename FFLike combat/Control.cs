using System;
using System.Collections.Generic;
using System.Text;

namespace FFLike_combat
{
    class Control
    {
        private readonly string space = "----------------------------------------------------------------------------------";
        public Unit Input(Team team)
        {
            //Console.WriteLine(space);
            //Console.WriteLine("Press 1 to attack");
            //int a = ChooseOption(1, 1);
            return SelectTarget(team);
        }

        public Unit SelectTarget(Team team)
        {
            Console.WriteLine(space);
            Console.WriteLine("Choose a target to attack: ");
            Console.WriteLine();
            for (int i = 0; i < team.Enemy.Count; i++)
            {
                Console.WriteLine($"Press {i + 1} for {team.Enemy[i].Name}");
            }
            Console.WriteLine(space);

            int u = ChooseOption(1, team.Enemy.Count);
            return team.Enemy[u - 1];
        }
        
        public static int GetNumber()
        {
            int n;
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Not a number.");
            }
            return n;
        }

        public static int ChooseOption(int min, int max)
        {
            int number = GetNumber();
            while (number > max || number < min)
            {
                Console.WriteLine($"Enter a number between {min} and {max}");
                number = GetNumber();
            }
            return number;
        }
    }
}
