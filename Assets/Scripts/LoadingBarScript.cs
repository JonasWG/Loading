using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LoadingBarScript : MonoBehaviour
{

    public float maxScale;
    public float minScale;
    public float timeSpent;

    public float timeToComplete;

    private MinigameLoader minigameLoader;
    private SpriteRenderer spriteRenderer;

    public float currentFillLevel;
    UI_Minigame UI_Minigame;
    bool onScreen = true;
    bool completed = false;


    // Start is called before the first frame update
    void Start()
    {
        minigameLoader = GameObject.FindWithTag("MinigameLoader").GetComponent<MinigameLoader>();
        spriteRenderer = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        UI_Minigame = GameObject.FindWithTag("UI").GetComponent<UI_Minigame>();
    }

    public void SetTimer(float seconds)
    {
        timeToComplete = seconds;
    }

    // Update is called once per frame
    void Update()
    {

        timeSpent += Time.deltaTime;
        if (timeSpent > timeToComplete)
        {
            minigameLoader.InvokeFailure();
        }
        else if (currentFillLevel >= maxScale && !completed)
        {
            completed = true;
            minigameLoader.InvokeSuccess();
        }

        if (onScreen && transform.position.y < -8)
        {
            minigameLoader.InvokeFailure();
            onScreen = false;
        }
    }

    public void SetFillPercent(float p)
    {
        var fill = p / 10f;
        if (fill > maxScale)
        {
            spriteRenderer.size = new Vector2(maxScale, 1);
        }
        else
        {
            Vector2 v = spriteRenderer.size;
            v.x = fill;
            spriteRenderer.size = v;
        }
        currentFillLevel = fill;
    }

    public void AddFillPercent(float p)
    {
        if (currentFillLevel >= maxScale)
        {
            return;
        }
        var fill = p / 10f;
        currentFillLevel += fill;
        if (currentFillLevel >= maxScale)
        {
            spriteRenderer.size = new Vector2(maxScale, 0);
        }
        else
        {
            if (spriteRenderer)
                spriteRenderer.size += new Vector2(fill, 0);
        }
    }

    public void ResetFill()
    {
    }

    public void ChangeBarColor(Color c)
    {
        //Brin fucked this
    }

    public bool IsCompletedSuccessfully()
    {
        return currentFillLevel >= maxScale;
    }

    private void OnBecameInvisible()
    {
    }

}
