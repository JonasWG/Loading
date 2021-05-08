using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class BarGrabable : MonoBehaviour
{
    private bool hasBeenGrabbed;
    private LoadingBarScript loadingBarScript;

    private GameObject handlePivot;
    

    public float mouseScrollScale;
    
    // Start is called before the first frame update
    void Start()
    {
        loadingBarScript = GameObject.FindWithTag("LoadingBar").GetComponent<LoadingBarScript>();
        handlePivot = GameObject.FindWithTag("HandlePivot");
        loadingBarScript.AddFillPercent(10);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenGrabbed)
        {
            float mouseScroll = -Input.mouseScrollDelta.y * mouseScrollScale;
            loadingBarScript.AddFillPercent(mouseScroll*10);
            transform.position += Vector3.right * (mouseScroll);
            handlePivot.transform.rotation = Quaternion.Slerp(handlePivot.transform.rotation, handlePivot.transform.rotation * quaternion.Euler(0,0,mouseScroll*-100), Time.deltaTime*30f);
            //transform.rotation = Quaternion.Slerp(transform);

            //loadingBarScript.AddFillPercent();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hook"))
        {
            hasBeenGrabbed = true;
        }
    }
}
