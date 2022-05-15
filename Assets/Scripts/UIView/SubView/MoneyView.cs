using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{

    [SerializeField] private TMP_Text moneyText;
    private int value;
    private bool isAnimating;
    private void Start()
    {
        GameManager.Instance.archiveManager.onMoneyChange += ValueChange;
    }

    private void ValueChange(int value)
    {
        if (isAnimating)
            return;
        StartCoroutine(Animation(value));
    }

    private IEnumerator Animation(int changeValue)
    {
        isAnimating = true;
        var wait = new WaitForEndOfFrame();
        while (value != changeValue)
        {
            var i = (value > changeValue ? -1 : 1);
            value = (int)Mathf.Lerp(value, changeValue, 0.1f) + i;
            moneyText.text = "Money: " + value;
            yield return wait;
        }
        isAnimating = false;
    }
}
