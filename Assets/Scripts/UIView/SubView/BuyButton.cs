using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Animator _animator;
    private TMP_Text _text;
    private bool canClick = true;
    public Action onClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canClick)
            return;
        onClick?.Invoke();
    }
    public void BuyResult(bool isPurchased)
    {
        if (!isPurchased)
        {
            DebugLog.Message("金钱不足");
            StartCoroutine(NoMoney());
            return;
        }
        DebugLog.Message("购买成功");
        _animator.SetBool("isOpen", false);
        _animator.Play(UIManager.close);

    }
    public void CloseThis()
    {
        Destroy(gameObject,0.3f);
    }

    private IEnumerator NoMoney()
    {
        _text.text = "No Money";
        canClick = false;
        yield return new WaitForSeconds(2);
        canClick = true;
        _text.text = "Locked";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //_animator.SetBool("isOpen", false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // _animator.SetBool("isOpen", true);
        // _animator.Play(UIManager.open);

    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _text = GetComponentInChildren<TMP_Text>();
    }
}
