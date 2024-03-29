using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Player : MonoBehaviour
{
    public string[] selfThoughts;
    public int stage = 0;
    public enum State { thought, normal }
    public State state;
    public bool canAct = true;
    public float moveTimer;
    public float moveTimeCheck = 0.1f;
    public Vector2 input;
    public LayerMask groundLayer;
    public enum Holding { none, bike, golf, rod, stretch }
    public Holding hold = Holding.none;
    public SpriteRenderer holdingSprite;
    public Sprite bike;
    public Sprite golf;
    public Sprite rod;
    public Sprite stretch;


    UI_Manager ui;
    public Sprite playerHands;
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.thought)
        {
            if (Input.anyKeyDown)
            {
                stage++;
                if (stage >= selfThoughts.Length)
                {
                    state = State.normal;
                    ui.ChangetText("");

                }
            }
            if (stage <= selfThoughts.Length - 1)
                ui.ChangetText(selfThoughts[stage]);
        }

        if (state == State.normal)
        {
            ShowHolding();
            //Method to draw the ray in scene for debug purpose
            Debug.DrawRay(transform.position, input, Color.red);

            if (canAct == true)
            {
                input.x = Input.GetAxisRaw("Horizontal");
                input.y = Input.GetAxisRaw("Vertical");

                var right = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
                var down = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
                var left = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
                var up = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);


                //right
                if (right)
                {
                    if (!WallAhead(new Vector2(1, 0))) Move(1, 0);
                    InteractionCheck(new Vector2(1, 0));
                }
                //down
                else if (down)
                {
                    if (!WallAhead(new Vector2(0, -1))) Move(0, -1);
                    InteractionCheck(new Vector2(0, -1));
                }
                //left
                else if (left)
                {
                    if (!WallAhead(new Vector2(-1, 0))) Move(-1, 0);
                    InteractionCheck(new Vector2(-1, 0));
                }
                //up
                else if (up)
                {
                    if (!WallAhead(new Vector2(0, 1))) Move(0, 1);
                    InteractionCheck(new Vector2(-1, 0));
                }
            }

            if (canAct == false)
            {
                moveTimer += Time.deltaTime;
                if (moveTimer >= moveTimeCheck)
                {
                    moveTimer = 0;
                    canAct = true;
                }
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
        canAct = false;
    }
    void InteractionCheck(Vector2 direction)
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
            Game_Interact interaction = hit.collider.gameObject.GetComponent<Game_Interact>();
            if (interaction != null)
            {
                interaction.NextStage();
                canAct = false;
            }
        }
    }

    public void GetHands()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = playerHands;
    }
    void ShowHolding()
    {
        switch (hold)
        {
            case Holding.none:
                holdingSprite.sprite = null;
                break;
            case Holding.bike:
                holdingSprite.sprite = bike;
                break;
            case Holding.golf:
                holdingSprite.sprite = golf;
                break;
            case Holding.rod:
                holdingSprite.sprite = rod;
                break;
            case Holding.stretch:
                holdingSprite.sprite = stretch;
                break;
        }
    }
}
