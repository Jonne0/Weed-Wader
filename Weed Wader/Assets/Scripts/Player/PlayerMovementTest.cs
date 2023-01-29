using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    public float Speed = 1f;
    public float Accel = 10f;
    public float GroundFriction = 5f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector2 _moveInput;
    private Vector2 _velocity;
    private float _deltaTime;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _deltaTime = Time.deltaTime;
        _moveInput = Vector2.right * Input.GetAxisRaw("Horizontal") + Vector2.up * Input.GetAxisRaw("Vertical");

        Accelerate(ref _velocity, Speed, Accel);
        Friction(ref _velocity, Speed, GroundFriction);

        Vector2 moveDif = _velocity - _rigidbody.velocity;
        _rigidbody.AddForce(moveDif, ForceMode2D.Impulse);

        _animator.SetFloat("MoveInput", _moveInput.magnitude);

        if (Mathf.RoundToInt(_moveInput.x) != 0)
            transform.localScale = new Vector2(Mathf.RoundToInt(_moveInput.x), transform.localScale.y);
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
