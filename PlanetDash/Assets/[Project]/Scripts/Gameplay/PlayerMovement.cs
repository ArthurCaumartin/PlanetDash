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

    private void Dash(InputValue value)
    {
        if(value.Get<float>() > .5f)
        {
            
        }
    }

    private void OnJump(InputValue value)
    {
        if (value.Get<float>() > .5f)
            Jump();
    }

    private void OnDrawGizmos()
    {
        Vector3[] path = GetPathOnDirection(_inputDirection.x, 20, 10);
        if(path.Length == 0) return;
        print("count : " + path.Length);
        foreach (var item in path)
            print("path : " + item);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(path[0], 1);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(path[path.Length - 1], 1);
        for (int i = 0; i < path.Length - 1; i++)
        {
            Gizmos.color = i % 2 == 0 ? Color.green : Color.yellow;
            Gizmos.DrawLine(path[i], path[i + 1]);
        }
    }
}




