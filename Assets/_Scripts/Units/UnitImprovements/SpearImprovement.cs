using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SpearImprovement : UnitImprove
{

    private void Awake()
    {
        this.Unit = gameObject.GetComponent<Unit>();
        Debug.Log(Unit);
        OnInit();

        improvement = Resources.LoadAll<ScriptableImprovement>("ScriptableImprovements/Spear").First();
        improvementPrefab = improvement.ImprovementPrefab;
        var stats = Unit.Stats;
        stats.Defence += improvement.GetDefence;

        Stats = stats;
    }


}