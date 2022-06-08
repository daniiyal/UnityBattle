using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<GameObject> SpawnUnits3VS3(IArmy Army)
    {

        float i = 2.2f;
        float j = 3.3f;
        GameObject unit_prefab;

        var unitGameObjects = new List<GameObject>();


        foreach (var unit in Army.Units)
        {
            if((12.1f + i) < 23.1f && (11f - i) > 0f)
            {
                if (Army.Faction == Faction.Left)
                    unit_prefab = Instantiate(unit.Prefab, new Vector3(12.1f + i, 1, j), Quaternion.Euler(0, 270, 0));
                else
                    unit_prefab = Instantiate(unit.Prefab, new Vector3(11f - i, 1, j), Quaternion.Euler(0, 90, 0));

                unit_prefab.GetComponent<IUnit>().InitializeTile(unit_prefab);
                unit_prefab.GetComponent<IUnit>().ScriptableUnit = unit.ScriptableUnit;
                unit_prefab.GetComponent<IUnit>().Stats = unit.Stats;

                unitGameObjects.Add(unit_prefab);

                if (j != 5.5f)
                    j += 1.1f;
                else
                {
                    i += 1.1f;
                    j = 3.3f;
                }
            }
            

        }

        return unitGameObjects;
    }

    public GameObject SpawnItem(GameObject unit, ScriptableImprovement improvement)
    {

        GameObject gameObjectReference;

        switch (improvement.ImprovementType)
        {

            case ImprovementType.Spear:
                gameObjectReference = Instantiate(improvement.ImprovementPrefab, unit.transform.Find("RigPelvis/RigSpine1/RigSpine2/RigRibcage/" +
                                                                            "RigRArm1/RigRArm2/RigRArmPalm/Dummy Prop Right"));
                break;
            case ImprovementType.Shield:
                gameObjectReference = Instantiate(improvement.ImprovementPrefab, unit.transform.Find("RigPelvis/RigSpine1/RigSpine2/RigRibcage/" +
                                                                             "RigLArm1/RigLArm2/RigLArmPalm/Dummy Prop Left"));
                break;
            case ImprovementType.Helmet:
                gameObjectReference = Instantiate(improvement.ImprovementPrefab, unit.transform.Find("RigPelvis/RigSpine1/RigSpine2/RigRibcage/" +
                                                                             "RigNeck/RigHead/Dummy Prop Head"));

                break;
            default:
                return null;

        }


        return gameObjectReference;

    }

    public List<GameObject> SpawnUnitsWallVSWall(IArmy Army)
    {

        float i = 2.2f;
        float j = 0f;
        GameObject unit_prefab = null;

        var unitGameObjects = new List<GameObject>();

        if ((12.1f + i) < 23.1f && (11f - i) > 0f)
        {

            foreach (var unit in Army.Units)
            {
                if (Army.Faction == Faction.Left && (11f + i) < 22)
                    unit_prefab = Instantiate(unit.Prefab, new Vector3(12.1f + i, 1, j), Quaternion.Euler(0, 270, 0));
                else
                {
                    if (11f - i > 0)
                        unit_prefab = Instantiate(unit.Prefab, new Vector3(11f - i, 1, j), Quaternion.Euler(0, 90, 0));
                }

                if (unit_prefab != null)
                {
                    unit_prefab.GetComponent<IUnit>().InitializeTile(unit_prefab);
                    unit_prefab.GetComponent<IUnit>().ScriptableUnit = unit.ScriptableUnit;
                    unit_prefab.GetComponent<IUnit>().Stats = unit.Stats;

                    unitGameObjects.Add(unit_prefab);

                }


                if (j != 8.8f)
                    j += 1.1f;
                else
                {
                    i += 1.1f;
                    j = 0f;
                }

            }
        }

        return unitGameObjects;
    }

    public List<GameObject> SpawnUnits1VS1(IArmy Army)
    {
        float i = 2.2f;
        GameObject unit_prefab;

        var unitGameObjects = new List<GameObject>();

        foreach (var unit in Army.Units)
        {
            if ((12.1f + i) <= 23.2f && (11f - i) >= -0.1f)
            {
                if (Army.Faction == Faction.Left)
                    unit_prefab = Instantiate(unit.Prefab, new Vector3(12.1f + i, 1, 4.4f), Quaternion.Euler(0, 270, 0));
                else
                    unit_prefab = Instantiate(unit.Prefab, new Vector3(11f - i, 1, 4.4f), Quaternion.Euler(0, 90, 0));

                unit_prefab.GetComponent<IUnit>().InitializeTile(unit_prefab);
                unit_prefab.GetComponent<IUnit>().ScriptableUnit = unit.ScriptableUnit;
                unit_prefab.GetComponent<IUnit>().Stats = unit.Stats;

                unitGameObjects.Add(unit_prefab);
                i += 1.1f;

            }
        }

        return unitGameObjects;
    }


    public GameObject SpawnUnit(IUnit unit, Vector3 pos)
    {
        GameObject unit_prefab;
        if (unit.Faction == Faction.Left)
            unit_prefab = Instantiate(unit.Prefab, pos, Quaternion.Euler(0, 270, 0));
        else
            unit_prefab = Instantiate(unit.Prefab, pos, Quaternion.Euler(0, 90, 0));

        unit_prefab.GetComponent<IUnit>().InitializeTile(unit_prefab);
        unit_prefab.GetComponent<IUnit>().ScriptableUnit = unit.ScriptableUnit;
        unit_prefab.GetComponent<IUnit>().Stats = unit.Stats;


        return unit_prefab;
    }

    public void DestroyUnit(GameObject unit)
    {
        Destroy(unit);
    }
    public void DestroyUnit(Unit unit)
    {
        Destroy(unit);
    }

}
