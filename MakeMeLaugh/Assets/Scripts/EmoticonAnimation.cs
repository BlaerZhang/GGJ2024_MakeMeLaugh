using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmoticonAnimation : MonoBehaviour
{
    private float yPos;

    private void Awake()
    {
        yPos = transform.position.y;
    }

    private void OnEnable()
    {
        transform.DOLocalMoveY(10, 0);
        transform.DOLocalMoveY(yPos, 1f).SetEase(Ease.OutElastic,1,1f);
    }

    private void OnDisable()
    {
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
