using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GolfFlag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GolfBall"))
        {
            if (DOTween.IsTweening(transform) == false)
                transform.DORotate(new Vector3(0, 0, 360f), 0.5f, RotateMode.LocalAxisAdd).SetEase(Ease.OutBack);
        }
    }
}
