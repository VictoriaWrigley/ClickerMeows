using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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

    public int cost1 = 100;
    public int cost2 = 200;
    public int cost3 = 400;
    public int cost4 = 75;
    public TextMeshProUGUI SpinachCatCost;
    public TextMeshProUGUI PumpkinCatCost;
    public TextMeshProUGUI BananaCatCost;
    public TextMeshProUGUI HarvesterCatCost;
    public void Update()
    {
        //display costs
        SpinachCatCost.text = cost1.ToString();
        PumpkinCatCost.text = cost2.ToString();
        BananaCatCost.text = cost3.ToString();
        HarvesterCatCost.text = cost4.ToString();
    }

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

    public void GivePlanterJob(GameObject cat, CatBoxInfo info)
    {
        CatListData nextjob = null;
        if (PlanterJobs.Count > 0 && info != null)
        {
            float width2 = Mathf.Abs((info.x2) - info.x) + 1;
            float height2 = Mathf.Abs((info.y2) - info.y) + 1;
            float middlex = (info.x + (info.x2 + 1)) / 2f;
            float middley = (info.y + (info.y2 + 1)) / 2f;

            foreach (CatListData job in PlanterJobs)
            {
                if(job.x >= middlex - width2 / 2 && job.x < middlex + width2 / 2 && job.y >= middley - height2 / 2 && job.y < middley + height2 / 2)
                {
                    nextjob = job;
                    break;
                }
            }
            if(nextjob != null)
            {
                cat.GetComponent<CatCon>().SetJob(nextjob);
                TakenJobs.Add(nextjob);
                PlanterJobs.Remove(nextjob);
            }
        }
    }

    public void GiveHarvesterJob(GameObject cat, CatBoxInfo info)
    {
        CatListData nextjob = null;
        if (HarvesterJobs.Count > 0 && info != null)
        {

            float width2 = Mathf.Abs((info.x2) - info.x) + 1;
            float height2 = Mathf.Abs((info.y2) - info.y) + 1;
            float middlex = (info.x + (info.x2 + 1)) / 2f;
            float middley = (info.y + (info.y2 + 1)) / 2f;

            if (HarvesterJobs.Count > 0)
            {
                foreach (CatListData job in HarvesterJobs)
                {
                    if (job.x >= middlex - width2 / 2 && job.x < middlex + width2 / 2 && job.y >= middley - height2 / 2 && job.y < middley + height2 / 2)
                    {
                        nextjob = job;
                        break;
                    }
                }
                if(nextjob != null)
                {
                    if (nextjob.x == -1)
                    {
                        return;
                    }
                }

                cat.GetComponent<CatCon>().SetJob(nextjob);
                TakenJobs.Add(nextjob);
                HarvesterJobs.Remove(nextjob);
            }
        }
    }

    public void SpawnSpinachPlanterCat()
    {
        if (CurrencyManager.GetComponent<Money>().currency >= cost1)
        {
            Vector3 offset = new Vector3(Random.Range(-2f, 2f),0, Random.Range(-2f, 2f));
            Instantiate(SpinachPlanterPrefab, transform.position + offset, Quaternion.identity);
            CurrencyManager.GetComponent<Money>().RemoveCurrency(cost1);
            cost1 += Mathf.RoundToInt(cost1 / 4);
        }
    }

    public void SpawnPumpkinPlanterCat()
    {
        if (CurrencyManager.GetComponent<Money>().currency >= cost2)
        {
            Vector3 offset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            Instantiate(PumpkinPlanterPrefab, transform.position + offset, Quaternion.identity);
            CurrencyManager.GetComponent<Money>().RemoveCurrency(cost2);
            cost2 += Mathf.RoundToInt(cost2 / 4);
        }
    }

    public void SpawnBananaPlanterCat()
    {
        if (CurrencyManager.GetComponent<Money>().currency >= cost3)
        {
            Vector3 offset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
            Instantiate(BananaPlanterPrefab, transform.position + offset, Quaternion.identity);
            CurrencyManager.GetComponent<Money>().RemoveCurrency(cost3);
            cost3 += Mathf.RoundToInt(cost3 / 4);
        }
    }

    public void SpawnHarvesterCat()
    {
        Vector3 offset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
        if (CurrencyManager.GetComponent<Money>().currency >= cost4)
        {
            Instantiate(HarvesterPrefab, transform.position + offset, Quaternion.identity);
            CurrencyManager.GetComponent<Money>().RemoveCurrency(cost4);
            cost4 += Mathf.RoundToInt(cost4 / 4);
        }
    }
}
