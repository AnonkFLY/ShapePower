using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputButton : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform _transform;
    private Vector2 _offset;
    private Vector2 _originPos;
    [Range(50, 200)]
    [SerializeField]
    private float maxDistance = 100;
    public Action<Vector2> onDrag;
    public EnterButton fireButton;
    public EnterButton cutoverButton;
    [SerializeField]
    private int touchID;
    private void Awake()
    {
        _transform = transform;
        maxDistance = Screen.height * (maxDistance / 600);
        _originPos = _transform.position;
    }

    private void Update()
    {
        // if (Time.timeScale <= 0)
        //     return;
        onDrag?.Invoke(_offset / maxDistance);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //_offset = _transform.position - Input.mousePosition;
        touchID = eventData.pointerId;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Time.timeScale <= 0)
            return;
        Vector2 mousePos;
        if (touchID != -1)
            mousePos = Input.GetTouch(touchID).position;
        else
            mousePos = Input.mousePosition;
        _offset = (Vector2)mousePos - _originPos;
        if (Vector3.Distance(mousePos, _originPos) > maxDistance)
        {
            _offset = _offset.normalized * maxDistance;
        }
        _transform.position = _originPos + _offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _offset = Vector3.zero;
        _transform.DOMove(_originPos, 0.3f);
    }
}
