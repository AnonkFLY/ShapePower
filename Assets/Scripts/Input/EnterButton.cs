using System;
using UnityEngine;
using UnityEngine.UI;

public class EnterButton : MonoBehaviour
{
    private EventButton _button;
    public event Action onClickEvent;
    private Image _images;
    [SerializeField]
    private Color normalColor = Color.white;
    [SerializeField]
    private Color highlightedColor = Color.grey;
    private void Awake()
    {
        _button = GetComponentInChildren<EventButton>();
        _images = GetComponent<Image>();
        _button.onClick += OnClick;
        _button.onDown += OnEnter;
        _button.onExit += OnExit;
    }
    private void OnEnter()
    {
        _images.color = highlightedColor;
    }
    private void OnExit()
    {
        _images.color = normalColor;
    }
    private void OnClick()
    {
        _images.color = normalColor;    
        onClickEvent?.Invoke();
        print("Click");
    }
}
