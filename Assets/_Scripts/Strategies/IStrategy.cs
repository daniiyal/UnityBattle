using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IStrategy
{
    public List<GameObject> spawnUnits(IArmy army);
    public List<MeleeOpponents> MeleeAttackOpponentQueue(IArmy FirstArmy, IArmy SecondArmy);
    public List<GameObject> GetUnitsWithSpecialAbility(IArmy FirstArmy, IArmy SecondArmy);
    public void Reorganize(IArmy Army);
}
