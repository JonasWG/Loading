using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeBar : MonoBehaviour
{
    private bool isOver;
    private Vector2 clickedAt;

    private SpriteRenderer spriteRenderer;

    private BoxCollider2D boxCollider2d;
    private float sizeDiff = 0.54f;
    private LoadingBarScript loadingBarScript;

    private bool resized;

    public CursorManager.CursorType cursorType;

    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        loadingBarScript = GameObject.FindWithTag("LoadingBar").GetComponent<LoadingBarScript>();
        //Cursor.SetCursor(Texture2D.normalTexture, Vector2.zero, cur);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        CursorManager.Instance.SetActiveCursorType(cursorType);
    }


    private void OnMouseOver()
    {
        isOver = true;
    }

    private void OnMouseExit()
    {
        isOver = false;
        CursorManager.Instance.SetActiveCursorType(CursorManager.CursorType.ARROW);
    }

    private void OnMouseUp()
    {
        boxCollider2d.size /= 2;
    }

    private void OnMouseDrag()
    {
        if (isOver)
        {
            Vector2 size = spriteRenderer.size;
            Vector2 newClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 deltaPos = newClick - clickedAt;
            clickedAt = newClick;
            if(deltaPos == Vector2.zero)
                return;
            boxCollider2d.offset += Vector2.right * deltaPos.x / 2f;
            spriteRenderer.size = new Vector2(size.x + deltaPos.x / 2f, size.y);
            
            loadingBarScript.SetFillPercent((spriteRenderer.size.x - 0.54f) * 10);
        }
    }

    private void OnMouseDown()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 pos = Camera.main.ScreenToWorldPoint(mouse);
        clickedAt = pos;
        boxCollider2d.size *= 2;
    }

    /*
    private void OnMouseUp()
    {
        clickedAt = Vector2.zero;
    }*/
}
