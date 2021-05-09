using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public GameObject triggerOne;
    public GameObject triggerTwo;

    public bool printLog;
    
    FillScript fillScript;

    private bool onAnim;
    private bool offAnim;

    private Dictionary<int, GameObject> nb;

    public bool hasEnergy;
    private bool connectedToSource;

    public int chainNum;

    // Start is called before the first frame update
    void Start()
    {
        chainNum = 9999;
        nb = new Dictionary<int, GameObject>();
        fillScript = GetComponentInChildren<FillScript>();
    }

    // Update is called once per frame
    void Update()
    {
        int minChain = 9999;
        foreach(KeyValuePair<int, GameObject> n in nb)
        {
            var pipe = n.Value.GetComponent<PipeScript>();
            if (pipe.ChainNum() < minChain)
            {
                minChain = pipe.ChainNum();
            }
            if (!hasEnergy && pipe.IsEnergized() && pipe.ChainNum() < chainNum - 1)
            {
                SetEnergy(true);
                chainNum = pipe.ChainNum() + 1;
                break;
            }
            
        }

        if(!connectedToSource && hasEnergy)
        {
           if(nb.Count == 0)
           {
                SetEnergy(false);
                chainNum = 9999;
           } else
           {
                if(minChain >= chainNum)
                {
                    SetEnergy(false);
                    chainNum = 9999;
                }
           }
        }

        HandleAnim();



    }

    public void debugLog(string l)
    {
        if(printLog)
        {
            Debug.Log(l);
        }
    }

    public void HandleAnim()
    {
        if(IsEnergized() && fillScript.getState() == FillScript.State.OFF)
        {
            StartCoroutine(fillScript.TurnOn());
            debugLog("Turned on Anim started");
        }

        if(!IsEnergized() && fillScript.getState() == FillScript.State.ON)
        {
            StartCoroutine(fillScript.TurnOff());
            debugLog("Turned off Anim started");
        }

        if (connectedToSource && fillScript.getState() == FillScript.State.OFF)
        {
            StartCoroutine(fillScript.TurnOn());
            debugLog("Turned off Anim started");
        }

    }


    public void Clicked()
    {
        transform.Rotate(Vector3.forward, -90f);
    }

    public bool IsEnergized()
    {
        return hasEnergy;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PipePoint"))
        {
            if (printLog)
            {
                Debug.Log("Gained connection");
                Debug.Log(collision.transform.parent.gameObject.GetInstanceID());

            }
            var id = collision.transform.parent.gameObject.GetInstanceID();

            if (!nb.ContainsKey(id))
            {
                nb.Add(id, collision.transform.parent.gameObject);
            }
        } else if(collision.CompareTag("PipeStart"))
        {
            debugLog("Turning on pipe since gained PipeStart");
            SetEnergy(true);
            //StartCoroutine(fillScript.TurnOn());
            connectedToSource = true;
            onAnim = true;
            offAnim = false;
            chainNum = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PipePoint"))
        {
            if(printLog)
            {
                Debug.Log("Lost connection");
                Debug.Log(collision.transform.parent.gameObject.GetInstanceID());

            }
            var id = collision.transform.parent.gameObject.GetInstanceID();

            if (nb.ContainsKey(id))
            {
                nb.Remove(id);
            }

        } else if(collision.CompareTag("PipeStart"))
        {
            debugLog("Turning off pipe since lost PipeStart");
            SetEnergy(false);
            //StartCoroutine(fillScript.TurnOff());
            connectedToSource = false;
            onAnim = false;
            offAnim = true;
            chainNum = 9999;
        }
    }



    public void SetEnergy(bool val)
    {
        hasEnergy = val;
    }

    public int ChainNum()
    {
        return chainNum;
    }
}
