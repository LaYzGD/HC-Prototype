using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;

[DefaultExecutionOrder(1)]
public class PlayerUIHandler : MonoBehaviour
{
    [Header("Shop")]
    [SerializeField] private Button _shopButton;
    [SerializeField] private float _downShopButtonPoint = -80f;
    [SerializeField] private float _upShopButtonPoint = 300f;
    [SerializeField] private float _tweeningDuration = 0.25f;
    [SerializeField] private GameObject _shopCanvas;
    [Space]
    [Header("ShopCanvas")]
    [SerializeField] private Button _closeShopButton;
    [SerializeField] private List<UpgradeButton> _upgradeButtons;
    [Space]
    [Header("Stats")]
    [SerializeField] private Slider _movementSpeedSlider;
    [SerializeField] private Slider _biteStrengthSlider;
    [SerializeField] private Slider _biteSpeedSlider;
    [Space]
    [SerializeField] private TextMeshProUGUI _moneyText;

    private RectTransform _shopButtonRectTransform;
    private PlayerMovementHandler _playerMovementHandler;
    private PlayerStatsHandler _playerStatsHandler;

    private MoneySystem _moneySystem;

    private void Awake()
    {
        GetAllComponents();
        SetButtonsListeners();
    }

    private void Start()
    {
        UpdateStatsInfo();
        UpdateMoneyText();
        _playerStatsHandler.OnStatsUpgraded += UpdateStatsInfo;
        _moneySystem.OnUpdateCoins += UpdateMoneyText;
    }

    public void SetButtonActive(bool condition)
    {
        var endPosition = condition ? _downShopButtonPoint : _upShopButtonPoint;

        _shopButtonRectTransform
            .DOAnchorPos(new Vector2(_shopButtonRectTransform.anchoredPosition.x, endPosition), _tweeningDuration);
    }

    private void SetButtonsListeners()
    {
        _shopButton.onClick.AddListener(() => SetShopActive(true));
        _closeShopButton.onClick.AddListener(() => SetShopActive(false));
        _upgradeButtons.ForEach(upgradeButton => 
        {
            if (upgradeButton.TryGetComponent(out Button button))
            {
                button.onClick.AddListener(() => 
                {
                    if (_moneySystem.TrySubtractCoins(upgradeButton.CurrentPrice))
                    {
                        _playerStatsHandler.UpgradeStats(upgradeButton.GetUpgradedStats());
                    }
                });
            }
        });
    }

    private void GetAllComponents()
    {
        _moneySystem = MoneySystem.Instance;
        _playerStatsHandler = GetComponent<PlayerStatsHandler>();
        _shopButtonRectTransform = _shopButton.GetComponent<RectTransform>();
        _playerMovementHandler = GetComponent<PlayerMovementHandler>();
    }

    private void SetShopActive(bool condition)
    {
        _shopCanvas.SetActive(condition);
        _playerMovementHandler.SetMovementAbilityTo(!condition);
    }

    private void UpdateStatsInfo()
    {
        _movementSpeedSlider.value = _playerStatsHandler.MovementSpeed;
        _biteStrengthSlider.value = _playerStatsHandler.BiteStrength;
        _biteSpeedSlider.value = _playerStatsHandler.BiteSpeed;
    }

    private void UpdateMoneyText() => _moneyText.text = _moneySystem.TotalCoins.ToString();

    private void OnDisable()
    {
        _moneySystem.OnUpdateCoins -= UpdateMoneyText;
        _playerStatsHandler.OnStatsUpgraded -= UpdateStatsInfo;
    }
}
