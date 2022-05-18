
using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    private Damage _damage;
    public float _delayTimer = 0.3f;
    [SerializeField]
    protected float speed = 10;
    [SerializeField]
    private float _offset = 10;
    [SerializeField]
    private float _recoil = 1f;
    [Range(0, 2)]
    [SerializeField]
    protected float _addForce = 1;
    [SerializeField]
    private int hitCount = 1;
    protected Transform _transform;
    protected Action onUpdate;
    protected Action onTrigger;
    [SerializeField]
    private int isBig;


    public float Recoil { get => _recoil; }

    private void Awake()
    {
        _transform = transform;
        if (isBig == 0)
            AudioManager.Instance.PlaySoundEffect(3);
        else if (isBig == 1)
            AudioManager.Instance.PlaySoundEffect(4);
        else if (isBig == 2)
            AudioManager.Instance.PlaySoundEffect(5);
    }
    public void Init(float eluer)
    {
        if (eluer < 1000)
            _transform.Rotate(0, 0, eluer, Space.Self);
        _transform.Rotate(Vector3.forward * UnityEngine.Random.Range(-_offset, _offset));
        Destroy(gameObject, 3f);
    }
    void Update()
    {
        _transform.Translate(_transform.up * (speed * Time.deltaTime), Space.World);
        onUpdate?.Invoke();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.GetComponent<IHurtable>()?.Hurt(_damage);
        other.rigidbody?.AddForce(_transform.up * (speed * _addForce), ForceMode2D.Impulse);
        BreakBullet(other, null);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.GetComponent<IHurtable>()?.Hurt(_damage);
        other.GetComponent<Rigidbody2D>()?.AddForce(_transform.up * (speed * _addForce), ForceMode2D.Impulse);
        --hitCount;
        onTrigger?.Invoke();
        if (hitCount == 0)
            BreakBullet(null, other);
    }
    public abstract void BreakBullet(Collision2D collision, Collider2D trigger);
}
