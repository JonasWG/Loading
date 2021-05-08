using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggler : MonoBehaviour
{
    public float timer;
    public float timeForWiggle = 0.175f;
    float orgTime;
    public float zMin = -5f;
    public float zMax = 5f;
    void Start()
    {
        orgTime = timeForWiggle;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeForWiggle)
        {
            timer = 0;
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(zMin, zMax));
            timeForWiggle = orgTime * Random.Range(0.8f, 1.2f);
        }
    }
}
