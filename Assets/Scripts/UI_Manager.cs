using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public string dialogueText;
    public TextMeshProUGUI hud;
    public RawImage background;

    [Header("Cards")]
    public string startCard;
    public string endCard;
    public TextMeshProUGUI card;

    private void Update()
    {

        switch (GameCat._.state)
        {

            case GameCat.State.start:
                CardStart();
                break;
            case GameCat.State.game:
                Game();
                break;
            case GameCat.State.end:
                CardEnd();
                break;

        }


    }
    void CardStart()
    {
        card.text = startCard;
    }
    void Game()
    {

        if (hud.text == "")
            background.enabled = false;
        else
            background.enabled = true;

    }
    void CardEnd()
    {
        card.text = endCard;
    }

    public void ChangetText(string text)
    {
        hud.text = text;
    }
}
