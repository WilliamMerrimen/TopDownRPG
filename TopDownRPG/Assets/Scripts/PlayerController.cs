using System;
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
    
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_movementInput != Vector2.zero)
        {
            int _count = _rb.Cast(
                _movementInput,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOfset);
            if (_count == 0)
            {
                _rb.MovePosition(_rb.position + _movementInput * moveSpeed * Time.fixedDeltaTime);
            }
            
        }
    }

    void OnMove(InputValue movementValue)
    {
        _movementInput = movementValue.Get<Vector2>();
    }
}
