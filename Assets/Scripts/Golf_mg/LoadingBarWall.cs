using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBarWall : MonoBehaviour
{
    public int neededHits;

    private BoxCollider2D _boxCollider2D;
    public SpriteRenderer wallRenderer;
    public Sprite wall3;
    public Sprite wall2;
    public Sprite wall1;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider2D = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (neededHits == 2)
            wallRenderer.sprite = wall2;
        if (neededHits == 1)
            wallRenderer.sprite = wall1;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            neededHits--;
            if (neededHits <= 0)
            {
                BreakWall();
                wallRenderer.sprite = null;
                //other.gameObject.GetComponent<GolfBall>().canFill = true;
            }
        }
    }

    private void BreakWall()
    {
        _boxCollider2D.enabled = false;
        //something with sprites
    }
}
