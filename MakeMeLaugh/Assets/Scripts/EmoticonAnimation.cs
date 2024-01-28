using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EmoticonAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DOLocalMoveY(10, 0);
        transform.DOLocalMoveY(0, 1f).SetEase(Ease.OutElastic,1,1f);
    }
}
