using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBarScript : MonoBehaviour
{

    public GameObject BarFill;

    public float MaxScale;
    public float MinScale;
    public float TimeToComplete;

    private MinigameLoader minigameLoader;

    private float timeSpent;
    private float currentFillLevel;


    // Start is called before the first frame update
    void Start()
    {
        minigameLoader = GameObject.FindWithTag("MinigameLoader").GetComponent<MinigameLoader>();
    }

    public void SetTimer(float seconds) {
        TimeToComplete = seconds;
    }

    // Update is called once per frame
    void Update()
    {
        timeSpent += Time.deltaTime;
        if(timeSpent > TimeToComplete)
        {
            minigameLoader.InvokeFailure();
        } else if(currentFillLevel >= MaxScale)
        {
            minigameLoader.InvokeSuccess();
        }
    }

    public void FillPercent(float p)
    {
        Vector3 v = BarFill.transform.localScale;
        v.x = p / 10f;
        BarFill.transform.localScale = v;
        currentFillLevel = p / 10f;
    }

    public void ResetFill()
    {
        Vector3 v = BarFill.transform.localScale;
        v.x = 0f;
        BarFill.transform.localScale = v;
    }

    public void ChangeBarColor(Color c)
    {
        //Brin fucked this
    }

    public bool IsCompletedSuccessfully()
    {
        return currentFillLevel >= MaxScale;
    }

}
