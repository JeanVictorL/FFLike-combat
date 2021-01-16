using System;
using System.Collections.Generic;
using System.Text;

namespace FFLike_combat
{
    class Unit
    {
        //most of these should only be accessed for reference but never changed
        //do some logic to make not changeable later
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public float PhDamage { get; set; }
        public float PhDefence { get; set; }
        public float Mana { get; set; }
        public int Initiative { get; set; }
        public bool IsDead => Health <= 0;

        public Unit(string name = "Generic", int level = 1)
        {
            Name = name;
            Level = level;
            SetStats();
            Initiative = new Random().Next(1, 21);
        }

        public void SetStats()
        {
            Health = 100 + (20 * (Level - 1));
            PhDamage = 20 + (5 * (Level - 1));
            PhDefence = 10 + (2 * (Level - 1));
            Mana = 100 + (10 * (Level - 1));
        }

        public float AttackDamage(int roll)
        {
            switch (roll)
            {
                case 1:
                    return 0;
                case 20:
                    return PhDamage * 2;
                default:
                    return PhDamage;
            }
        }

        public float CalcDamage(float rawDamage)
        {
            if (rawDamage == 0)
            {
                return 0;
            }
            else
            {
                return rawDamage - PhDefence;
            }
            
        }

        public void TakeDamage(float rawDamage)
        {
            Health -= (int)Math.Max(0, CalcDamage(rawDamage));
            if (Health < 0)
            {
                Health = 0;
            }
        }
    }
}
