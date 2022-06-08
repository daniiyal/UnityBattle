using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStrategy : MonoBehaviour
{
    public void SetStrategy1VS1()
    {
        DataHolder.Strategy = new Strategy1VS1();
    }
    public void SetStrategy3VS31()
    {
        DataHolder.Strategy = new Strategy3VS3();
    }
    public void SetStrategyWallVSWall()
    {
        DataHolder.Strategy = new StrategyWallVSWall();
    }
}
