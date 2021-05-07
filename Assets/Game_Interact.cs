using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Interact : MonoBehaviour
{
    public string[] dialogue;
    public int stage = 0;
    UI_Manager ui;
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI_Manager>();
    }

    // Update is called once per frame
    public void NextStage()
    {
        if (stage >= dialogue.Length)
        {
            ui.ChangetText("");
            stage = 0;
        }
        else
        {
            ui.ChangetText(dialogue[stage]);
            stage += 1;
        }

    }

}
