using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Minigame : MonoBehaviour
{

    public string promptMsg = "Do something to load!!";
    public bool promptActive = true;
    public enum State { prompt, active, fail }
    public State state = State.prompt;
    public GameObject prompt;
    public TextMeshProUGUI promptTxt;
    public float promptTimeSpent;
    public float promptTimer = 2f;
    //minigame refereces
    public LoadingBarScript loadingBarScript;
    public GameObject minigame;

    public TextMeshProUGUI timerTxt;
    public float startTime;
    [Header("Failure")]
    public GameObject failScreen;
    public float failTimer = 3;
    public TextMeshProUGUI failTimeDisplay;

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
        if (state == State.prompt)
        {
            prompt.SetActive(true);
            minigame.SetActive(false);
            failScreen.SetActive(false);

            promptTimeSpent += Time.deltaTime;
            if (promptTimeSpent >= promptTimer)
            {
                promptActive = false;
                prompt.SetActive(false);
                minigame.SetActive(true);
            }
        }
        //mingame ui
        else if (state == State.active)
        {
            prompt.SetActive(false);
            minigame.SetActive(true);
            failScreen.SetActive(false);

            float timer = startTime - loadingBarScript.timeSpent;
            timerTxt.text = timer.ToString("F2");
        }
        else if (state == State.fail)
        {
            prompt.SetActive(false);
            minigame.SetActive(false);
            failScreen.SetActive(true);

            failTimer -= Time.deltaTime;
            failTimeDisplay.text = "Restarting in: " + failTimer.ToString("F1") + " !";
            if (failTimer <= 0)
            {
                MinigameLoader._.InvokeFailure();
            }
        }
    }
}
