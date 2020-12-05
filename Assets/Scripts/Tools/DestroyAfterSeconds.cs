using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float time;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > time)
        {
            Destroy(gameObject);
        }
    }
}
