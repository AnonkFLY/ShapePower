using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Range(50, 200)]
    [SerializeField]
    private float maxDistance = 100;
    private Vector2 _offset;
    private Vector2 _originPos;
    private Transform _child;
    private InputButton _moveButton;
    private Transform _moveTransform;
    private CanvasGroup _canvasGroup;
    private int touchID = -1;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
        _child.position = eventData.position;
        _originPos = _child.position;
        touchID = eventData.pointerId;
    }

    public void OnDrag(PointerEventData eventData)
    {
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
        _moveTransform.position = _originPos + _offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _offset = Vector3.zero;
        _moveTransform.DOMove(_originPos, 0.3f);
    }


    void Start()
    {
        maxDistance = Screen.height * (maxDistance / 600);
        _child = transform.GetChild(0);
        _canvasGroup = _child.GetComponent<CanvasGroup>();
        _moveButton = GetComponentInChildren<InputButton>();
        _moveTransform = _moveButton.transform;
    }
    private void Update()
    {
        _moveButton.onDrag?.Invoke(_offset / maxDistance);
    }


}
