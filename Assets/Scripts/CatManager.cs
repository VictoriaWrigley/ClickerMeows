using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    public List<CatListData> PlanterJobs = new List<CatListData>();
    public List<CatListData> HarvesterJobs = new List<CatListData>();
    public GameObject SpinachPlanterPrefab;
    public GameObject PumpkinPlanterPrefab;
    public GameObject BananaPlanterPrefab;
    public GameObject HarvesterPrefab;
    public GameObject CurrencyManager;
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
        HarvesterJobs.Add(newjob);
    }

    public void GivePlanterJob(GameObject cat)
    {
        if(PlanterJobs.Count > 0)
        {
            cat.GetComponent<CatCon>().SetJob(PlanterJobs[0].x, PlanterJobs[0].y);
            PlanterJobs.Remove(PlanterJobs[0]);
        }
    }

    public void GiveHarvesterJob(GameObject cat)
    {
        if(HarvesterJobs.Count > 0)
        {
            cat.GetComponent<CatCon>().SetJob(HarvesterJobs[0].x, HarvesterJobs[0].y);
            HarvesterJobs.Remove(HarvesterJobs[0]);
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
