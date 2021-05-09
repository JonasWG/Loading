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
        SceneManager.sceneLoaded += PlaySceneMusic;
        SceneManager.sceneLoaded += SetCursorVisible;
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
        if (!CurrentScene.isLoaded)
        {
            Debug.Log("Encountered error loading scene");
        }
    }

    public void InvokeFailure()
    {
        //lastSceneState = LastSceneState.FAILURE;
        //LoadMinigame(SceneIndex);
        UI_Minigame minigameUI = GameObject.FindWithTag("UI").GetComponent<UI_Minigame>();
        minigameUI.state = UI_Minigame.State.fail;
    }

    public void InvokeSuccess()
    {
        lastSceneState = LastSceneState.SUCCESS;
        StartCoroutine(LoadNext(0.25f));
    }
    IEnumerator LoadNext(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        LoadMinigame(SceneIndex + 1);

    }


    public void InvokeRestart()
    {
        lastSceneState = LastSceneState.FAILURE;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public LastSceneState GetLastSceneState()
    {
        return lastSceneState;
    }

    void PlaySceneMusic(Scene scene, LoadSceneMode mode)
    {
        SoundController.Instance.PlayMusic(scene.name);
    }

    void SetCursorVisible(Scene scene, LoadSceneMode mode)
    {
        CursorManager.Instance.SetCursorVisible(SceneIndex % 2 != 0);
    }

}
