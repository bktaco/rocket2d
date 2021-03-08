using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private readonly float _verticalInputAcceleration = 2;
    private readonly float _horizontalInputAcceleration = 20;

    private readonly float _maxSpeed = 20;
    private readonly float _maxRotationSpeed = 150;

    private readonly float _velocityDrag = 1;
    private readonly float _rotationDrag = 1;

    private Vector3 _velocity;
    private float _zRotationVelocity;

    private void Update()
    {
        // apply forward input
        Vector3 acceleration = Input.GetAxis("Vertical") * _verticalInputAcceleration * transform.up;
        _velocity += acceleration * Time.deltaTime;

        // apply turn input
        float zTurnAcceleration = -2 * Input.GetAxis("Horizontal") * _horizontalInputAcceleration;
        _zRotationVelocity += zTurnAcceleration * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // apply velocity drag
        _velocity *= (1 - Time.deltaTime * _velocityDrag);

        // clamp to maxSpeed
        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);

        // apply rotation drag
        _zRotationVelocity *= (1 - Time.deltaTime * _rotationDrag);

        // clamp to maxRotationSpeed
        _zRotationVelocity = Mathf.Clamp(_zRotationVelocity, -_maxRotationSpeed, _maxRotationSpeed);

        // update transform
        transform.position += _velocity * Time.deltaTime;
        transform.Rotate(0, 0, _zRotationVelocity * Time.deltaTime);
    }
}
