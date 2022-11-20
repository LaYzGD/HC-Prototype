using System;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class PlayerStatsHandler : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _biteStrength;
    [SerializeField] private int _biteSpeed;

    private PlayerStats _playerStats;

    public delegate void OnStatsUpgradedDelegate();

    public OnStatsUpgradedDelegate OnStatsUpgraded;

    private void Awake()
    {
        _playerStats = new PlayerStats(_movementSpeed, _biteStrength, _biteSpeed);
    }

    public float MovementSpeed => _playerStats.MovementSpeed;
    public int BiteStrength => _playerStats.BiteStrength;
    public int BiteSpeed => _playerStats.BiteSpeed;

    public void UpgradeStats(PlayerStats upgradedStats)
    {
        _playerStats += upgradedStats;
        OnStatsUpgraded?.Invoke();
    }
}
