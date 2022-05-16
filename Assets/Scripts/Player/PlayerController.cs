using System;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 100;
    //[SerializeField]
    private RoleBase _playerData;
    [Header("Camera")]
    [SerializeField]
    private float smoothTimer = 10f;
    [SerializeField]
    private Vector3 currentSpeed;
    private SpriteRenderer _spriteRenderer;

    private int _maxHealth;
    private int _currentHealth;
    private InputManager _inputManager;
    private Rigidbody2D _rig;
    private Transform _transform;
    private Transform _cameraTrans;
    private Vector3 _offset;


    public void InitPlayerData(RoleBase data)
    {
        _playerData = data;
        _rig = GetComponent<Rigidbody2D>();
        _transform = transform;
        _cameraTrans = Camera.main.transform;
        _offset = _cameraTrans.position - _transform.position;

        _maxHealth = _playerData.health;
        _currentHealth = _playerData.health;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = data.sprite;

        InitInput();
    }

    private void Update()
    {
        var over = _transform.position + _offset;
        //_cameraTrans.position = Vector3.SmoothDamp(_cameraTrans.position, over,ref currentSpeed,smoothTimer,20,Time.deltaTime);
        _cameraTrans.position = Vector3.Lerp(_cameraTrans.position, over, Time.deltaTime * smoothTimer);
    }

    private void InitInput()
    {
        _inputManager = GameManager.Instance.inputManager;
        _inputManager.InputButton.onDrag += Move;
    }
    private void OnDestroy()
    {
        if (_inputManager != null && _inputManager.InputButton != null)
            _inputManager.InputButton.onDrag -= Move;
    }

    private void Dead()
    {
        _spriteRenderer.DOFade(0, 2f);
    }
    public void Move(Vector2 input)
    {
        DebugLog.Message(input);
        _rig.AddForce(input * _playerData.speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        // var dir = Quaternion.LookRotation(input.normalized);
        // if (Quaternion.Angle(_transform.rotation, dir) > 1)
        // {
        //     _transform.rotation = Quaternion.Lerp(_transform.rotation, dir, _rotationSpeed * Time.deltaTime);
        // }

    }
}