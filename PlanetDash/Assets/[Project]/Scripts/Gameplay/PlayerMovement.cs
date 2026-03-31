using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : CircularSurfaceMovement
{
    [SerializeField] private Vector2 _inputDirection;

    protected override void Update()
    {
        velocity = _inputDirection;
        base.Update();
    }

    private void OnMove(InputValue value)
    {
        Vector3 inputDir = value.Get<Vector2>();
        if (inputDir == Vector3.zero) return;
        _inputDirection = inputDir;
    }

    private void OnJump(InputValue value)
    {
        if (value.Get<float>() > .5f)
            Jump();
    }
}