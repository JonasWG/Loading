using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    public GameObject triggerOne;
    public GameObject triggerTwo;

    public bool printLog;
    
    FillScript fillScript;

    public bool hasEnergy;
    private bool onAnim = false;
    private bool offAnim = true;
    private bool connectedToSource;

    private List<KeyValuePair<GameObject, bool>> neighbours;

    // Start is called before the first frame update
    void Start()
    {
        neighbours = new List<KeyValuePair<GameObject, bool>>();
        fillScript = GetComponentInChildren<FillScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTriggers();
    }

    public void Clicked()
    {
        transform.Rotate(Vector3.forward, -90f);
        //CheckTriggers();
    }

    public bool IsEnergized()
    {
        return hasEnergy;
    }

    public void CheckTriggers()
    {
        if (connectedToSource) return;
        bool hasConnected = false;
        foreach(KeyValuePair<GameObject, bool> n in neighbours)
        {
            if(!hasEnergy && n.Value && n.Key.GetComponent<PipeScript>().IsEnergized())
            {
                if(printLog)
                {
                    Debug.Log("Found powered neighbouhr");
                }
                hasEnergy = true;
                StartCoroutine(fillScript.TurnOn());
                onAnim = true;
                offAnim = false;
            } else if(hasEnergy && n.Value)
            {
                if (printLog)
                {
                    Debug.Log("found neightbour, already powered");
                }
                hasConnected = true;
            }
        }

        if(!hasConnected && hasEnergy)
        {
            if(printLog)
            {
                Debug.Log("Turn off");
            }
            StartCoroutine(fillScript.TurnOff());
            offAnim = true;
            onAnim = false;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(printLog)
        {
            //Debug.Log(collision.gameObject.name);
        }
        if (collision.transform.parent != null)
        {
            if (collision.transform.parent.CompareTag("Pipe"))
            {
                bool cont = false;

                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (printLog)
                    {
                        Debug.Log("Found new");
                    }
                    if (neighbours[i].Key == collision.transform.parent.gameObject)
                    {
                        neighbours[i] = new KeyValuePair<GameObject, bool>(collision.transform.parent.gameObject, true);
                        cont = true;
                        if (printLog)
                        {
                            Debug.Log("Added new");
                        }
                        break;
                    }
                }

                if(!cont)
                {
                    neighbours.Add(new KeyValuePair<GameObject, bool>(collision.transform.parent.gameObject, true));
                }
            }
            else if (collision.transform.parent.CompareTag("PipeStart"))
            {
                hasEnergy = true;
                connectedToSource = true;
                StartCoroutine(fillScript.TurnOn());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent != null)
        {
            if (collision.transform.parent.CompareTag("Pipe"))
            {
                for(int i = 0;i < neighbours.Count;i++)
                {
                    if (neighbours[i].Key == collision.transform.parent.gameObject)
                    {
                        neighbours[i] = new KeyValuePair<GameObject, bool>(collision.transform.parent.gameObject, false);
                        break;
                    }
                }
            }
            else if (collision.transform.parent.CompareTag("PipeStart"))
            {
                hasEnergy = false;
                connectedToSource = false;
                StartCoroutine(fillScript.TurnOff());
            }
        }
    }
}
