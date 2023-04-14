using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerMain _playerMain;
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    private Vector2 _moveInput;
    private bool _isFacingRight;
    // Start is called before the first frame update
    void Start()
    {
        _playerMain = gameObject.GetComponent<PlayerMain>();
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _isFacingRight = true;
    }

    void Update()
    {
        MovementInput();
    }
    void FixedUpdate()
    {
        Move();
    }
    private void MovementInput()
    {
        _moveInput = _playerMain._movement.ReadValue<Vector2>().normalized;
        _playerMain._animator.SetFloat(_playerMain._horizontalID, _moveInput.x);
        _playerMain._animator.SetFloat(_playerMain._verticalID, _moveInput.y);
    }
    private void Move()
    {
        //Flip Character when facing right or left
        if (_moveInput.x > 0 && !_isFacingRight) FlipCharacter();
        if (_moveInput.x < 0 && _isFacingRight) FlipCharacter();
            _rigidbody.velocity =_moveInput * _speed;
    }
    private void FlipCharacter()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        _isFacingRight = !_isFacingRight;
    }
}
