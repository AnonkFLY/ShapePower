using System;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour, IHurtable
{
    [SerializeField]
    private float angle;
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

    public void InitPlayerData(RoleBase data)
    {
        _playerData = data;
        _rig = GetComponent<Rigidbody2D>();
        _transform = transform;
        _firePos = _transform.GetChild(0);
        FindObjectOfType<CameraMananger>().SetLockTrans(_transform);


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
        print("Firing");
        isFire = true;
    }
    private void FireKeyUp()
    {
        isFire = false;
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
        currentSpeed += force;
    }
    private void Dead()
    {
        _spriteRenderer.DOFade(0, 2f);
    }
    private Vector2 currentSpeed;
    public void Move(Vector2 input)
    {
        currentSpeed = Vector2.Lerp(currentSpeed, input * _playerData.speed, Time.deltaTime);
        // if (input == Vector2.zero)
        // {
        // }
        // currentSpeed = Vector2.Lerp(Vector2.zero, input * _playerData.speed, Time.deltaTime);
        // currentSpeed += (input.normalized - currentSpeed).normalized * _playerData.speed * Time.deltaTime;
        // if(currentSpeed.sqrMagnitude<0)
        //     currentSpeed = Vector2.zero;
        //var dir = input.normalized * _playerData.speed * Time.fixedDeltaTime;
        //_rig.AddForce(input * _playerData.speed * Time.fixedTime, ForceMode2D.Force);
        _transform.Translate(currentSpeed * Time.deltaTime, Space.World);
        if (input == Vector2.zero)
            return;
        angle = Vector2.SignedAngle(Vector2.up, input);
        _transform.eulerAngles = new Vector3(0, 0, angle);
        // var ratation = Quaternion.LookRotation(new Vector3(angle, 0, 0));
        // if (Quaternion.Angle(_transform.rotation, ratation) > 1)
        //     _transform.rotation = Quaternion.Lerp(_transform.rotation, ratation, Time.deltaTime * _rotationSpeed);
    }
    public void Fire()
    {
        if (!state)
        {
            _timer = Instantiate(bullets[_playerData.level - 1], _firePos.position, _transform.rotation).GetComponent<Bullet>()._delayTimer;
        }
        else
        {
            _timer = Instantiate(bullets[_playerData.level + 5], _firePos.position, _transform.rotation).GetComponent<Bullet>()._delayTimer; ;
        }
    }
    private GameView _gameView;
    public void Hurt(Damage damage)
    {
        if (damage.originDamage == OriginDamage.Player)
            return;
        _currentHealth -= damage.damageValue - (damage.damageValue / _playerData.armor);
        _currentHealth = Math.Clamp(_currentHealth, 0, _maxHealth);
        var value = _currentHealth / _maxHealth;
        if (_gameView == null)
            _gameView = FindObjectOfType<GameView>();
        _gameView.sliderView.SetValue(value);
    }
}