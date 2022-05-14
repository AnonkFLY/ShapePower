using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleChoice : MonoBehaviour
{
    [SerializeField] private RoleBase roleData;
    [SerializeField] private Image roleImage;
    [SerializeField] private TMP_Text priceText;
    private BuyButton _buyButton;
    private Button _displayData;
    private Animator _animator;
    private Toggle _toggle;
    private RoleDataView _roleView;
    private int _roleIndex;

    private readonly int jump = Animator.StringToHash("Jump");
    private readonly int withdraw = Animator.StringToHash("Withdraw");
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(OnClick);

        _displayData = GetComponentInChildren<Button>();
        _buyButton = GetComponentInChildren<BuyButton>();

        _buyButton.onClick += BuyRole;
    }
    private void OnClick(bool isOn)
    {
        if (!isOn)
            return;
        GameManager.Instance.onRoleChange?.Invoke(roleData, _roleIndex);
    }

    public void InitChoiceView(RoleBase roleData, int index, bool isPurchased)
    {
        _roleIndex = index;
        this.roleData = roleData;
        roleImage.sprite = roleData.sprite;
        _buyButton.gameObject.SetActive(!isPurchased);
        SetPurchased(isPurchased);

        _roleView?.UpdateView(roleData);
        _toggle.group = transform.parent.GetComponent<ToggleGroup>();
    }
    private void SetPurchased(bool value)
    {

        _toggle.interactable = value;
        //_displayData.interactable = value;
        _displayData.gameObject.SetActive(value);
        if (value)
        {
            UnLockRole();
            if (priceText != null)
                Destroy(priceText.gameObject);
        }
        else
        {
            priceText.text = "Price: " + +roleData.price;
        }
    }

    public void Jump()
    {
        _animator.Play(jump);
    }
    public void WithDraw()
    {
        _animator.Play(withdraw);
    }
    private void UnLockRole()
    {

    }
    public void BuyRole()
    {
        DebugLog.Message("Buy");
        var result = GameManager.Instance.archiveManager.BuyRole(_roleIndex, roleData.price);
        if (result)
            SetPurchased(result);
        _buyButton.BuyResult(result);
    }
}
