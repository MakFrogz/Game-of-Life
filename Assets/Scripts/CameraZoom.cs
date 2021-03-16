using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraZoom : MonoBehaviour
{
    private Camera _camera;
    private UserInput _inputActions;
    private Vector2 _direction;

    private int _width;
    private int _height;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _height = (int)(2 * _camera.orthographicSize);
        _width = (int)(_height * _camera.aspect);
    }

    public void InitInput(UserInput inputActions)
    {
        _inputActions = inputActions;
        _inputActions.Camera.Zoom.performed += ctx => OnZoom(ctx);
        _inputActions.Camera.Move.performed += ctx => OnMove(ctx);
        _inputActions.Camera.Move.canceled += ctx => OnMove(ctx);
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        _direction = ctx.ReadValue<Vector2>().normalized;
        Debug.Log(_direction);
    }

    private void OnZoom(InputAction.CallbackContext ctx)
    {
        Vector2 scrollVector = ctx.ReadValue<Vector2>();
        if (scrollVector.y > 0)
        {
            if (_camera.orthographicSize > 10)
            {
                _camera.orthographicSize -= 1;
            }
        }
        else if (scrollVector.y < 0)
        {
            if (_camera.orthographicSize < 48)
            {
                _camera.orthographicSize += 1;
            }
        }
        _height = (int)(2 * _camera.orthographicSize);
        _width = (int)(_height * _camera.aspect);
    }

    private void Update()
    {
        float xBound = Mathf.Abs(Board.Instance.LeftBottomPoint.x) - (_width / 2);
        float yBound = Mathf.Abs(Board.Instance.LeftBottomPoint.y) - (_height / 2);

        float xClamp = Mathf.Clamp(transform.position.x, -xBound, xBound);
        float yClamp = Mathf.Clamp(transform.position.y, -yBound, yBound);

        transform.position = new Vector3(xClamp, yClamp, transform.position.z);
    }

    private void FixedUpdate()
    {
        transform.Translate(_direction * 20f * Time.deltaTime);
    }
}
