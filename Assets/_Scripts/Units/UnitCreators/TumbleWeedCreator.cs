using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class TumbleWeedCreator : IUnitCreator
{

    public IUnit CreateUnit(Faction faction)
    {
        ScriptableUnit[] scriptableUnits;
        if (faction == Faction.Left)
            scriptableUnits = Resources.LoadAll<ScriptableUnit>("ScriptableUnits/Externals/TumbleWeedLeft");
        else
            scriptableUnits = Resources.LoadAll<ScriptableUnit>("ScriptableUnits/Externals/TumbleWeedRight");

        ScriptableUnit tumbleWeedUnit = scriptableUnits[0];
        Debug.Log(tumbleWeedUnit);
        GameObject gameObject = new GameObject(tumbleWeedUnit.name);

        var unit = gameObject.AddComponent<TumbleWeed>();
        
        unit.ScriptableUnit = tumbleWeedUnit;

        unit.Stats = tumbleWeedUnit.BaseStats;

        return unit;

    }
}
