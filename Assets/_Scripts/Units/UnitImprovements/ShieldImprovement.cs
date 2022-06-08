using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ShieldImprovement : UnitImprove
{
    private void Awake()
    {

        this.Unit = gameObject.GetComponent<Unit>();
        OnInit();
        improvement = Resources.LoadAll<ScriptableImprovement>("ScriptableImprovements/Shield").First();
        improvementPrefab = improvement.ImprovementPrefab;
        var stats = Unit.Stats;
        stats.Defence += improvement.GetDefence;

        Stats = stats;
    }

}
