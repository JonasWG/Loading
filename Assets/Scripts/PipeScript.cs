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

    private List<GameObject> nbb;
    
    public bool hasEnergy;
    private bool connectedToSource;

    public int chainNum;

    private int startCount = 0;

    private bool retrigger;
    
    // Start is called before the first frame update
    void Start()
    {
        chainNum = 9999;
        nb = new Dictionary<int, GameObject>();
        nbb = new List<GameObject>();
        fillScript = GetComponentInChildren<FillScript>();
        CursorManager.Instance.SetCursorVisible(true);
        CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.ARROW);
    }

    // Update is called once per frame
    void Update()
    {
        if (startCount > 0)
        {
            //
            if (!hasEnergy)
            {
                debugLog("Turning on pipe since gained PipeStart");
                SetEnergy(true);
            }
            chainNum = 1;
        }
        int minChain = 9999;
        /*foreach(KeyValuePair<int, GameObject> n in nb)
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
            
        }*/

        foreach (GameObject gb in nbb)
        {
            var pipe = gb.GetComponent<PipeScript>();
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
        
        if(startCount == 0 && hasEnergy)
        {
           if(nbb.Count == 0)
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

        if (startCount > 0 && fillScript.getState() == FillScript.State.OFF)
        {
            StartCoroutine(fillScript.TurnOn());
            debugLog("Turned off Anim started");
        }

    }


    public void Clicked()
    {
        //transform.Rotate(Vector3.forward, -90f);
        retrigger = false;
        Vector3 targetRot = transform.rotation.eulerAngles;
        targetRot.z -= 90f;
        transform.DORotate(targetRot, 0.1f);
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

            }
            var id = collision.transform.parent.gameObject.GetInstanceID();
            
            nbb.Add(collision.transform.parent.gameObject);
            
            if (!nb.ContainsKey(id))
            {
                nb.Add(id, collision.transform.parent.gameObject);
            }
        } else if(collision.CompareTag("PipeStart"))
        {
            startCount++;
            //debugLog("Turning on pipe since gained PipeStart");
            //SetEnergy(true);
            //StartCoroutine(fillScript.TurnOn());
            //onAnim = true;
            //offAnim = false;
            //chainNum = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PipePoint"))
        {
            if(printLog)
            {
                Debug.Log("Lost connection");

            }
            var id = collision.transform.parent.gameObject.GetInstanceID();
            RemovePipe(id);
            if (nb.ContainsKey(id))
            {
              
                nb.Remove(id);
            }

        } else if(collision.CompareTag("PipeStart"))
        {
            startCount--;
            debugLog("Turning off pipe since lost PipeStart");
            //SetEnergy(false);
            //StartCoroutine(fillScript.TurnOff());
            //connectedToSource = false;
            //onAnim = false;
            //offAnim = true;
            //chainNum = 9999;
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

    void RemovePipe(int id)
    {
        int remIndex = 9999;
        for (int i = 0; i < nbb.Count; i++)
        {
            if (nbb[i].GetInstanceID() == id)
            {
                remIndex = i;
                break;
            }
        }

        if (remIndex != 9999)
        {
            nbb.RemoveAt(remIndex);
        }
    }
    
}
