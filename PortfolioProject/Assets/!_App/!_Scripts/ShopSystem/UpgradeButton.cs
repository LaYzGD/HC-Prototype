using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Stat
{
    Movement,
    BiteStrength,
    BiteSpeed
}

[DefaultExecutionOrder(0)]
[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private int _statUpgradeAmount;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private int _price;
    [SerializeField] private int _upgradeMultiplierIndex = 2;
    [SerializeField] private Stat _currentStat;
    [SerializeField] private int _limit = 10;

    private PlayerStats _playerStats;

    private int _currentUpgradeLevel;

    public int CurrentPrice => _price;

    private void Awake()
    {
        SetStats();
    }

    private void Start()
    {
        UpdateText();
    }

    private void SetStats()
    {
        if (_currentUpgradeLevel == _limit)
        {
            gameObject.SetActive(false);
            return;
        }

        switch (_currentStat)
        {
            case Stat.Movement:
                _playerStats = new PlayerStats(_statUpgradeAmount, 0, 0);
                break;
            case Stat.BiteStrength:
                _playerStats = new PlayerStats(0, _statUpgradeAmount, 0);
                break;
            case Stat.BiteSpeed:
                _playerStats = new PlayerStats(0, 0, _statUpgradeAmount);
                break;
        }
    }

    private void UpdateInfo()
    {
        _price *= _upgradeMultiplierIndex;
        _currentUpgradeLevel++;
        if (_currentUpgradeLevel == _limit)
        {
            gameObject.SetActive(false);
        }
        UpdateText();
    }

    private void UpdateText() => _priceText.text = _price.ToString();

    public PlayerStats GetUpgradedStats()
    {
        UpdateInfo();
        return _playerStats;
    }
}


