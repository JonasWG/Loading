using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfMinigame : MonoBehaviour
{
    private GameObject golfBat;
    private bool clicking;

    private Bounds batBounds;
    
    // Start is called before the first frame update
    void Start()
    {
        golfBat = GameObject.FindWithTag("GolfBat");
        batBounds = golfBat.GetComponent<SpriteRenderer>().bounds;
        golfBat.SetActive(false);
        CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.GOLF);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            mousePos.y += batBounds.extents.y;
            mousePos.x -= batBounds.extents.x;
            golfBat.transform.position = mousePos;
            golfBat.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            golfBat.SetActive(false);
        }
    }
}
