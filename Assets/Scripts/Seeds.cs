using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Seeds : MonoBehaviour
{
    public int TypeOfSeed;
    public GameObject GridManager;
    public GameObject CurrencyManager;
    public GameObject Shop;
    public GameObject InGameUI;
    private int MoneySpent;
    public void OpenShop()
    {
        InGameUI.SetActive(false);
        Shop.SetActive(true);
        GridManager.GetComponent<GridCon>().PauseCon(true);
    }

    public void CloseShop()
    {
        InGameUI.SetActive(true);
        Shop.SetActive(false);
        GridManager.GetComponent<GridCon>().PauseCon(false);
    }

    public void PumpkinSeed()
    {
        SeedsBoughtCheck(1);
        TypeOfSeed = 1;
        if (CurrencyManager.GetComponent<Money>().currency > 2)
        {
            GridManager.GetComponent<GridCon>().AddSeed(TypeOfSeed);
            CurrencyManager.GetComponent<Money>().RemoveCurrency(3);
        }
        MoneySpent += 3;
    }
    public void SpinachSeed()
    {
        SeedsBoughtCheck(4);
        TypeOfSeed = 4;
        if (CurrencyManager.GetComponent<Money>().currency > 0)
        {
            GridManager.GetComponent<GridCon>().AddSeed(TypeOfSeed);
            CurrencyManager.GetComponent<Money>().RemoveCurrency(1);
        }
        MoneySpent += 1;
    }


    public void SeedsBoughtCheck(int SeedType)
    {
        if (MoneySpent == 0 && TypeOfSeed != SeedType && GridManager.GetComponent<GridCon>().numberofseeds != 0)
        {
            GridManager.GetComponent<GridCon>().ResetSeeds();
            CurrencyManager.GetComponent<Money>().AddCurrency(MoneySpent);
            MoneySpent = 0;
        }
    }
}
