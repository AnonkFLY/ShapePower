using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
{
    public Action onClick;
    public Action onExit;
    public Action onDown;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData == null)
            return;
        onClick?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData == null)
            return;
        onDown?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData == null)
            return;
        onExit?.Invoke();
    }
}
