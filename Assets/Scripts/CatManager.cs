using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatManager : MonoBehaviour
{
    public List<CatListData> PlanterJobs = new List<CatListData>();
    public List<CatListData> HarvesterJobs = new List<CatListData>();

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
}
