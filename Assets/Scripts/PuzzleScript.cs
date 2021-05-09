using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript : MonoBehaviour
{

    public GameObject bar;
    public float fillRatio;
    
    private LoadingBarScript loadingBarScript;
    
    private PipeScript connectedPipe;

    private int countCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        loadingBarScript = bar.GetComponent<LoadingBarScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countCount > 0)
        {
            if (connectedPipe.IsEnergized())
            {
                loadingBarScript.AddFillPercent(fillRatio * Time.deltaTime);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PipePoint"))
        {
            Debug.Log("Collided with pipe");
            connectedPipe = collision.transform.parent.GetComponent<PipeScript>();
            countCount++;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PipePoint"))
        {
            countCount--;
        } 
    }
}
