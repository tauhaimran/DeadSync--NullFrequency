using System.Collections;
using System.Collections.Generic;
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
        transform.localScale = Vector3.one * ScaleDownTo;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = OriginalScale;
    }
}
