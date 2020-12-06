using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CatBoxInfo
{
    public int x;
    public int y;
    public int x2;
    public int y2;
}

public class CatCon : MonoBehaviour
{
    public float speed;
    public float rotationspeed;
    public float rotationmagnitude;
    public int Seedtype;
    public CatListData Job;
    public TypesOfCat MyType = TypesOfCat.PLANTER;
    private GameObject GridManager;
    public GameObject CatManager;
    private float tick;
    private float tick2;
    private bool plant;
    private bool harvest;
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
                            plant = true;
                        }
                        else
                        {
                            plant = false;
                        }
                        break;
                    case TypesOfCat.HARVESTER:
                        if(Job != null)
                        {
                            harvest = true;
                        }
                        else
                        {
                            harvest = false;
                        }
                        break;
                }
            }
        }
        if(plant == true)
        {
            WalkTowardsJob();
            Plant();
        }
        if(harvest == true)
        {
            WalkTowardsJob();
            Harvest();
        }
    }

    public void CreateBox(int x, int y, int x2,int y2)
    {
        CatBoxInfo box; box.x = x; box.y = y; box.x2 = x2; box.y2 = y2;

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

    public void WalkTowardsJob()
    {
        Vector3 target = new Vector3(Job.x + 0.5f, 0, Job.y + 0.5f);
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target - transform.position, rotationspeed, rotationmagnitude);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void Plant()
    {
        Vector3 target = new Vector3(Job.x + 0.5f, 0, Job.y + 0.5f);
        if (transform.position.x < target.x + 0.5f && transform.position.x > target.x - 0.5f && transform.position.z < target.z + 0.5f && transform.position.z > target.z - 0.5F)
        {
            if(Job != null)
            {
                GridManager.GetComponent<GridCon>().RemoveSeed(Seedtype);
                GridManager.GetComponent<GridCon>().ChangeCell(Job.x, Job.y, Seedtype);
                Job = null;
                plant = false;
            }
        }
    }

    public void Harvest()
    {
        Vector3 target = new Vector3(Job.x + 0.5f, 0, Job.y + 0.5f);
        if (transform.position.x < target.x + 0.5f && transform.position.x > target.x - 0.5f && transform.position.z < target.z + 0.5f && transform.position.z > target.z - 0.5F)
        {
            if (Job != null)
            {
                GridManager.GetComponent<GridCon>().HarvestCell(Job.x, Job.y);
                Job = null;
                harvest = false;
            }
        }
    }
}
