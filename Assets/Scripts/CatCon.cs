﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CatBoxInfo
{
    public int x;
    public int y;
    public int x2;
    public int y2;

    public CatBoxInfo(int xx,int yy,int xxx,int yyy)
    {
        x = xx;
        y = yy;
        x2 = xxx;
        y2 = yyy;
    }
}

public class CatCon : MonoBehaviour
{
    public float speed;
    public float rotationspeed;
    public float rotationmagnitude;
    public int Seedtype;
    public CatListData Job = null;
    public TypesOfCat MyType = TypesOfCat.PLANTER;
    private GameObject GridManager;
    public GameObject CatManager;
    private float tick;
    private float tick2;
    private bool plant;
    private bool harvest;
    public CatBoxInfo box;
    public GameObject patch = null;
    public GameObject ring;
    public bool selected = false;
    public void Awake()
    {
        CatManager = GameObject.Find("CatManager");
        GridManager = GameObject.Find("GridManager");
    }
    public void OnEnable()
    {
        GridManager = GameObject.Find("GridManager");
        ring.SetActive(false);
    }
    public enum TypesOfCat
    {
        PLANTER,
        HARVESTER
    }
    void Update()
    {
        if (patch == null)
        {
            return;
        }
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
                    Debug.Log("GetJob");
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

    public void SetCatBox(CatBoxInfo info, GameObject plane)
    {
        box = new CatBoxInfo(info.x,info.y,info.x2,info.y2);
        Debug.Log(box.x + "box x");
        Debug.Log(box.y + "box y");
        Debug.Log(box.x2 + "box x");
        Debug.Log(box.y2 + "box y");
        patch = plane;
    }

    public void GetJob()
    {
        if(MyType == TypesOfCat.PLANTER)
        {
            CatManager.GetComponent<CatManager>().GivePlanterJob(gameObject,box);
        }

        if (MyType == TypesOfCat.HARVESTER)
        {
            CatManager.GetComponent<CatManager>().GiveHarvesterJob(gameObject,box);
        }
    }

    public void SetJob(CatListData newjob)
    {
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
                CatManager.GetComponent<CatManager>().RemoveJob(Job);
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
                CatManager.GetComponent<CatManager>().RemoveJob(Job);
                Job = null;
                harvest = false;
            }
        }
    }

    public void Highlight()
    {
        ring.SetActive(true);
    }

    public void Highlightfalse()
    {
        ring.SetActive(false);
        var rend = ring.GetComponent<SpriteRenderer>();
        rend.material.color = Color.white;
        selected = false;
    }

    public void Select()
    {
        var rend = ring.GetComponent<SpriteRenderer>();
        rend.material.color = Color.yellow;
        selected = true;
    }
}
