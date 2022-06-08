using UnityEngine;

public struct MeleeOpponents
{
    public GameObject LeftUnit;
    public GameObject RightUnit;


    public MeleeOpponents(GameObject LeftUnit, GameObject RightUnit)
    {
        this.LeftUnit = LeftUnit;
        this.RightUnit = RightUnit;
    }
}