using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public string dialogueText;
    public TextMeshProUGUI hud;

    void Start()
    {

    }

    public void ChangetText(string text)
    {
        hud.text = text;
    }
}
