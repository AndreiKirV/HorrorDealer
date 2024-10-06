using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float2 _constraintRotationX;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _moveHeightSpeed = 1f;
    [SerializeField] private float2 _Y =  new (0f,50f);

    private InputSettings _input;
    private bool _isRotate = false;

    private void OnEnable()
    {
        _input = new InputSettings();
        _input.Mouse.Delta.performed += OnMouseDeltaChanged;
        _input.Mouse.RB.started += ChangeIsRotate;
        _input.Mouse.RB.canceled += ChangeIsRotate;
        _input.Enable();
    }

    private void OnDisable()
    {
        _input = new InputSettings();
        _input.Mouse.Delta.performed -= OnMouseDeltaChanged;
        _input.Mouse.RB.started -= ChangeIsRotate;
        _input.Mouse.RB.canceled -= ChangeIsRotate;
        _input.Disable();
    }

    private void Update()
    {
        OnMoveDeltaChanged(_input.Move.WASD.ReadValue<Vector2>());
        OnHeightDeltaChanged(_input.Move.Height.ReadValue<float>());
    }

    private void ChangeIsRotate(InputAction.CallbackContext context)
    {
        _isRotate = !_isRotate;
    }

    private void OnHeightDeltaChanged(float context)
    {
        Vector3 move = new Vector3(0, context * _moveHeightSpeed, 0);

        if (transform.position.y + move.y > _Y.x && transform.position.y + move.y < _Y.y)
            transform.position += move;
    }

    private void OnMoveDeltaChanged(Vector2 context)
    {
        Vector3 moveDelta = new Vector3(context.x, 0, context.y) * _moveSpeed * Time.deltaTime;
        Vector3 move = transform.right * moveDelta.x + transform.forward * moveDelta.z;
        move.y = 0;
        transform.position += move;
    }

    private void OnMouseDeltaChanged(InputAction.CallbackContext context)
    {
        if (_isRotate)
        {
            Vector2 mouseDelta = context.ReadValue<Vector2>();

            float rotationX = mouseDelta.y * _rotationSpeed * Time.deltaTime;
            float rotationY = mouseDelta.x * _rotationSpeed * Time.deltaTime;

            Vector3 currentRotation = transform.localEulerAngles;
            currentRotation.x = Mathf.Repeat(currentRotation.x + 180f, 360f) - 180f;
            float newRotationX = Mathf.Clamp(currentRotation.x - rotationX, _constraintRotationX.x, _constraintRotationX.y);
            float newRotationY = currentRotation.y + rotationY;

            transform.localRotation = Quaternion.Euler(newRotationX, newRotationY, 0f);
        }
    }
}