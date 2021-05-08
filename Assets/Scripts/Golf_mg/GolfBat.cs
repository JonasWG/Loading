using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class GolfBat : MonoBehaviour
{
    private Quaternion initialRotation;
    public float rotationDuration;
    public float rotationTarget;
    public Ease rotationEase;
    
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(!DOTween.IsTweening(this.transform))
                this.transform.DORotate(new Vector3(0, 0, rotationTarget), rotationDuration, RotateMode.Fast).SetEase(rotationEase);
        }
        
    }

    private void OnDisable()
    {
        DOTween.Kill(this.transform);
        this.transform.rotation = initialRotation;
    }
    
}
