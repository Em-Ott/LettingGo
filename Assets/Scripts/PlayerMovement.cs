using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _horizontalMoveSpeed = 0.1f;
    [SerializeField]
    private float _verticalMoveSpeed = 1f;
    private float _currentHorizontalMoveSpeed = 0.1f;
    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private bool _inWater;

    // Grab components in Awake incase other scripts end up relying on us having assigned variables in Start
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentHorizontalMoveSpeed = _horizontalMoveSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("On trigger enter for player with " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Water"))
        {
            _inWater = true;
            _moveInput = new Vector2(_moveInput.x, 0);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            _inWater = false;
        }
    }

    public void ChangeLocation(Vector3 newLocation)
    {
        this.gameObject.transform.position = newLocation;
    }

    public void ChangeMoveSpeed(float newMoveSpeed)
    {
        if (newMoveSpeed <= _horizontalMoveSpeed)
        {
            _currentHorizontalMoveSpeed = _horizontalMoveSpeed;
        }
        else
        {
            _currentHorizontalMoveSpeed = newMoveSpeed;
        }
    }

    public void UpdateMovement(float newXMove, float newYMove)
    {
        if (_inWater) { newYMove = 0; }

        _moveInput = new Vector2(newXMove, newYMove).normalized;
        _moveInput = new Vector2(_moveInput.x * _currentHorizontalMoveSpeed, _moveInput.y * _verticalMoveSpeed);
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = _moveInput;
    }
}