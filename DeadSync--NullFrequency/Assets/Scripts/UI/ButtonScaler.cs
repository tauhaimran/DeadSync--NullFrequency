using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] float ScaleDownTo = 0.9f;
    Vector3 OriginalScale;
    void Start()
    {
        OriginalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(OriginalScale * ScaleDownTo, 0.15f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(OriginalScale, 0.15f);
    }
}
