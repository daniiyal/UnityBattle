using System;
using UnityEngine;


[CreateAssetMenu(fileName = "New Scriptable Example")]
public class ScriptableUnit : ScriptableUnitBase
{
    public UnitType UnitType;

}

[Serializable]
public enum UnitType
{
    Infantry,
    Archer,
    Knight,
    Healer,
    Mage,
    TumbleWeed
}