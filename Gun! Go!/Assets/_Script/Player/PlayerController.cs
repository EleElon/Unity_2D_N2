using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerController : MonoBehaviour {
    internal static PlayerController Instance { get; }

    //variable
    Vector2 moveSpeed = Vector2.zero;
    float maxSpeed = 9f, giaToc = 5f, giaTocGiam = 15f;

    [Header("---------- Component ----------")]
    Rigidbody2D _rb;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    [Header("---------- Element ----------")]
    bool ismoving;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        HandleMovement();
        UpdateAnimation();
    }

    void HandleMovement() {
        // float x_Posi = Input.GetAxisRaw("Horizontal");
        // float y_Posi = Input.GetAxisRaw("Vertical");

        // Vector2 direction = new Vector2(x_Posi, y_Posi).normalized;

        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        Move(direction);

        transform.Translate(moveSpeed * Time.deltaTime);

        // if (direction.x > 0) {
        //     transform.localScale = new Vector2(1, 1);
        // }
        // else if (direction.x < 0) {
        //     transform.localScale = new Vector2(-1, 1);
        // }

        if (direction.x < 0) {
            _spriteRenderer.flipX = true;
        }
        else if (direction.x > 0) {
            _spriteRenderer.flipX = false;
        }

    }

    void Move(Vector2 direction) {
        if (direction.magnitude > 0) {
            moveSpeed = Vector2.MoveTowards(moveSpeed, direction * maxSpeed, giaToc * Time.deltaTime);
            ismoving = true;
        }
        else {
            moveSpeed = Vector2.MoveTowards(moveSpeed, Vector2.zero, giaTocGiam * Time.deltaTime);
            ismoving = false;
        }
    }

    void UpdateAnimation() {
        bool isMovingAnimation = ismoving;

        _animator.SetBool("IsMoving", isMovingAnimation);
    }
}
