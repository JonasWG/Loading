using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float force;
    public float bounceForce;

    public bool canFill;

    private Vector3 extents;
    public Vector3 startingPoint;
    public Vector3 endingPoint;

    private LoadingBarScript loadingBar;

    public int neededHits = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        extents = GetComponent<SpriteRenderer>().bounds.extents;

        startingPoint += extents;
        endingPoint += extents;
        
        loadingBar = GameObject.FindWithTag("LoadingBar").GetComponent<LoadingBarScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (neededHits == 0)
        {
            float xPos = transform.position.x;
            float percentageIn = (xPos - startingPoint.x) / (endingPoint.x - startingPoint.x);
            //Debug.Log(percentageIn);
            percentageIn *= 100;
            if (percentageIn > 0)
            {
                loadingBar.SetFillPercent(percentageIn);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("GolfBat"))
        {
            _rigidbody2D.AddForce(new Vector2(force,0), ForceMode2D.Impulse);
        }

        if (other.gameObject.CompareTag("LoadingBarWall"))
        {
            neededHits--;
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(new Vector2(bounceForce, 0));
            Camera.main.DOShakePosition(.35f, .2f, 15, 90f, false);
        }
    }
}
