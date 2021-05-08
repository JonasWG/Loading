using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Minigame : MonoBehaviour
{

    public string promptMsg = "Do something to load!!";
    public bool promptActive = true;
    public GameObject prompt;
    public TextMeshProUGUI promptTxt;
    public float promptTimeSpent;
    public float promptTimer = 2f;
    //minigame refereces
    public LoadingBarScript loadingBarScript;
    public GameObject minigame;

    public TextMeshProUGUI timerTxt;
    public float startTime;

    private void Start()
    {
        startTime = loadingBarScript.timeToComplete;
        minigame.SetActive(false);
        promptTxt.text = promptMsg;

    }

    // Update is called once per frame
    void Update()
    {
        //prompt ui
        if (promptActive)
        {
            promptTimeSpent += Time.deltaTime;
            if (promptTimeSpent >= promptTimer)
            {
                promptActive = false;
                prompt.SetActive(false);
                minigame.SetActive(true);
            }
        }
        //mingame ui
        else
        {
            float timer = startTime - loadingBarScript.timeSpent;
            timerTxt.text = timer.ToString("F2");
        }
    }
}
