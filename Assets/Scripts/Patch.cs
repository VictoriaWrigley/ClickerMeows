using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : MonoBehaviour
{
    public CatBoxInfo MyBox;
    public void MakeSoilPlane(CatBoxInfo info)
    {
        MyBox = new CatBoxInfo(info.x,info.y,info.x2,info.y2);
        float width = Mathf.Abs((info.x2) - info.x) + 1;
        float height = Mathf.Abs((info.y2) - info.y) +1;
        float middlex = (info.x + (info.x2 + 1)) / 2f;
        float middley = (info.y + (info.y2 + 1)) / 2f;
        transform.position = new Vector3(middlex, 0.001f, middley);
        transform.localScale = new Vector3(width / 10f, 1, height / 10f);
    }
}
