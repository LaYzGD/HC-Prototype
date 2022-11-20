using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AnimationHandler))]
[RequireComponent(typeof(PlayerInputSystem))]
[RequireComponent(typeof(PlayerStatsHandler))]
public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] protected Transform _orientation;

    private PlayerStatsHandler _playerStatsHandler;
    private float _movementSpeed;

    private PlayerInputSystem _inputSystem;
    private Rigidbody _rigidbody;

    private Vector3 _movementDirection;
    private AnimationHandler _animationHandler;

    private float _startMovementSpeed;
    private bool _canMove = true;

    private void Start()
    {
        GetAllComponents();
        SetMovementSpeed();

        _playerStatsHandler.OnStatsUpgraded += SetMovementSpeed;
    }

    private void GetAllComponents()
    {
        _playerStatsHandler = GetComponent<PlayerStatsHandler>();
        _inputSystem = GetComponent<PlayerInputSystem>();
        _animationHandler = GetComponent<AnimationHandler>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void HandleMovementDirection()
    {
        _movementDirection = _orientation.forward * _inputSystem.MovementDirection.y + _orientation.right * _inputSystem.MovementDirection.x;
    }

    private void Update()
    {
        if (_canMove)
        {
            HandleMovementDirection();
        }

        HandleAnimation();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_movementDirection.x * _movementSpeed, _rigidbody.velocity.y, _movementDirection.z * _movementSpeed);
    }

    private void HandleAnimation()
    {
        bool isMoving = _rigidbody.velocity != Vector3.zero;
        _animationHandler.SetMoveAnimationBoolState(isMoving);
    }

    public void SetMovementAbilityTo(bool condition)
    {
        _canMove = condition;
        _rigidbody.velocity = Vector3.zero;
        _movementSpeed = condition ? _startMovementSpeed : 0;
    }

    private void SetMovementSpeed()
    {
        _movementSpeed = _playerStatsHandler.MovementSpeed;
        _startMovementSpeed = _movementSpeed;
    }

    private void OnDisable()
    {
        _playerStatsHandler.OnStatsUpgraded -= SetMovementSpeed;
    }
}
