using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeScript : MonoBehaviour
{

    public float speedModifier;
    public GameObject pedal;
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
        Vector3 currentPos = gameObject.transform.position;

        float p = (currentPos.x - startPos.x) / (endPos.x - startPos.x);
        Debug.Log(p);
        loadingBarScript.SetFillPercent(p * 100f);
    }

    void RotatePedal()
    {
        pedal.transform.Rotate(new Vector3(0, 0, rb.velocity.magnitude * Time.deltaTime * 360));
    }
}
