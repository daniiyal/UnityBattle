using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataHolder
{
    public static int money = 1000;
    public static IStrategy Strategy = new Strategy1VS1();
}
