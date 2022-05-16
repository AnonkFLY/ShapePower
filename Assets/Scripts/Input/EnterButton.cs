using System;
using UnityEngine;
using UnityEngine.UI;

public class EnterButton : MonoBehaviour
{
    public EventButton button;
    private Image _images;
    [SerializeField]
    private Color normalColor = Color.white;
    [SerializeField]
    private Color highlightedColor = Color.grey;
    private void Awake()
    {
        button = GetComponentInChildren<EventButton>();
        _images = GetComponent<Image>();
        button.onClick += OnClick;
        button.onDown += OnEnter;
        button.onExit += OnExit;
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
    }
}
