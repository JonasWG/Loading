using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject bar;

    private HandScript leftHandScript;
    private HandScript rightHandScript;

    public float moveSpeed;

    private bool barGripped = false;
    private float maxDist;
    private float minDist;
    // Start is called before the first frame update
    void Start()
    {
        leftHandScript = leftHand.GetComponent<HandScript>();
        rightHandScript = rightHand.GetComponent<HandScript>();
    }

    // Update is called once per frame
    void Update()
    {
        RightHandMovement();
        LeftHandMovement();
        GripBar();
    }

    void RightHandMovement()
    {
        Vector3 moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVector.y += moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVector.y -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVector.x -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVector.x += moveSpeed * Time.deltaTime;
        }
        if(barGripped)
        {
            float moveDist = Vector3.Distance(leftHand.transform.position, rightHand.transform.position + moveVector);
            float potentialMoveDist = Vector3.Distance(rightHand.transform.position, rightHand.transform.position + moveVector);
            if (moveDist < maxDist)
            {
                rightHand.transform.position += moveVector;
                bar.transform.position = Vector3.Lerp(rightHand.transform.position, leftHand.transform.position, 0.5f);
                var dir = leftHand.transform.position - rightHand.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bar.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bar.transform.Rotate(0f, 0f, 180f);
            }
            else
            {
                rightHand.transform.position += moveVector;
                leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, rightHand.transform.position, potentialMoveDist);
                bar.transform.position = Vector3.Lerp(rightHand.transform.position, leftHand.transform.position, 0.5f);
                var dir = leftHand.transform.position - rightHand.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bar.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                bar.transform.Rotate(0f, 0f, 180f);
            }
        }
        else
        {
            rightHand.transform.position += moveVector;
        }
    }

    void LeftHandMovement()
    {
        Vector3 moveVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveVector.y += moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveVector.y -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveVector.x -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveVector.x += moveSpeed * Time.deltaTime;
        }
        if(barGripped)
        {
            float moveDist = Vector3.Distance(leftHand.transform.position + moveVector, rightHand.transform.position);
            float potentialMoveDist = Vector3.Distance(leftHand.transform.position, leftHand.transform.position + moveVector);

            if (moveDist < maxDist)
            {
                leftHand.transform.position += moveVector;
                bar.transform.position = Vector3.Lerp(rightHand.transform.position, leftHand.transform.position, 0.5f);
                var dir = leftHand.transform.position - rightHand.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bar.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bar.transform.Rotate(0f, 0f, 180f);
            }
            else
            {
                leftHand.transform.position += moveVector;
                rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, leftHand.transform.position, potentialMoveDist);
                bar.transform.position = Vector3.Lerp(rightHand.transform.position, leftHand.transform.position, 0.5f);
                var dir = leftHand.transform.position - rightHand.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bar.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bar.transform.Rotate(0f, 0f, 180f);
            }
        }
        else
        {
            leftHand.transform.position += moveVector;
        }
    }


    void BarGrippedMovement()
    {
        
    }
    

    void GripBar()
    {
        if (Input.GetKey(KeyCode.Space) && leftHandScript.IsTouchingBar() && rightHandScript.IsTouchingBar())
        {
            barGripped = true;
            maxDist = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
            minDist = maxDist / 2f;
        }
    }

}
