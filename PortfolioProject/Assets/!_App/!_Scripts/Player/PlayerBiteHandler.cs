using UnityEngine;

[RequireComponent(typeof(PlayerStatsHandler))]
public class PlayerBiteHandler : MonoBehaviour
{
    [SerializeField] private Transform _bitePoint;
    [SerializeField] private float _checkDistance;
    [Space]
    [SerializeField] private float _biteColdownTime = 1f;

    private float _biteStartTime;
    private bool _canBite = true;

    private PlayerStatsHandler _playerStatsHandler;
    private int _biteStrength;
    private int _biteSpeed;

    private AnimationHandler _animationHandler;

    private void Start()
    {
        GetAllComponents();
        SetBiteStats();

        _playerStatsHandler.OnStatsUpgraded += SetBiteStats;
    }

    private void Update()
    {
        CheckForBlock();
        HandleBiteColdown();
    }

    private void GetAllComponents()
    {
        _playerStatsHandler = GetComponent<PlayerStatsHandler>();
        _animationHandler = GetComponent<AnimationHandler>();
    }

    private void SetBiteStats()
    {
        _biteStrength = _playerStatsHandler.BiteStrength;
        _biteSpeed = _playerStatsHandler.BiteSpeed;
    }

    private void CheckForBlock()
    {
        if (Physics.Raycast(_bitePoint.position, _bitePoint.forward, out RaycastHit hitInfo, _checkDistance))
        {
            if (hitInfo.collider.transform.parent != null)
            {
                var block = hitInfo.collider.transform.parent.GetComponent<Block>();

                if (block != null && _canBite)
                {
                    block.TakeDamage(_biteStrength);
                    _animationHandler.PlayBiteAnimation();
                    _biteStartTime = Time.time;
                    _canBite = false;
                }
            }
        }
    }

    private void HandleBiteColdown()
    {
        if (Time.time > _biteStartTime + _biteColdownTime / _biteSpeed)
        {
            _canBite = true;
        }
    }

    private void OnDisable()
    {
        _playerStatsHandler.OnStatsUpgraded -= SetBiteStats;
    }
}
