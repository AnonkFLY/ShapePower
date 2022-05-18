using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleChoice : MonoBehaviour, IJumpable
{
    [SerializeField] private RoleBase roleData;
    [SerializeField] private Image roleImage;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text brieflyText;
    private CanvasGroup _brieflyCanvas;
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
        _brieflyCanvas = brieflyText.GetComponent<CanvasGroup>();
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
    private float timer = 0;
    public void OnClick(bool isOn)
    {
        if (!isOn)
            return;
        AudioManager.Instance.PlaySoundEffect(8);
        DebugLog.Message($"Click {_roleIndex}");
        GameManager.Instance.OnRoleChange(roleData, _roleIndex);
        _brieflyCanvas.alpha = 1;
        timer = 1.5f;
    }
    private void Update()
    {
        if (timer <= 0)
            return;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                _brieflyCanvas.DOFade(0, 0.6f);
            }
        }
    }

    public void InitChoiceView(RoleBase roleData, int index, bool isPurchased)
    {
        _roleIndex = index;
        this.roleData = roleData;
        roleImage.sprite = roleData.sprite;
        _buyButton.gameObject.SetActive(!isPurchased);
        brieflyText.text = roleData.briefly;
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
