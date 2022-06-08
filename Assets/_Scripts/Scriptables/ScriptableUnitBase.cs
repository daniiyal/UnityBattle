using LibraryUnits;
using System;
using UnityEngine;

public class ScriptableUnitBase : ScriptableObject
{
    public Faction Faction;

    [SerializeField] private Stats _stats;

    public Stats BaseStats => _stats;

    public GameObject UnitPrefab;

    public int GetPrice { get { return _stats.Cost; } }
    public int GetHealth { get { return _stats.Health; } }
}

[Serializable]
public struct Stats
{
    public int Health;
    public int MaxHealth;
    public int Defence;
    public int AttackPower;
    public int AttackRange;
    public int Chance;
    public int TravelDistance;
    public int Cost;


}

[Serializable]
public enum Faction
{
    Left,
    Right
}