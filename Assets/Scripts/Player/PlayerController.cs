using System;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private RoleBase _playerData;
    private SpriteRenderer _spriteRenderer;
    private int _maxHealth;
    private int _currentHealth;
    private InputManager _inputManager;
    private Transform _transform;
    public void InitPlayerData(RoleBase data)
    {
        _transform = transform;
        _playerData = data;
        _maxHealth = _playerData.health;
        _currentHealth = _playerData.health;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = data.sprite;

        InitInput();
    }

    private void InitInput()
    {
        _inputManager = GameManager.Instance.inputManager;
        _inputManager.InputButton.onDrag += Move;
    }
    private void OnDestroy()
    {
        _inputManager.InputButton.onDrag -= Move;
    }

    private void Dead()
    {
        _spriteRenderer.DOFade(0, 2f);
    }
    public void Move(Vector2 input)
    {
        DebugLog.Message(input);
        _transform.Translate(input * _playerData.speed * Time.fixedDeltaTime, Space.World);
    }
}