using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{

    private bool touchingBar;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LoadingBar"))
        {
            touchingBar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LoadingBar"))
        {
            touchingBar = false;
        }
    }

    public bool IsTouchingBar()
    {
        return touchingBar;
    }

}