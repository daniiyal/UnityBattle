using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetMoney : MonoBehaviour
{
    public TMP_InputField moneyInputField;
    public void SetArmyMoney()
    {
        DataHolder.money = Convert.ToInt32(moneyInputField.text);
    }
}
