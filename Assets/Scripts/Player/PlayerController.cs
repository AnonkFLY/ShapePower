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
    public void InitPlayerData(RoleBase data)
    {
        _playerData = data;
        _maxHealth = _playerData.health;
        _currentHealth = _playerData.health;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = data.sprite;
    }
    private void Dead()
    {
        _spriteRenderer.DOFade(0,2f);
    }
}