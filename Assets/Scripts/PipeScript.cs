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
    

    private List<GameObject> nbb;
    
    public bool hasEnergy;

    public int chainNum;

    private int startCount;

    
    // Start is called before the first frame update
    void Start()
    {
        chainNum = 9999;
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
                DebugLog("Turning on pipe since gained PipeStart");
                SetEnergy(true);
            }
            chainNum = 1;
        }
        int minChain = 9999;

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

    public void DebugLog(string l)
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
            SoundController.Instance.PlayRandom("Water1", "Water2", "Water3", "Water4", "Water5", "Water6");
            DebugLog("Turned on Anim started");
        }

        if(!IsEnergized() && fillScript.getState() == FillScript.State.ON)
        {
            StartCoroutine(fillScript.TurnOff());
            DebugLog("Turned off Anim started");
        }

        if (startCount > 0 && fillScript.getState() == FillScript.State.OFF)
        {
            StartCoroutine(fillScript.TurnOn());
            DebugLog("Turned off Anim started");
        }

    }


    public void Clicked()
    {
        Vector3 targetRot = transform.rotation.eulerAngles;
        targetRot.z -= 90f;
        transform.DORotate(targetRot, 0.1f);
        SoundController.Instance.PlayRandom("Pipe1", "Pipe2", "Pipe3", "Pipe4", "Pipe5");
    }

    public bool IsEnergized()
    {
        return hasEnergy;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PipePoint"))
        {
            DebugLog("Gained connection");
            var gb = collision.transform.parent.gameObject;
            nbb.Add(gb);
        } else if(collision.CompareTag("PipeStart"))
        {
            startCount++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PipePoint"))
        {
            DebugLog("Lost connection");
            var id = collision.transform.parent.gameObject.GetInstanceID();
            RemovePipe(id);
        } else if(collision.CompareTag("PipeStart"))
        {
            startCount--;
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
