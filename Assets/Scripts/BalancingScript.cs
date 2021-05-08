using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancingScript : MonoBehaviour
{

    public float fillVariable = 5.0f;
    public float balanceFactor = 28.5f;
    public float recoveryStrength = 2.5f;
    public float difficulty = 30.0f;
    
    
    private Rigidbody2D rb;
    private LoadingBarScript loadingBarScript;
    
    private bool fell;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        loadingBarScript = GetComponent<LoadingBarScript>();
    }

    // Update is called once per frame
    void Update()
    {
        AddRandomRotation();
        HandleKeyboardPresses();
        CheckFall();
        FillBar();
    }

    void AddRandomRotation()
    {
        Vector3 currentRot = transform.rotation.eulerAngles;
        var angle = transform.eulerAngles.z;// * Mathf.Rad2Deg * 2f;
        if (angle > 180)
        {
            angle -= 360;
        }
        
        float val = Random.Range(1.5f, 4f);

        currentRot.z += val * difficulty * (angle/30f) * Time.deltaTime;



        //currentRot.z += newVal * Time.deltaTime;
        transform.rotation = Quaternion.Euler(currentRot);   
    }
    
    void HandleKeyboardPresses()
    {
        float rot = 0;
        if (Input.GetKey(KeyCode.A))
        {
            rot += balanceFactor * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rot -= balanceFactor * Time.deltaTime;
        }

        var angle = transform.eulerAngles.z;// * Mathf.Rad2Deg * 2f;
        if (angle > 180)
        {
            angle -= 360;
        }
        
        if (rot > 0 && angle < -10f)
        {
            rot *= recoveryStrength;
        } else if (rot < 0 && angle > 10f)
        {
            rot *= recoveryStrength;
        }
        
        Vector3 currentRot = transform.rotation.eulerAngles;
        currentRot.z += rot;
        transform.rotation = Quaternion.Euler(currentRot);
    }

    void CheckFall()
    {
        var angle = transform.eulerAngles.z;// * Mathf.Rad2Deg * 2f;
        if (angle > 180)
        {
            angle -= 360;
        }
        if (angle < -30f || angle > 30f)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            fell = true;
        }

    }

    void FillBar()
    {
        if (fell) return;
        
        loadingBarScript.AddFillPercent(fillVariable * Time.deltaTime);
        
        
    }
}
