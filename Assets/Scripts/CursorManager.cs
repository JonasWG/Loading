using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance
    {
        get;
        private set;
    }
    
    [SerializeField] private bool visible;

    [SerializeField] private List<CursorAnimation> cursorAnimations;

    private CursorAnimation _cursorAnimation;

    private int _currentFrame;
    private float _frameTimer;
    private int _frameCount;

    
    public enum CursorType
    {
        ARROW,
        STRETCH_HORIZONTAL,
        STRETCH_VERTICAL,
        GOLF
    }

    private void Awake()
    {
        if (Instance != null)
            GameObject.Destroy(this);
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        SetActiveCursorType(CursorType.ARROW);
    }

    void Update()
    {
        if (visible)
            Cursor.visible = true;
        else
            Cursor.visible = false;
        
        _frameTimer -= Time.deltaTime;
        if (_frameTimer <= 0f)
        {
            _frameTimer += _cursorAnimation.frameRate;
            _currentFrame = (_currentFrame + 1) % _frameCount;
            Cursor.SetCursor(_cursorAnimation.textureArray[_currentFrame], _cursorAnimation.offset, CursorMode.Auto);
        }
        
        if(Input.GetKeyDown(KeyCode.T)) SetActiveCursorAnimation(cursorAnimations[0]);
        if(Input.GetKeyDown(KeyCode.Y)) SetActiveCursorAnimation(cursorAnimations[1]);
        
    }

    public void SetActiveCursorType(CursorType cursorType)
    {
        SetActiveCursorAnimation(GetCursorAnimation(cursorType));
    }

    private CursorAnimation GetCursorAnimation(CursorType cursorType)
    {
        foreach (CursorAnimation cursorAnimation in cursorAnimations)
        {
            if (cursorAnimation.cursorType == cursorType)
                return cursorAnimation;
        }

        return null;
    }

    private void SetActiveCursorAnimation(CursorAnimation cursorAnimation)
    {
        this._cursorAnimation = cursorAnimation;
        _currentFrame = 0;
        _frameTimer = cursorAnimation.frameRate;
        _frameCount = cursorAnimation.textureArray.Length;
    }

    public void SetCursorVisible(bool visibleNew)
    {
        this.visible = visibleNew;
    }

    [System.Serializable]
    public class CursorAnimation
    {
        public CursorType cursorType;
        public Texture2D[] textureArray;
        public float frameRate;
        public Vector2 offset;
    }
}
