using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Seeds : MonoBehaviour
{
    public GameObject GridManager;
    public GameObject CurrencyManager;
    public GameObject Shop;
    public GameObject InGameUI;
    private int MoneySpent;

    public void OpenShop()
    {
        Shop.SetActive(true);
        GridManager.GetComponent<GridCon>().PauseCon(true);
    }

    public void CloseShop()
    {
        Shop.SetActive(false);
        GridManager.GetComponent<GridCon>().PauseCon(false);
    }
}
