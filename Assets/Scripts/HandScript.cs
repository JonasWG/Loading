using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public Hand hand;
    public float moveSpeed;

    private bool touchingBar;
    private bool grippingBar;


    public enum Hand
    {
        LEFT, RIGHT
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      


    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!grippingBar && collision.CompareTag("LoadingBar"))
        {
            touchingBar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!grippingBar && collision.CompareTag("LoadingBar"))
        {
            touchingBar = false;
        }
    }

    public bool IsTouchingBar()
    {
        return touchingBar;
    }

}