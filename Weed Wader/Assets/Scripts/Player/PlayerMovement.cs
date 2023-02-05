using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool CanMove = true;
    public float Speed = 1f;
    public float Accel = 10f;
    public float GroundFriction = 5f;
    public float DodgeForce = 5f;
    public float DodgeCooldown = 5f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector2 _moveInput;
    private Vector2 _velocity;
    private float _deltaTime;
    private bool _dodgeKeyDown;
    private float _dodgeCooldownDelta;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _deltaTime = Time.deltaTime;
        _moveInput = Vector2.right * Input.GetAxisRaw("Horizontal") + Vector2.up * Input.GetAxisRaw("Vertical");
        _dodgeKeyDown = Input.GetButtonDown("Fire2");


        if (!CanMove) 
        {
            this._rigidbody.velocity = Vector2.zero;
            return;
        }
        Accelerate(ref _velocity, Speed, Accel);
        Friction(ref _velocity, Speed, GroundFriction);

        Vector2 moveDif = _velocity - _rigidbody.velocity;
        
        _rigidbody.AddForce(moveDif, ForceMode2D.Impulse);

        _animator.SetFloat("Speed", _rigidbody.velocity.magnitude);
        _animator.SetFloat("Horizontal", _moveInput.x);
        _animator.SetFloat("Vertical", _moveInput.y);

        if (_dodgeCooldownDelta > 0)
        {
            _dodgeCooldownDelta -= _deltaTime;
        }

        if (_dodgeKeyDown && _dodgeCooldownDelta <= 0)
        {
            Dodge();
        }

    }

    void Dodge()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

        _velocity = direction * DodgeForce;
        _dodgeCooldownDelta = DodgeCooldown;

        GetComponent<Player>().Invincible = true;
    }

    private void Accelerate(ref Vector2 velocity, float speed, float accel)
    {
        float dot = Vector2.Dot(velocity, _moveInput);

        if (dot + accel > 10f)
            accel = 10f - dot;

        velocity += (_moveInput * speed * accel) * _deltaTime;
    }

    private void Friction(ref Vector2 velocity, float speed, float friction)
    {
        float addFriction = speed * friction * _deltaTime;

        velocity *= Mathf.Max(speed - addFriction, 0) / speed;
    }
}
