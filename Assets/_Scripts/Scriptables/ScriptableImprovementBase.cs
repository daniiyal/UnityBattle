using System;
using UnityEngine;

public class ScriptableImprovementBase : ScriptableObject
{
    [SerializeField] private ImprovementStats _stats;

    public GameObject ImprovementPrefab;

    public int GetDefence { get { return _stats.Defence; } }
    public int GetAttackPower { get { return _stats.AttackPower; } }
}

[Serializable]
public struct ImprovementStats
{
    public int Defence;
    public int AttackPower;
}
