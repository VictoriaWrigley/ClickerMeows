using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Items
{
    public string Name = "";
    public int CellType = 0;
    public bool Seed = false;
    public bool Plant = false;
    public bool Growing = false;
    public bool Dying = false;
    public int GrowTime = 0;
    public int DieTime = 0;
    public int Cash = 0;
    public Object Prefab = null;
    public int NumberOfSeeds = 0;
    public int Cost = 0;
    public int MerchantValue;
}

