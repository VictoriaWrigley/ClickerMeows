using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    public List<CatListData> PlanterJobs = new List<CatListData>();
    public List<CatListData> HarvesterJobs = new List<CatListData>();
    public List<CatListData> TakenJobs = new List<CatListData>();
    public GameObject SpinachPlanterPrefab;
    public GameObject PumpkinPlanterPrefab;
    public GameObject BananaPlanterPrefab;
    public GameObject HarvesterPrefab;
    public GameObject CurrencyManager;
    private CatListData jobtoremove;
    public int harvesterlength;

    public void AddPlanterJob(int x, int y)
    {
        CatListData newjob = new CatListData(x, y);
        foreach(CatListData job in PlanterJobs)
        {
            if(job.x == x && job.y == y)
            {
                return;
            }
        }
        foreach (CatListData job in TakenJobs)
        {
            if (job.x == x && job.y == y)
            {
                return;
            }
        }
        PlanterJobs.Add(newjob);
    }

    public void AddHarvesterJob(int x, int y)
    {
        CatListData newjob = new CatListData(x, y);
        foreach (CatListData job in HarvesterJobs)
        {
            if (job.x == x && job.y == y)
            {
                return;
            }
        }
        foreach (CatListData job in TakenJobs)
        {
            if (job.x == x && job.y == y)
            {
                return;
            }
        }
        HarvesterJobs.Add(newjob);
    }

    public void RemoveJob(CatListData job)
    {
        if (TakenJobs.Contains(job))
        {
            TakenJobs.Remove(job);
        }
    }

    public void UpdateListWithMouse(int x, int y, int index)
    {
        foreach (CatListData job in PlanterJobs)
        {
            if (job.x == x && job.y == y)
            {
                if (index != 0) //update this for soil
                {
                    jobtoremove = job;
                }
            }
        }
        foreach (CatListData job in HarvesterJobs)
        {
            if (job.x == x && job.y == y)
            {
                if (index == 0) //change this to soil
                {
                    jobtoremove = job;
                }
            }
        }

        if (PlanterJobs.Contains(jobtoremove))
        {
            PlanterJobs.Remove(jobtoremove);
        }

        if (HarvesterJobs.Contains(jobtoremove))
        {
            HarvesterJobs.Remove(jobtoremove);
        }
        harvesterlength = HarvesterJobs.Count;
    }

    public void GivePlanterJob(GameObject cat, CatBoxInfo box)
    {
        CatListData nextjob = new CatListData(-1,-1);
        if (PlanterJobs.Count > 0)
        {
            foreach(CatListData job in PlanterJobs)
            {
                if(job.x > box.x && job.x < box.x2 && job.y > box.y && job.y > box.y2)
                {
                    nextjob = job;
                    break;
                }
            }
            if(nextjob.x == -1)
            {
                return;
            }

            cat.GetComponent<CatCon>().SetJob(PlanterJobs[0]);
            TakenJobs.Add(PlanterJobs[0]);
            PlanterJobs.Remove(PlanterJobs[0]);
        }
    }

    public void GiveHarvesterJob(GameObject cat, CatBoxInfo info)
    {
        if(HarvesterJobs.Count > 0)
        {
            CatListData nextjob = new CatListData(-1, -1);
            if (HarvesterJobs.Count > 0)
            {
                foreach (CatListData job in HarvesterJobs)
                {
                    if (job.x > info.x && job.x < info.x2 && job.y > info.y && job.y > info.y2)
                    {
                        nextjob = job;
                        break;
                    }
                }
                if (nextjob.x == -1)
                {
                    return;
                }

                cat.GetComponent<CatCon>().SetJob(HarvesterJobs[0]);
                TakenJobs.Add(PlanterJobs[0]);
                HarvesterJobs.Remove(HarvesterJobs[0]);
            }
        }
    }

    public void SpawnSpinachPlanterCat()
    {
        if (CurrencyManager.GetComponent<Money>().currency >= 100)
        {
            Instantiate(SpinachPlanterPrefab, transform.position, Quaternion.identity);
            CurrencyManager.GetComponent<Money>().RemoveCurrency(100);
        }
    }

    public void SpawnPumpkinPlanterCat()
    {
        if (CurrencyManager.GetComponent<Money>().currency >= 200)
        {
            Instantiate(PumpkinPlanterPrefab, transform.position, Quaternion.identity);
            CurrencyManager.GetComponent<Money>().RemoveCurrency(200);
        }
    }

    public void SpawnBananaPlanterCat()
    {
        if (CurrencyManager.GetComponent<Money>().currency >= 400)
        {
            Instantiate(BananaPlanterPrefab, transform.position, Quaternion.identity);
            CurrencyManager.GetComponent<Money>().RemoveCurrency(400);
        }
    }

    public void SpawnHarvesterCat()
    {
        if (CurrencyManager.GetComponent<Money>().currency >= 75)
        {
            Instantiate(HarvesterPrefab, transform.position, Quaternion.identity);
            CurrencyManager.GetComponent<Money>().RemoveCurrency(75);
        }
    }
}
