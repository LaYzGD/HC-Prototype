using UnityEngine;

public class PlayerRotationHandler : MonoBehaviour
{
    [SerializeField] private Transform _orientation;
    [SerializeField] private Transform _playerObj;
    [SerializeField] private float _rotationSpeed;

    private PlayerInputSystem _inputSystem;

    private void Start()
    {
        _inputSystem = GetComponent<PlayerInputSystem>();
    }

    private void Update()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        var inputDirection = _orientation.forward * _inputSystem.MovementDirection.y + _orientation.right * _inputSystem.MovementDirection.x;

        if (inputDirection != Vector3.zero)
        {
            _playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
        }
    }
}
