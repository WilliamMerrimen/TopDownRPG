using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOfset = 0.05f;
    public ContactFilter2D movementFilter;
    private Vector2 _movementInput;
    private Rigidbody2D _rb;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_movementInput != Vector2.zero)
        {
            bool success = TryMove(_movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(_movementInput.x, 0));
            }
            if (!success)
            {
                TryMove(new Vector2(0, _movementInput.y));
            }
            _anim.SetBool("isMovingDown", success && (_movementInput.x == 0 && _movementInput.y < 0));
            _anim.SetBool("isMovingUp", success &&(_movementInput.x == 0 && _movementInput.y > 0));
            _anim.SetBool("isMovingLeft", success && _movementInput.x != 0);
            _spriteRenderer.flipX = _movementInput.x < 0;
        }
        else
        {
            _anim.SetBool("isMovingLeft",false);
            _anim.SetBool("isMovingDown", false);
            _anim.SetBool("isMovingUp", false);
        }
    }


    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = _rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOfset);
            if (count == 0)
            {
                _rb.MovePosition(_rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        _movementInput = movementValue.Get<Vector2>();
    }
}
