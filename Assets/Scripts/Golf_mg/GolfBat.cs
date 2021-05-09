using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEditor.U2D.Path;
using UnityEngine;

public class GolfBat : MonoBehaviour
{
    private Quaternion initialRotation;
    public float rotationDuration;
    public float rotationTarget;
    public Ease rotationEase;

    private PolygonCollider2D _polygonCollider2D;
    
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = this.transform.rotation;
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
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
        _polygonCollider2D.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        StartCoroutine(DeactivateCollision());
    }

    private IEnumerator DeactivateCollision()
    {
        _polygonCollider2D.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _polygonCollider2D.enabled = true;
    }
}
