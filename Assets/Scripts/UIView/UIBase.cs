using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public abstract class UIBase : MonoBehaviour
{
    public UIType type;
    public float transitionTimer;
    public int index;
    public CanvasGroup canvasGroup;
    protected UIManager uiManager;
    public virtual void Open()
    {
        canvasGroup.DOFade(1, transitionTimer);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public virtual void Close()
    {
        canvasGroup.DOFade(0, transitionTimer);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    public virtual void RegisterUI(UIManager uiManager)
    {
        this.uiManager = uiManager;
        canvasGroup = GetComponent<CanvasGroup>();
    }
}
