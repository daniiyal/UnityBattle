using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Improvement Example")]
public class ScriptableImprovement : ScriptableImprovementBase
{
    public ImprovementType ImprovementType;
}


[Serializable]
public enum ImprovementType
{
    Spear,
    Shield,
    Helmet
}
