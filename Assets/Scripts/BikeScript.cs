using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeScript : MonoBehaviour
{

    public float speedModifier;
    public GameObject pedal;
    public GameObject wheelFront;
    public GameObject wheelBack;
    
    public GameObject finish;
    public GameObject bar;


    private bool btn;
    private Rigidbody2D rb;
    private LoadingBarScript loadingBarScript;

    Vector3 startPos;
    Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = gameObject.transform.position;
        endPos = finish.transform.position;
        loadingBarScript = bar.GetComponent<LoadingBarScript>();
        CursorManager.Instance.SetCursorVisible(false);
    }

    // Update is called once per frame
    void Update()
    {
        MoveBike();
    }

    void MoveBike()
    {
        var left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        var right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);


        if (left)
        {
            btn = false;
            rb.AddForce(new Vector2(Time.deltaTime * speedModifier, 0), ForceMode2D.Impulse);
            //RotatePedal();
        }
        if (right)
        {
            btn = true;
            rb.AddForce(new Vector2(Time.deltaTime * speedModifier, 0), ForceMode2D.Impulse);
            //RotatePedal();
        }

        if (left || right)
        {
            SoundController.Instance.Play("Bicycle", 1);
        }
        else
        {
            SoundController.Instance.Pause("Bicycle");
        }
        
        Vector3 currentPos = gameObject.transform.position;

        float p = (currentPos.x - startPos.x) / (endPos.x - startPos.x);
        loadingBarScript.SetFillPercent(p * 100f);

        RotateWheels();
        RotatePedal();

    }

    void RotatePedal()
    {
        pedal.transform.Rotate(new Vector3(0, 0, rb.velocity.magnitude * Time.deltaTime * 360));
    }

    void RotateWheels()
    {
        wheelFront.transform.Rotate(new Vector3(0, 0, rb.velocity.magnitude * Time.deltaTime * 360));
        wheelBack.transform.Rotate(new Vector3(0, 0, rb.velocity.magnitude * Time.deltaTime * 360));
    }
}
