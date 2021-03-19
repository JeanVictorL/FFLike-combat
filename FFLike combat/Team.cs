using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FFLike_combat
{
    class Team
    {
        public List<Unit> Ally { get; set; } = new List<Unit>();
        public List<Unit> Enemy { get; set; } = new List<Unit>();
        public List<Unit> AllUnits { get; set; } = new List<Unit>();

        public void AddEnemy(Unit unit)
        {
            Enemy.Add(unit);
            AllUnits.Add(unit);
        }

        public void AddAlly(Unit unit)
        {
            Ally.Add(unit);
            AllUnits.Add(unit);
        }

    }
}
