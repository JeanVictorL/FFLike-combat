using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace FFLike_combat
{
    class Battle
    {
        public int TurnNumber { get; set; }
        public bool Finished { get; set; } = false;
        public string Winner { get; set; }
        private const int DisplayDelay = 0;

        public List<Unit> Units { get; set; } = new List<Unit>();

        Random random = new Random();

        Queue btQueue = new Queue();

        string[] Names = { "Templar", "Fighter", "Monk", "Rogue", "Barbarian", "Ninja" };

        public void AddUnit(Unit unit)
        {
            Units.Add(unit);
        }

        public void Attack(Unit attacker, Unit defender)
        {
            Console.WriteLine($"{attacker.Name} attacks {defender.Name}!");
            Thread.Sleep(DisplayDelay);
            var prevHp = defender.Health;
            var roll = random.Next(1, 21);
            defender.TakeDamage(attacker.AttackDamage(roll));
            Console.WriteLine(
                $"{defender.Name} takes {defender.CalcDamage(attacker.AttackDamage(roll))} damage! HP {prevHp}->{defender.Health}");
            Thread.Sleep(DisplayDelay);
            if (defender.IsDead)
            {
                Console.WriteLine($"{defender.Name} died!");
                btQueue.Units.Remove(defender);
                Thread.Sleep(DisplayDelay);
            }
        }

        public void Turn()
        {
            TurnNumber++;
            Console.WriteLine($"[ TURN {TurnNumber} ]");
            Thread.Sleep(DisplayDelay);
            for (int i = 0; i < btQueue.Units.Count; i++)
            {
                var attacker = btQueue.Pop();
                var otherUnits = btQueue.Units.Where(u => u != attacker).ToList();
                var defender = otherUnits[random.Next(0, otherUnits.Count - 1)];
                Attack(attacker, defender);
            }
        }   

        public void Play()
        {
            for (int i = 0; i < 4; i++)
            {
                AddUnit(new Unit(Names[random.Next(0, Names.Length-1)]));
                //AddUnit(new Unit($"Unit{i+1}"));
            }
            foreach (var unit in Units.OrderBy(u=>u.Initiative)) 
            {
                btQueue.Add(unit);
            }
            Thread.Sleep(2000);
            while (!Finished)
            {
                if (btQueue.Units.Count > 1)
                {
                    Turn();
                }
                else
                {
                    Finished = true;
                    Winner = btQueue.Units[0].Name;
                }
            }

            Console.WriteLine($"** Game over! {Winner} wins! **");
            Console.ReadLine();
        }
    }
}
