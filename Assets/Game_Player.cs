using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Player : MonoBehaviour
{
    public bool canMove = true;
    public float moveTimer;
    public float moveTimeCheck = 0.1f;
    public Vector2 input;
    public LayerMask groundLayer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Method to draw the ray in scene for debug purpose
        Debug.DrawRay(transform.position, input, Color.red);

        if (canMove == true)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //right
            if (input == new Vector2(1, 0))
            {
                if (!WallAhead(new Vector2(1, 0))) Move(1, 0);
            }
            //down
            else if (input == new Vector2(0, -1))
            {
                if (!WallAhead(new Vector2(0, -1))) Move(0, -1);
            }
            //left
            else if (input == new Vector2(-1, 0))
            {
                if (!WallAhead(new Vector2(-1, 0))) Move(-1, 0);

            }
            //up
            else if (input == new Vector2(0, 1))
            {
                if (!WallAhead(new Vector2(0, -1))) Move(0, 1);
            }
        }

        if (canMove == false)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer >= moveTimeCheck)
            {
                moveTimer = 0;
                canMove = true;
            }
        }
    }
    bool WallAhead(Vector2 direction)
    {
        //Length of the ray
        float laserLength = 1;

        //Get the first object hit by the ray
        RaycastHit2D hit = Physics2D.Raycast(transform.position, input, laserLength, groundLayer);

        print("Sending a ray in " + direction);


        if (hit.collider != null)
        {
            //Hit something, print the tag of the object
            Debug.Log("Hitting: " + hit.collider.tag);
            return true;
        }
        else return false;
    }
    void Move(int x, int y)
    {
        transform.position += new Vector3(x, y, 0);
        canMove = false;
    }
}
