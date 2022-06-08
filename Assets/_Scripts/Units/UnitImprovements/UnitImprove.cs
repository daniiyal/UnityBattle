using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class UnitImprove : Unit, IImprovable
{
    public GameObject improvementPrefab;
    public ScriptableImprovement improvement;
    public IUnit Unit { get; protected set; }
    public List<ImprovementType> improvements { get; set; }
    public List<GameObject> improvementGameObjects { get; set; }

    public bool CanImprove(ImprovementType type)
    {
        foreach (var item in improvements)
        {
            if (item == type)
            {
                return false;
            }
        }

        return true;
    }
    protected void OnInit()
    {
        currentTile = Unit.currentTile;
        Animator = Unit.Animator;
        ScriptableUnit = Unit.ScriptableUnit;
    }


}