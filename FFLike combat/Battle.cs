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

        private const int DisplayDelay = 1000;

        Random random = new Random();

        Queue btQueue = new Queue();

        Team team = new Team();

        Control control = new Control();

        public void Attack(Unit attacker, Unit defender)
        {
            Console.WriteLine($"{attacker.Name} attacks {defender.Name}!");
            Thread.Sleep(DisplayDelay);
            var prevHp = defender.Health;
            var roll = random.Next(1, 21);
            defender.TakeDamage(attacker.AttackDamage(roll));
            Console.WriteLine(
                $"{defender.Name} takes {defender.CalcDamage(attacker.AttackDamage(roll))} " +
                $"damage! HP {prevHp}->{defender.Health}");
            Thread.Sleep(DisplayDelay);
            if (defender.IsDead)
            {
                Console.WriteLine($"{defender.Name} died!");
                btQueue.Kill(team, defender);
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
                Console.WriteLine($"{attacker.Name}'s turn");
                Thread.Sleep(DisplayDelay);
                if (team.Ally.Contains(attacker))
                {
                    defender = control.Input(team);
                }
                else if (team.Enemy.Contains(attacker))
                {
                    defender = team.Ally[random.Next(0, team.Ally.Count - 1)];
                }
                Attack(attacker, defender);

            }
        }

        public void Play()
        {
            string Message = "TBD";
            team.AddAlly(new Unit("Bob", 3));
            team.AddAlly(new Unit("Lucy", 3));
            for (int i = 0; i < 3; i++)
            {
                team.AddEnemy(new Unit());
            }
            ToQueue();
            Thread.Sleep(2000);
            while (team.Ally.Count > 0 && team.Enemy.Count > 0)
            {
                Turn();
            }
            if (team.Ally.Count > 0)
            {
                Message = "You won!";
            }
            else if (team.Enemy.Count > 0)
            {
                Message = "You lost...";
            }
            Console.WriteLine($"** Game over! {Message} **");
            Console.ReadLine();
        }
        public void ToQueue()
        {
            foreach (var unit in team.AllUnits.OrderBy(u => u.Initiative))
            {
                btQueue.Add(unit);
            }
        }
    }
}
