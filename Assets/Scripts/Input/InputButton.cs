using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputButton : MonoBehaviour
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

}
