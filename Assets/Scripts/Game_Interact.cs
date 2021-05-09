using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Interact : MonoBehaviour
{
    public string[] dialogue;
    public int stage = 0;
    UI_Manager ui;
    public enum Type { npc, truck, mom, wall }
    public Type type;
    public float distance;
    public Transform player;
    public bool npcGive = false;
    public Game_Player.Holding item;
    public Sprite momNoHands;
    public bool goingOut = false;

    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI_Manager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        if (type == Type.wall)
        {
            for (int i = 0; i < dialogue.Length; i++)
            {
                dialogue[i] = "";
            }
        }
    }

    private void Update()
    {
        if (type == Type.truck)
        {
            distance = Vector3.Distance(transform.position, player.position);
            if (distance > 3.5f)
            {
                stage = 0;
            }

        }
    }
    public void NextStage()
    {
        if (type == Type.npc || type == Type.mom)
        {
            if (stage >= dialogue.Length)
            {
                if (npcGive)
                {
                    player.GetComponent<Game_Player>().hold = item;
                }
                if (type == Type.mom)
                {
                    GetComponentInChildren<SpriteRenderer>().sprite = momNoHands;
                    player.GetComponent<Game_Player>().GetHands();
                }
                ui.ChangetText("");
                stage = 0;
            }

            else
            {
                ui.ChangetText(dialogue[stage]);
                stage += 1;
            }
        }
        else if (type == Type.truck)
        {

            if (stage >= dialogue.Length && goingOut == false)
            {
                goingOut = true;
                GameCat._.state = GameCat.State.end;
            }

            else
            {
                ui.ChangetText(dialogue[stage]);
                stage += 1;
            }

        }


    }
}
