using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private Rope rope;
    private bool isGrabbing;
    private Vector3 grabPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        rope = GameObject.Find("Rope").GetComponent<Rope>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = rope.GetLastPos();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("BarGrabable"))
        {
            isGrabbing = true;
            rope.isGrabbing = true;
            grabPosition = other.transform.position + new Vector3(1f, 0.5f, 0f);
            rope.grabPosition = grabPosition;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("BarGrabable"))
        {
            grabPosition = other.transform.position + new Vector3(1f, 0.5f, 0f);
            rope.grabPosition = grabPosition;
        }
    }
}
