using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderView
{
    private Image _valueImage;
    private Image _followImage;
    private TMP_Text _valueText;
    public float followSpeed = 0.35f;
    public float waitTimer = 0.6f;
    public float addSpeed = 0.8f;
    public bool isSegmented;

    private float _value = 1;
    private float _followValue;
    private bool _onStop;
    private bool _isFollowDone = true;
    private bool _isAddDone = true;

    private WaitForEndOfFrame wait;
    public SliderView(Transform parent)
    {
        _valueImage = parent.Find("Value").GetComponent<Image>();
        _followImage = parent.Find("ValueFollow").GetComponent<Image>();

        _valueText = _valueImage.GetComponentInChildren<TMP_Text>();
        wait = new WaitForEndOfFrame();
    }
    public void Reset()
    {
        _valueImage.fillAmount = 1;
        _followImage.fillAmount = 1;
    }

    public void SetValue(float value)
    {
        value = Mathf.Clamp01(value);
        var isRemove = _value >= value;
        _value = value;

        if (isRemove)
            OnRemove();
        else
            OnAdd();
    }
    public void SetValue(float currentValue, float maxValue)
    {
        if (_valueText)
            _valueText.text = currentValue.ToString("f1") + "/" + maxValue.ToString("f1");
        SetValue(currentValue / maxValue);
    }
    private void OnRemove()
    {
        if(_value<=0)
            return;
        _onStop = true;
        if (!_isAddDone)
        {
            _followImage.fillAmount = _valueImage.fillAmount;
            _isAddDone = true;
        }
        _valueImage.fillAmount = _value;
        if (_isFollowDone)
            _followImage.StartCoroutine(SilderFollowChange());
    }
    private void OnAdd()
    {
        _followImage.fillAmount = _valueImage.fillAmount;
        if (!_isFollowDone)
        {
            _isFollowDone = true;
        }
        if (_isAddDone)
            _valueImage.StartCoroutine(SilderAddCoroutine());
    }
    private IEnumerator SilderFollowChange()
    {
        _isFollowDone = false;
        var waitStop = new WaitForSeconds(waitTimer);
        while (!_isFollowDone)
        {
            if (_onStop)
            {
                yield return waitStop;
                _onStop = false;
            }
            if (_followImage.fillAmount > _value)
                _followImage.fillAmount -= followSpeed * Time.deltaTime;
            else
                _isFollowDone = true;
            yield return wait;
        }
        _followImage.fillAmount = _valueImage.fillAmount;
    }
    private IEnumerator SilderAddCoroutine()
    {
        _isAddDone = isSegmented;
        while (!_isAddDone)
        {
            if (_valueImage.fillAmount < _value)
                _valueImage.fillAmount += addSpeed * Time.deltaTime;
            else
            {
                _isAddDone = true;
            }
            yield return wait;
        }
        _valueImage.fillAmount = _value;
        _followImage.fillAmount = _valueImage.fillAmount;
    }
}
