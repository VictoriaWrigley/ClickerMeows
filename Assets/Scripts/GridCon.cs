using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCon : MonoBehaviour
{
    private GridInfo[,] gridArray;
    private int width;
    private int height;
    private float cellSize;
    public Color dirt;
    public Color PumpkinSeed;
    public Color YoungPumpkin;
    public Color Pumpkin;
    public Color SpinachSeed;
    public Color YoungSpinach;
    public Color Spinach;
    public GameObject CellBehaviourManager;
    private float tick = 0;
    public GameObject CurrencyManager;
    public int SelectedSeedType;
    public List<Items> ItemList;
    public bool Pause = false;
    public GameObject CashParticles;
    public GameObject CatManager;
    public GameObject SeedManager;
    void Awake()
    {
        if (ItemList == null)
        {
            ItemList = new List<Items>();
        }
        GenerateGrid(10, 10, 1);
        //Set to dirt
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y].celltype = 0;
            }
        }
    }
    public void GenerateGrid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridArray = new GridInfo[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = new GridInfo();
            }
        }
    }

    public void PauseCon(bool OnOff)
    {
        Pause = OnOff;
    }

    void Update()
    {
        //Tick
        if(Pause == false)
        {
            tick += Time.deltaTime;
        }
        if (tick >= 1)
        {
            UpdateGrid();
            tick = 0;
        }

        if (Input.GetMouseButtonDown(0) && Pause == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 mousepos = new Vector3(0, 0, 0);
            LayerMask mask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(ray, out hit, 100f, mask))
            {
                mousepos = hit.point;
            }
            Debug.Log(mousepos);
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    if (mousepos.x > x && mousepos.x < x + 1 && mousepos.z > y && mousepos.z < y + 1)
                    {
                        if(gridArray[x, y].celltype == 0)
                        {
                            if(ReturnNumberOfSeeds(SelectedSeedType) > 0)
                            {
                                RemoveSeed(SelectedSeedType);
                                gridArray[x, y].celltype = SelectedSeedType;
                            }
                        }
                        else
                        {
                            HarvestCell(x, y);
                        }
                        UpdatePrefab(x,y);
                    }
                }
            }
        }
    }
    private void UpdateGrid()
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                foreach (Items item in ItemList)
                {
                    if(item.CellType == gridArray[x, y].celltype)
                    {
                        //Grow
                        if(item.Growing == true)
                        {
                            gridArray[x, y].timer += 1;
                            if (gridArray[x, y].timer >= item.GrowTime)
                            {
                                gridArray[x, y].celltype++;
                                gridArray[x, y].timer = 0;
                                UpdatePrefab(x, y);
                            }
                        }
                        //Die
                        if (item.Dying == true)
                        {
                            gridArray[x, y].timer += 1;
                            if (gridArray[x, y].timer >= item.DieTime)
                            {
                                gridArray[x, y].celltype = 0;
                                gridArray[x, y].timer = 0;
                                UpdatePrefab(x, y);
                            }
                        }

                        //Send Planter Jobs To Cat Manager
                        if(gridArray[x,y].celltype == 0)
                        {
                            CatManager.GetComponent<CatManager>().AddPlanterJob(x,y);
                        }

                        //Sent Harvester Jobs To Cat Manager
                        if (item.Plant == true)
                        {
                            CatManager.GetComponent<CatManager>().AddHarvesterJob(x, y);
                        }
                    }
                }
            }
        }
    }

    public void UpdatePrefab(int x, int y)
    {
        if(gridArray[x,y].CurrentPrefab != null)
        {
            Destroy(gridArray[x, y].CurrentPrefab);
        }
        Object Prefab = null;
        Vector3 pos = new Vector3(x + 0.5f, 0, y+ 0.5f);
        foreach (Items item in ItemList)
        {
            if(item.CellType == gridArray[x, y].celltype)
            {
                Prefab = item.Prefab;
            }
        }
        if (Prefab != null)
        {
            var SetPrefab = Instantiate(Prefab, pos, Quaternion.identity);
            gridArray[x, y].CurrentPrefab = SetPrefab;
        }
        else
        {
            gridArray[x, y].CurrentPrefab = null;
        }
    }

    private void OnDrawGizmos()
    {
        if(gridArray != null)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    Vector3 pos = new Vector3(x + 0.5f, 0, y + 0.5f);
                    Vector3 size = new Vector3(1, 0.1f, 1);
                    if (gridArray[x, y].celltype == 0)
                    {
                        Gizmos.color = dirt;
                    }
                    if (gridArray[x, y].celltype == 1)
                    {
                        Gizmos.color = PumpkinSeed;
                    }
                    if (gridArray[x, y].celltype == 2)
                    {
                        Gizmos.color = YoungPumpkin;
                    }
                    if (gridArray[x, y].celltype == 3)
                    {
                        Gizmos.color = Pumpkin;
                    }
                    if (gridArray[x, y].celltype == 4)
                    {
                        Gizmos.color = SpinachSeed;
                    }
                    if (gridArray[x, y].celltype == 5)
                    {
                        Gizmos.color = YoungSpinach;
                    }
                    if (gridArray[x, y].celltype == 6)
                    {
                        Gizmos.color = Spinach;
                    }
                    Gizmos.DrawCube(pos, size);
                }
            }
        }
    }
    public void AddMoney(int Cash)
    {
        CurrencyManager.GetComponent<Money>().AddCurrency(Cash);
    }

    public void ChangeCell(int x, int y,int Celltype)
    {
        gridArray[x, y].celltype = Celltype;
        UpdatePrefab(x, y);
    }

    public void HarvestCell(int x, int y)
    {
        foreach (Items item in ItemList)
        {
            if (item.CellType == gridArray[x, y].celltype)
            {
                if (item.Plant == true)
                {
                    gridArray[x, y].celltype = 0;
                    gridArray[x, y].timer = 0;
                    AddMoney(item.Cash);
                    Vector3 pos = new Vector3(x + 0.5f, 0, y + 0.5f);
                    Quaternion rot = Quaternion.Euler(-90, 0, 0);
                    var cp = Instantiate(CashParticles, pos, rot);

                    UpdatePrefab(x, y);
                }
            }
        }
    }

    public void AddSeed(int index)
    {
        foreach (Items item in ItemList)
        {
            if(item.CellType == index)
            {
                item.NumberOfSeeds++;
            }
        }
    }

    public void RemoveSeed(int index)
    {
        foreach (Items item in ItemList)
        {
            if (item.CellType == index)
            {
                item.NumberOfSeeds--;
            }
        }
    }

    public int ReturnNumberOfSeeds(int index)
    {
        int total = 0;
        foreach (Items item in ItemList)
        {
            if (item.CellType == index)
            {
                total = item.NumberOfSeeds;
            }
        }
        return total;
    }

    public int ReturnCost(int index)
    {
        int cost = 0;
        foreach (Items item in ItemList)
        {
            if (item.CellType == index)
            {
                cost = item.Cost;
            }
        }
        return cost;
    }

    public void SetSelectedSeed(int index)
    {
        SelectedSeedType = index;
    }

    public void BuySeed(int index)
    {
        int cost = ReturnCost(index);
        if (CurrencyManager.GetComponent<Money>().currency >= cost)
        {
            AddSeed(index);
            CurrencyManager.GetComponent<Money>().RemoveCurrency(cost);
        }
    }
}
