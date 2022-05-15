using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : UIBase, IUpdatable
{
    private Button _stopButton;
    private bool isStop;
    private StopView _stopView;
    [SerializeField]
    private Transform _silderTrans;
    private SliderView _sliderView;
    [SerializeField] private TMP_Text _infoText;
    public override void RegisterUI(UIManager uiManager)
    {
        base.RegisterUI(uiManager);
        _stopButton = GetComponentInChildren<Button>();
        _stopView = GetComponentInChildren<StopView>();
        _stopButton.onClick.AddListener(ChangeState);
        _stopView.onStop += () => { isStop = false; };
        _sliderView = new SliderView(_silderTrans);
    }
    private float _timer;
    private float _maxTimer;
    private void FixedUpdate()
    {
        if (!isOpen)
            return;
        _timer -= Time.fixedDeltaTime;

    }
    private void TimeOutOver()
    {
        
    }
    public void OnUpdateView()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeState();
        }
    }
    private void ChangeState()
    {
        isStop = !isStop;
        if (isStop)
        {
            _stopView.Open();
            return;
        }
        _stopView.Close();
    }
    public override void Open()
    {
        base.Open();
        _sliderView.SetValue(1);
    }
    public override void Close()
    {
        canvasGroup.DOFade(0,1.5f);
    }   
}
