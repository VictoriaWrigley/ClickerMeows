using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Money : MonoBehaviour
{
    public int currency;
    public TextMeshProUGUI Currencytext;

    void Update()
    {
        Currencytext.text = currency.ToString("$0");
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
    }

    public void RemoveCurrency(int amount)
    {
        currency -= amount;
    }
}
