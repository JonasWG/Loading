using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    public Sprite heldSprite;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject bar;
    public float moveSpeed;
    public float shakeModifier;

    private HandScript leftHandScript;
    private HandScript rightHandScript;
    private LoadingBarScript loadingBarScript;


    private bool barGripped;
    private float maxDist;
    private float minDist;

    private Vector3 prevBarAngle;

    // Start is called before the first frame update
    void Start()
    {
        leftHandScript = leftHand.GetComponent<HandScript>();
        rightHandScript = rightHand.GetComponent<HandScript>();
        loadingBarScript = bar.GetComponent<LoadingBarScript>();
        prevBarAngle = bar.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        RightHandMovement();
        LeftHandMovement();
        GripBar();
        MeasureBarShake();
        UpdateSprite();
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
        if (barGripped)
        {
            float moveDist = Vector3.Distance(leftHand.transform.position, rightHand.transform.position + moveVector);
            float potentialMoveDist = Vector3.Distance(rightHand.transform.position, rightHand.transform.position + moveVector);
            if (moveDist < maxDist && moveDist > minDist)
            {
                rightHand.transform.position += moveVector;
                bar.transform.position = Vector3.Lerp(rightHand.transform.position, leftHand.transform.position, 0.5f);
                var dir = leftHand.transform.position - rightHand.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bar.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bar.transform.Rotate(0f, 0f, 180f);
            }
            else if (moveDist >= maxDist)
            {
                rightHand.transform.position += moveVector;
                leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, rightHand.transform.position, potentialMoveDist);
                bar.transform.position = Vector3.Lerp(rightHand.transform.position, leftHand.transform.position, 0.5f);
                var dir = leftHand.transform.position - rightHand.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bar.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                bar.transform.Rotate(0f, 0f, 180f);
            }
            else
            {
                rightHand.transform.position += moveVector;
                leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, rightHand.transform.position, potentialMoveDist * -1f);
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
        if (barGripped)
        {
            float moveDist = Vector3.Distance(leftHand.transform.position + moveVector, rightHand.transform.position);
            float potentialMoveDist = Vector3.Distance(leftHand.transform.position, leftHand.transform.position + moveVector);

            if (moveDist < maxDist && moveDist > minDist)
            {
                leftHand.transform.position += moveVector;
                bar.transform.position = Vector3.Lerp(rightHand.transform.position, leftHand.transform.position, 0.5f);
                var dir = leftHand.transform.position - rightHand.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bar.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bar.transform.Rotate(0f, 0f, 180f);
            }
            else if (moveDist >= maxDist)
            {
                leftHand.transform.position += moveVector;
                rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, leftHand.transform.position, potentialMoveDist);
                bar.transform.position = Vector3.Lerp(rightHand.transform.position, leftHand.transform.position, 0.5f);
                var dir = leftHand.transform.position - rightHand.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bar.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bar.transform.Rotate(0f, 0f, 180f);
            }
            else
            {
                leftHand.transform.position += moveVector;
                rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, leftHand.transform.position, potentialMoveDist * -1f);
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


    void MeasureBarShake()
    {
        Vector3 barAngle = bar.transform.rotation.eulerAngles;
        float diff = Vector3.Distance(prevBarAngle, barAngle);
        loadingBarScript.AddFillPercent(diff * shakeModifier * Time.deltaTime);
        prevBarAngle = barAngle;
    }

    void GripBar()
    {
        if (Input.GetKey(KeyCode.Space) && leftHandScript.IsTouchingBar() && rightHandScript.IsTouchingBar())
        {
            barGripped = true;
            maxDist = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
            minDist = maxDist * 0.8f;
        }
    }

    void UpdateSprite()
    {
        if (barGripped)
        {
            leftHand.GetComponentInChildren<SpriteRenderer>().sprite = heldSprite;
            rightHand.GetComponentInChildren<SpriteRenderer>().sprite = heldSprite;
        }
    }

}
