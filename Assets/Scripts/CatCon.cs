using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCon : MonoBehaviour
{
    public int Seedtype;
    public int NumberOfSeeds;
    public int areax;
    public int areay;
    public int areax2;
    public int areay2;
    public List<CatListData> Chores = new List<CatListData>();
    public TypesOfCat MyType = TypesOfCat.PLANTER;
    private GameObject GridManager;
    private float tick;
    private float tick2;

    public void Awake()
    {
        GridManager = GameObject.Find("GridManager");
    }
    public void OnEnable()
    {
        GridManager = GameObject.Find("GridManager");
    }
    public enum TypesOfCat
    {
        PLANTER,
        HARVESTER
    }
    void Update()
    {
        tick += Time.deltaTime;
        if(tick >= 1)
        {
            tick = 0;
            tick2 += Time.deltaTime;
            if(tick2 >= 0.1)
            {
                FindChores();
                Debug.Log(Chores.Count);
                switch (MyType)
                {
                    case TypesOfCat.PLANTER:
                        Plant();
                        break;
                    case TypesOfCat.HARVESTER:
                        break;
                }
            }
        }
    }

    public void FindChores()
    {
        GridManager.GetComponent<GridCon>().ReturnChores(areax, areay,areax2,areay2, MyType.ToString(),gameObject);
    }

    public void Plant()
    {
        if(Chores.Count > 0 && NumberOfSeeds > 0)
        {
            Vector3 pos = new Vector3(Chores[0].x + 0.5f, 0, Chores[0].y + 0.5f);
            transform.position = pos;
            GridManager.GetComponent<GridCon>().ChangeCell(Chores[0].x, Chores[0].y, Seedtype);
            Chores.Remove(Chores[0]);
            NumberOfSeeds--;
        }
    }

    public void AddChore(int cx,int cy)
    {
        foreach(CatListData cell in Chores)
        {
            if(cx == cell.x && cx == cell.y)
            {
                return;
            }
        }
        Chores.Add(new CatListData(cx,cy));
    }
}
