using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Minigame : MonoBehaviour
{
    public enum State { prompt, active, fail }
    public State state = State.prompt;
    [Header("Prompt")]
    public GameObject prompt;

    public string promptMsg = "Do something to load!!";
    public TextMeshProUGUI promptTxt;
    public float promptTimeSpent;
    public float promptTimer = 2f;

    //minigame refereces
    [Header("Minigame")]
    public GameObject minigame;
    public LoadingBarScript loadingBarScript;
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

        if (SceneManager.GetActiveScene().name == "Golf_mg")
        {
            SetCursor(true, CursorManager.CursorType.GOLF);
        }
        else if (SceneManager.GetActiveScene().name == "Stretching_mg")
        {
            SetCursor(true, CursorManager.CursorType.ARROW);
        }
        else if (SceneManager.GetActiveScene().name == "Biking_mg")
        {
            SetCursor(false, CursorManager.CursorType.ARROW);
        }
        else if (SceneManager.GetActiveScene().name == "Balancing_mg")
        {
            SetCursor(false, CursorManager.CursorType.ARROW);
        }
        else if (SceneManager.GetActiveScene().name == "Fishing_mg")
        {
            SetCursor(false, CursorManager.CursorType.ARROW);
        }
        else if (SceneManager.GetActiveScene().name == "Waterpipe_mg")
        {
            SetCursor(false, CursorManager.CursorType.ARROW);
        }
        else if (SceneManager.GetActiveScene().name == "Shaking_mg")
        {
            SetCursor(false, CursorManager.CursorType.ARROW);
        }
        else
        {
            SetCursor(false, CursorManager.CursorType.ARROW);
        }

    }

    void SetCursor(bool visible, CursorManager.CursorType cursorType)
    {
        CursorManager.Instance.SetCursorVisible(visible);
        CursorManager.Instance.SetActiveCursorType(cursorType);
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
                state = State.active;
            }
        }
        //mingame ui
        else if (state == State.active)
        {
            prompt.SetActive(false);
            minigame.SetActive(true);
            failScreen.SetActive(false);

            float timer = startTime - loadingBarScript.timeSpent;
            timerTxt.text = "Loading completes in:\n" + timer.ToString("F1");
        }
        //failure ui
        else if (state == State.fail)
        {
            prompt.SetActive(false);
            minigame.SetActive(false);
            failScreen.SetActive(true);

            failTimer -= Time.deltaTime;
            if (failTimer > 3)
                failTimeDisplay.text = "Loading failed!";

            else
            {
                failTimeDisplay.text = "Retrying in: " + failTimer.ToString("F1") + " !";

            }
            if (failTimer <= 0)
            {
                MinigameLoader._.InvokeRestart();
            }
        }
    }
}
