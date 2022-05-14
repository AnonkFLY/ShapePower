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
    private ShapePowerArchive archiveManager;
    private readonly int jump = Animator.StringToHash("Jump");
    private readonly int showBriefly = Animator.StringToHash("ShowBriefly");
    private readonly int hideBriefly = Animator.StringToHash("HideBriefly");
    private readonly int withdraw = Animator.StringToHash("Withdraw");
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(OnClick);

        _displayData = GetComponentInChildren<Button>();
        _buyButton = GetComponentInChildren<BuyButton>();

        _buyButton.onClick += BuyRole;
        _displayData.onClick.AddListener(ShowBriefly);
        _roleView = GetComponentInChildren<RoleDataView>();
    }
    void Start()
    {
        archiveManager = GameManager.Instance.archiveManager;
    }

    private void ShowBriefly()
    {
        _animator.Play(showBriefly);
        _displayData.onClick.RemoveListener(ShowBriefly);
        _displayData.onClick.AddListener(HideBriefly);
    }
    private void HideBriefly()
    {
        _animator.Play(hideBriefly);
        _displayData.onClick.RemoveListener(HideBriefly);
        _displayData.onClick.AddListener(ShowBriefly);
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

        _roleView.onLevel += LevelUp;
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
        _displayData.onClick.RemoveAllListeners();
        _displayData.onClick.AddListener(ShowBriefly);
    }
    public void WithDraw()
    {
        _animator.Play(withdraw);
    }

    public void BuyRole()
    {
        DebugLog.Message("Buy");
        var result = archiveManager.BuyRole(_roleIndex, roleData.price);
        if (result)
            SetPurchased(result);
        _buyButton.BuyResult(result);
    }
    private void LevelUp()
    {
        var result = archiveManager.UpdateRoleLevelUp(_roleIndex);
        Debug.Log(result);
        if (result)
        {
            var roleDatas = archiveManager.archiveObj.roles[_roleIndex];
            _roleView?.UpdateView(roleDatas);
        }
    }
}
