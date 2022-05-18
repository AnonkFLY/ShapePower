using System;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHurtable
{
    [SerializeField]
    private float angle;
    [SerializeField]
    private float fireDistance = 1;
    [SerializeField]
    private RoleBase _playerData;
    private SpriteRenderer _spriteRenderer;

    private int _maxHealth;
    private int _currentHealth;
    private InputManager _inputManager;
    private Rigidbody2D _rig;
    private Transform _transform;
    private Vector3 _offset;
    [Header("Bullet")]
    [SerializeField]
    private GameObject[] bullets;
    private bool state;
    private bool isFire;
    private Transform _firePos;
    private Transform _target;
    private CameraMananger _camera;

    public void InitPlayerData(RoleBase data)
    {
        _playerData = data;
        _rig = GetComponent<Rigidbody2D>();
        _transform = transform;
        _firePos = _transform.GetChild(0);
        _camera = FindObjectOfType<CameraMananger>();
        _camera.SetLockTrans(_transform);

        _maxHealth = _playerData.health;
        _currentHealth = _playerData.health;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = data.sprite;
        InitInput();
    }
    private float _timer;
    private void Update()
    {
        if (!isFire)
            return;
        if (_timer <= 0)
            Fire();
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
                Fire();
        }
    }


    // private void FixedUpdate()
    // {
    //     if (_rig.velocity.magnitude > _maxSpeed)
    //     {
    //         _rig.velocity = Vector3.ClampMagnitude(_rig.velocity, _maxSpeed);
    //     }
    // }

    private void InitInput()
    {
        _inputManager = GameManager.Instance.inputManager;
        _inputManager.InputButton.onDrag += Move;
        _inputManager.InputButton.fireButton.button.onDown += FireKey;
        _inputManager.InputButton.fireButton.button.onClick += FireKeyUp;
        _inputManager.InputButton.fireButton.button.onExit += FireKeyUp;
        _inputManager.InputButton.cutoverButton.button.onClick += Cutover;
    }

    private void Cutover()
    {
        state = !state;
    }

    private void FireKey()
    {
        isFire = true;
        _camera.SetLockTrans(_firePos.GetChild(0));
        _camera.StartShakeCamera();
    }
    private void FireKeyUp()
    {
        isFire = false;
        _camera.SetLockTrans(_transform);
        _camera.StopShakeCamera();
    }
    private void OnDestroy()
    {
        if (_inputManager == null || _inputManager.InputButton == null)
            return;
        _inputManager.InputButton.onDrag -= Move;
        _inputManager.InputButton.fireButton.button.onDown -= FireKey;
        _inputManager.InputButton.fireButton.button.onClick -= FireKeyUp;
        _inputManager.InputButton.fireButton.button.onExit -= FireKeyUp;
        _inputManager.InputButton.cutoverButton.button.onClick -= Cutover;
    }
    public void AddForce(Vector2 force)
    {
        currentSpeed += force / _rig.mass;
    }
    private void Dead()
    {
        _gameView.sliderView.SetValue(0);
        GetComponent<Collider2D>().enabled = false;
        _spriteRenderer.DOFade(0, 2f).onComplete += () => { GameManager.Instance.uiManager.OpenUI(UIType.GameLose, true); };
    }
    private Vector2 currentSpeed;
    public void Move(Vector2 input)
    {
        if (_currentHealth <= 0)
            input = Vector2.zero;
        currentSpeed = Vector2.Lerp(currentSpeed, input * _playerData.speed, Time.deltaTime);
        _transform.Translate(currentSpeed * Time.deltaTime, Space.World);
        if (_target != null)
        {
            var radius = (_target.position - _transform.position).normalized;
            _transform.eulerAngles = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, radius));
            return;
        }
        if (input == Vector2.zero || _currentHealth <= 0)
            return;
        angle = Vector2.SignedAngle(Vector2.up, input);
        _transform.eulerAngles = new Vector3(0, 0, angle);
    }
    public void Fire()
    {
        if (_currentHealth <= 0)
            return;
        _target = RayUtil.FindObjOfLate(_transform.position, 7, 1 << 6, 5);

        Vector2 firePos = _firePos.position;
        Vector3 euler = _firePos.position - _transform.position;
        if (_target != null)
        {
            euler = (_target.position - _transform.position).normalized;
            firePos = euler * fireDistance + _transform.position;
        }
        Bullet bullet;
        if (!state)
        {
            bullet = Instantiate(bullets[_playerData.level - 1], firePos, Quaternion.identity).GetComponent<Bullet>();
        }
        else
        {
            bullet = Instantiate(bullets[_playerData.level + 4], firePos, Quaternion.identity).GetComponent<Bullet>();
        }
        var angle = Vector2.SignedAngle(Vector3.up, euler);
        _transform.position -= euler * (bullet.Recoil * Time.deltaTime);
        bullet.Init(angle);
        _timer = bullet._delayTimer;
    }
    private GameView _gameView;
    public void Hurt(Damage damage)
    {
        if (damage.originDamage == OriginDamage.Player)
            return;
        if (_currentHealth <= 0)
            return;
        _currentHealth -= Mathf.Clamp(damage.damageValue - (_playerData.armor / damage.damageValue), 1, 999);
        _currentHealth = Math.Clamp(_currentHealth, 0, _maxHealth);
        var value = (float)_currentHealth / (float)_maxHealth;
        if (_gameView == null)
            _gameView = FindObjectOfType<GameView>();
        _gameView.sliderView.SetValue(value);
        if (_currentHealth <= 0)
            Dead();
    }
}