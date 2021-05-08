using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public float maxFlow = 1f;
    public float flowRate = 0.5f;
    public StartPoint direction;


    private SpriteRenderer sr;
    private GameObject childObj;

    private float angle;

    public enum StartPoint
    {
        Left, Right, Top, Bottom
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (direction)
        {
            case StartPoint.Left:
                childObj = transform.GetChild(1).gameObject;
                break;
            case StartPoint.Right:
                childObj = transform.GetChild(2).gameObject;
                break;
            case StartPoint.Top:
                childObj = transform.GetChild(3).gameObject;
                break;
            case StartPoint.Bottom:
                childObj = transform.GetChild(0).gameObject;
                break;
        }
        sr = childObj.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case StartPoint.Left:
                FlowRight();
                break;
            case StartPoint.Right:
                FlowLeft();
                break;
            case StartPoint.Top:
                FlowDown();
                break;
            case StartPoint.Bottom:
                FlowUp();
                break;
        }


        
    }

    void FlowRight()
    {
        if(sr.size.x < maxFlow)
        {
            sr.size += new Vector2(flowRate * Time.deltaTime, 0);
        }
    }

    void FlowDown()
    {
        //childObj.transform.rotation = Quaternion.Euler(0, 0, angle-- * Time.deltaTime);
        if (sr.size.y < maxFlow)
        {
            sr.size += new Vector2(0, flowRate * Time.deltaTime);
        }
    }

    void FlowUp()
    {
        if (sr.size.y < maxFlow)
        {
            sr.size += new Vector2(0, flowRate * Time.deltaTime);
        }
    }

    void FlowLeft()
    {
        if (sr.size.x < maxFlow)
        {
            sr.size += new Vector2(flowRate * Time.deltaTime, 0);
        }
    }
}
