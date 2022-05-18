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
    public Action<Vector2> onDrag;
    public EnterButton fireButton;
    public EnterButton cutoverButton;

}
