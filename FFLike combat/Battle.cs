using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FFLike_combat
{
    class Battle
    {
        public int TurnNumber { get; set; }
        public bool Finished { get; set; } = false;
        public string Winner { get; set; }
        private const int DisplayDelay = 0;

        public Random random = new Random();

        public List<Unit> Units { get; set; }

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
                defender.Initiative = 0;
                Thread.Sleep(DisplayDelay);
                Winner = attacker.Name;
                Finished = true;
            }
        }

        public void TurnOrder()
        {
            
        }

        public void Turn()
        {
            TurnNumber++;
            Console.WriteLine($"[ TURN {TurnNumber} ]");
            Thread.Sleep(DisplayDelay);
            
        }

        public void Play()
        {
            AddUnit(new Unit());
            AddUnit(new Unit());
            Thread.Sleep(2000);
            while (!Finished)
            {
                Turn();
            }

            Console.WriteLine($"** Game over! {Winner} wins! **");
            Console.ReadLine();
        }
    }
}
