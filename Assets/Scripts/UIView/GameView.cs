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
    public SliderView sliderView;
    [SerializeField] private TMP_Text _infoText;
    private Vector3 _orginCameraPos;
    public override void RegisterUI(UIManager uiManager)
    {
        base.RegisterUI(uiManager);
        _stopButton = GetComponentInChildren<Button>();
        _stopView = GetComponentInChildren<StopView>();
        _stopButton.onClick.AddListener(ChangeState);
        _stopView.onStop += () => { isStop = false; };
        sliderView = new SliderView(_silderTrans);
        _orginCameraPos = Camera.main.transform.position;
    }
    private void SetInfo(int info)
    {
        _infoText.text = "<size=36>remaining enemies:</size>\n" + info;
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
        AudioManager.Instance.PlaySoundEffect(8);
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
        //FindObjectOfType<CameraMananger>().ResetPosition();

        sliderView.SetValue(1);
        FindObjectOfType<SceneBase>().RegisterEnemyCount(SetInfo);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public override void Close()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.DOFade(0, 1.5f);
    }
}
