using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 click2d = new Vector2(clickPos.x, clickPos.y);

            RaycastHit2D hit = Physics2D.Raycast(click2d, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Pipe"))
                {
                    hit.collider.gameObject.GetComponent<PipeScript>().Clicked();
                }
            }
        }
    }
}
