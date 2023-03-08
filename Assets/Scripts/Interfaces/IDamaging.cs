using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDamaging
{
    public Unit From { get; set; }
    public int Damage { get; set; }
    public void HitEnemy(Unit from, Unit to, int damage);
}
