using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FillScript : MonoBehaviour
{

    private SpriteRenderer sr;
    private Color c;

    private State currentState;
    
    public enum State
    {
        ON, OFF
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = State.OFF;
        sr = GetComponent<SpriteRenderer>();
        c = sr.color;
        //StartCoroutine(TurnOn());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator TurnOn()
    {
        currentState = State.ON;
        sr.enabled = true;

        sr.color = Color.white;

        yield return new WaitForSeconds(0.1f);

        sr.color = c;

        yield return new WaitForSeconds(0.1f);

        sr.color = Color.white;

        yield return new WaitForSeconds(0.1f);

        sr.color = c;


    }

    public IEnumerator TurnOff()
    {
        currentState = State.OFF;
        sr.color = Color.white;

        yield return new WaitForSeconds(0.1f);

        sr.color = c;

        yield return new WaitForSeconds(0.1f);

        sr.color = Color.white;

        yield return new WaitForSeconds(0.1f);

        sr.enabled = false;
    }

    public State getState()
    {
        return currentState;
    }
}
