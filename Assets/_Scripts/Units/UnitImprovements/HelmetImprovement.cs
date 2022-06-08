using System.Linq;
using UnityEngine;

public class HelmetImprovement : UnitImprove
{

    private void Awake()
    {
        this.Unit = gameObject.GetComponent<Unit>();
        OnInit();
        improvement = Resources.LoadAll<ScriptableImprovement>("ScriptableImprovements/Helmet").First();
        improvementPrefab = improvement.ImprovementPrefab;
        var stats = Unit.Stats;
        stats.Defence += improvement.GetDefence;

        Stats = stats;
    }


}