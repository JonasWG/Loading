using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeScript : MonoBehaviour
{

    public float speedModifier;
    public GameObject pedal;


    private bool btn;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBike();
    }

    void MoveBike()
    {
        if(Input.GetKey(KeyCode.A) && btn)
        {
            btn = false;
            rb.AddForce(new Vector2(Time.deltaTime * speedModifier, 0), ForceMode2D.Impulse);
            RotatePedal();
        }
        if (Input.GetKey(KeyCode.D) && !btn)
        {
            btn = true;
            rb.AddForce(new Vector2(Time.deltaTime * speedModifier, 0), ForceMode2D.Impulse);
            RotatePedal();
        }
    }

    void RotatePedal()
    {
        pedal.transform.Rotate(new Vector3(0, 0, rb.velocity.magnitude * Time.deltaTime * 360));
    }
}
