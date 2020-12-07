using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GridCon : MonoBehaviour
{
    private GridInfo[,] gridArray;
    public int StartWidth;
    public int StartHeight;
    public int ExpandCost = 10;
    public int width;
    public int height;
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
    public GameObject SoilPlane;
    public bool DrawMode = false;
    private int x3;
    private int y3;
    //temp
    public TextMeshProUGUI PumpkinText;
    public TextMeshProUGUI SpinachText;
    public TextMeshProUGUI BananaText;
    public TextMeshProUGUI ExpandText;
    public GameObject CellHighlight;
    public GameObject FarmGround;

    public TextMeshProUGUI Stext;
    public TextMeshProUGUI ptext;
    public TextMeshProUGUI btext;
    public TextMeshProUGUI CostOfNewPatch;
    public GameObject BuildCanvas;
    public bool selectingcat = false;
    public GameObject CatSelected = null;
    void Awake()
    {
        FarmGround.transform.localScale = new Vector3(StartWidth /10f, 1, StartHeight / 10f);
        FarmGround.transform.position = new Vector3(StartWidth / 2f, 0, StartHeight / 2f);
        if (ItemList == null)
        {
            ItemList = new List<Items>();
        }
        GenerateGrid(StartWidth, StartHeight, 1);
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
        //temp
        PumpkinText.text = ItemList[7].NumberOfSeeds.ToString();
        SpinachText.text = ItemList[3].NumberOfSeeds.ToString();
        BananaText.text = ItemList[11].NumberOfSeeds.ToString();
        ExpandText.text = ExpandCost.ToString();
        Stext.text = ItemList[3].MerchantValue.ToString();
        ptext.text = ItemList[7].MerchantValue.ToString();
        btext.text = ItemList[11].MerchantValue.ToString();
        //Tick
        if (Pause == false)
        {
            tick += Time.deltaTime;
            if (tick >= 1)
            {
                UpdateGrid();
                tick = 0;
                BuyMerchantSeeds();
            }
            UpdateMouse();
        }

    }

    //fix this
    public void BuyMerchantSeeds()
    {
        foreach(Items item in ItemList)
        {
            if(item.MerchantValue > 0 && CurrencyManager.GetComponent<Money>().currency >= item.MerchantValue)
            {
                item.NumberOfSeeds += item.MerchantValue;
                CurrencyManager.GetComponent<Money>().RemoveCurrency(item.MerchantValue * item.Cost);
            }
        }
    }

    private void UpdateMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 mousepos = new Vector3(0, 0, 0);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if(hit.transform.gameObject.tag == "Ground")
            {
                mousepos = hit.point;
                CellHighlight.SetActive(true);
            }

            if (hit.transform.gameObject.tag == "Patch")
            {
                mousepos = hit.point;
                CellHighlight.SetActive(true);

                if (Input.GetMouseButtonDown(1) && selectingcat == true && CatSelected != null)
                {
                    CatSelected.GetComponent<CatCon>().SetCatBox(hit.transform.gameObject.GetComponent<Patch>().MyBox);
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (hit.transform.gameObject.tag == "Cat")
                {
                    CatSelected = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<CatCon>().Select();
                    selectingcat = true;
                }
                else
                {
                    selectingcat = false;
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                selectingcat = false;
            }

            CellHighlight.SetActive(false);
            return;
        }

        int x1 = Mathf.FloorToInt(mousepos[0]);
        int y1 = Mathf.FloorToInt(mousepos[2]);
        int x = 0;
        int y = 0;
        float cost = 0;
        if(x1 >= 0 && x1 <= width && y1 >= 0 && y1 < height)
        {
            x = Mathf.FloorToInt(mousepos[0]);
            y = Mathf.FloorToInt(mousepos[2]);
        }
        Vector3 highlightpos = new Vector3(x + 0.5f, 0.05f, y + 0.5f);
        CellHighlight.transform.position = highlightpos;
        CellHighlight.transform.localScale = new Vector3(1, 1, 1);
        if(DrawMode == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                x3 = x;
                y3 = y;
                Debug.Log(gridArray[x,y].celltype);
            }

            if (Input.GetMouseButton(0))
            {
                float middlex = (x3 + (x + 1)) / 2f;
                float middley = (y3 + (y + 1)) / 2f;
                float width2 = Mathf.Abs((x) - x3) + 1;
                float height2 = Mathf.Abs((y) - y3) + 1;
                cost = width2 * height2;
                CostOfNewPatch.text = cost.ToString();
                CellHighlight.transform.position = new Vector3(middlex, 0.05f, middley);
                CellHighlight.transform.localScale = new Vector3(width2, height2, 1);
            }

            CatBoxInfo box = new CatBoxInfo(x3, y3, x, y);
            if (Input.GetMouseButtonUp(0))
            {
                float width2 = Mathf.Abs((x) - x3) + 1;
                float height2 = Mathf.Abs((y) - y3) + 1;
                cost = width2 * height2;
                if (CurrencyManager.GetComponent<Money>().currency < cost)
                {
                    cost = 0;
                    return;
                }
                else
                {
                    CurrencyManager.GetComponent<Money>().RemoveCurrency(Mathf.RoundToInt(cost));
                    cost = 0;
                }
                for (int x4 = 0; x4 < gridArray.GetLength(0); x4++)
                {
                    for (int y4 = 0; y4 < gridArray.GetLength(1); y4++)
                    {
                        float middlex = (box.x + (box.x2 + 1)) / 2f;
                        float middley = (box.y + (box.y2 + 1)) / 2f;
                        width2 = Mathf.Abs((box.x2) - box.x) + 1;
                        height2 = Mathf.Abs((box.y2) - box.y) + 1;

                        if (x4 >= middlex - width2/2 && x4 < middlex + width2 / 2 && y4 >= middley - height2 / 2 && y4 < middley + height2 / 2)
                        {
                            if (gridArray[x4, y4].celltype == 2)
                            {
                                return;
                            }
                            gridArray[x4, y4].celltype = 2;
                        }
                    }
                }
                var plane = Instantiate(SoilPlane, new Vector3(0, 0, 0), Quaternion.identity);
                plane.gameObject.GetComponent<Patch>().MakeSoilPlane(box);
            }
        }

        if (Input.GetMouseButtonDown(0) && Pause == false && DrawMode == false)
        {
            if (gridArray[x, y].celltype == 2)
            {
                if (ReturnNumberOfSeeds(SelectedSeedType) > 0)
                {
                    RemoveSeed(SelectedSeedType);
                    gridArray[x, y].celltype = SelectedSeedType;
                }
            }
            else
            {
                HarvestCell(x, y);
            }
            UpdatePrefab(x, y);
            CatManager.GetComponent<CatManager>().UpdateListWithMouse(x, y, gridArray[x,y].celltype);
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
                        if(gridArray[x,y].celltype == 2)
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

    public void ExpandGrid()
    {
        if(CurrencyManager.GetComponent<Money>().currency < ExpandCost)
        {
            return;
        }
        else
        {
            CurrencyManager.GetComponent<Money>().RemoveCurrency(ExpandCost);
            ExpandCost += Mathf.RoundToInt(ExpandCost / 4f);
        }
        GridInfo[,] temparray = new GridInfo[width, height];
        System.Array.Copy(gridArray, temparray, width * height);
        width++;
        height++;
        GenerateGrid(width, height, 1);
        for (int x = 0; x < temparray.GetLength(0); x++)
        {
            for (int y = 0; y < temparray.GetLength(1); y++)
            {
                gridArray[x, y].celltype = temparray[x, y].celltype;
                gridArray[x, y].timer = temparray[x, y].timer;
                gridArray[x, y].CurrentPrefab = temparray[x, y].CurrentPrefab;
                UpdatePrefab(x, y);
            }
        }

        FarmGround.transform.localScale = new Vector3(width / 10f, 1, height / 10f);
        FarmGround.transform.position = new Vector3(width / 2f, 0, height / 2f);
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
            SelectedSeedType = index;
        }
    }

    public void AddMerchantSeeds(int type)
    {
        foreach(Items item in ItemList)
        {
            if(item.CellType == type)
            {
                item.MerchantValue++;
            }
        }
    }

    public void RemoveMerchantSeeds(int type)
    {
        foreach (Items item in ItemList)
        {
            if (item.CellType == type)
            {
                item.MerchantValue--;
            }
        }
    }

    public void ToggleBuildMode()
    {
        DrawMode = !DrawMode;
        BuildCanvas.SetActive(DrawMode);
    }

}
