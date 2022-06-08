using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryScreen : MonoBehaviour
{
    public TMP_Text text;
    public GameObject moveButtons;
    public void Setup(string side)
    {
        moveButtons.SetActive(false);
        gameObject.SetActive(true);
        text.text = side;
    }
}
