using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCon : MonoBehaviour
{
    public int Seedtype;
    public int areax;
    public int areay;
    public int areax2;
    public int areay2;
    public CatListData Job;
    public TypesOfCat MyType = TypesOfCat.PLANTER;
    private GameObject GridManager;
    public GameObject CatManager;
    private float tick;
    private float tick2;

    public void Awake()
    {
        CatManager = GameObject.Find("CatManager");
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
            if (GridManager.GetComponent<GridCon>().Pause == true)
            {
                return;
            }

            tick2 += Time.deltaTime;
            if(tick2 >= 0.1)
            {
                if(Job == null)
                {
                    GetJob();
                }
                switch (MyType)
                {
                    case TypesOfCat.PLANTER:
                        if(Job != null && GridManager.GetComponent<GridCon>().ReturnNumberOfSeeds(Seedtype) > 0)
                        {
                            Plant();
                        }
                        break;
                    case TypesOfCat.HARVESTER:
                        if(Job != null)
                        {
                            Harvest();
                        }
                        break;
                }
            }
        }
    }

    public void GetJob()
    {
        if(MyType == TypesOfCat.PLANTER)
        {
            CatManager.GetComponent<CatManager>().GivePlanterJob(gameObject);
        }

        if (MyType == TypesOfCat.HARVESTER)
        {
            CatManager.GetComponent<CatManager>().GiveHarvesterJob(gameObject);
        }
    }

    public void SetJob(int x, int y)
    {
        CatListData newjob = new CatListData(x, y);
        Job = newjob;
    }

    public void Plant()
    {
        Vector3 pos = new Vector3(Job.x, 1, Job.y);
        transform.position = pos;
        GridManager.GetComponent<GridCon>().RemoveSeed(Seedtype);
        GridManager.GetComponent<GridCon>().ChangeCell(Job.x,Job.y,Seedtype);
        Job = null;
    }

    public void Harvest()
    {
        Vector3 pos = new Vector3(Job.x, 1, Job.y);
        transform.position = pos;

        GridManager.GetComponent<GridCon>().HarvestCell(Job.x, Job.y);
        Job = null;
    }
}
