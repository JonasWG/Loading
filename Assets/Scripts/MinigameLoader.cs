using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameLoader : MonoBehaviour
{
    public string[] SceneCollection;
    private int SceneIndex;

    private Scene CurrentScene;

    public LastSceneState lastSceneState;

    public enum LastSceneState
    {
        SUCCESS, FAILURE
    }
    public static MinigameLoader _;
    private void Awake()
    {
        if (_ == null)
            _ = this;
        else
            Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    void LoadMinigame(int i)
    {
        SceneManager.LoadScene(SceneCollection[i]);
        SceneIndex = i;
        CurrentScene = SceneManager.GetActiveScene();
        CursorManager.Instance.SetCursorVisible(i % 2 != 0);
        if (!CurrentScene.isLoaded)
        {
            Debug.Log("Encountered error loading scene");
        }
    }

    public void InvokeFailure()
    {
        lastSceneState = LastSceneState.FAILURE;
        LoadMinigame(SceneIndex + 1);
    }

    public void InvokeSuccess()
    {
        lastSceneState = LastSceneState.SUCCESS;
        LoadMinigame(SceneIndex + 1);
    }

    public LastSceneState GetLastSceneState()
    {
        return lastSceneState;
    }

}
