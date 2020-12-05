using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{
    [SerializeField]
    private float tick = 0;
    [SerializeField]
    private float tickdelay;
    public GameObject GridManager;
    public GameObject CurrencyManager;

    void Update()
    {
        tick += Time.deltaTime;
        if (tick >= tickdelay)
        {
            tick = 0;
        }
    }

    public void CreateActiveList()
    {

    }

    public void GrowSeeds()
    {

    }

    public void HarvestPlant()
    {
        CurrencyManager.GetComponent<Money>().AddCurrency(1);
    }
}
