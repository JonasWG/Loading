using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCat : MonoBehaviour
{
    #region uhoh
    //im sorry
    public static GameCat _;
    private void Awake()
    {
        if (_ == null)
            _ = this;
        else
            Destroy(this.gameObject);

        if (MinigameLoader._ != null && MinigameLoader._.lastSceneState == MinigameLoader.LastSceneState.SUCCESS)
            loaded = true;
    }
    #endregion

    public bool npcTalked = false;
    public bool loaded = false;
    public enum State { start, game, end }
    public State state = State.start;

    public GameObject game;
    public GameObject card;

    private void Update()
    {
        switch (state)
        {

            case State.start:
                StateStart();
                card.SetActive(true);
                game.SetActive(false);
                break;
            case State.game:
                card.SetActive(false);
                game.SetActive(true);
                break;
            case State.end:
                StateEnd();
                card.SetActive(true);
                game.SetActive(false);
                break;
        }
    }

    void StateStart()
    {
        if (Input.anyKeyDown)
        {
            state = State.game;
        }
    }
    void StateEnd()
    {
        if (Input.anyKeyDown)
        {
            MinigameLoader._.InvokeSuccess();
        }
    }

}
