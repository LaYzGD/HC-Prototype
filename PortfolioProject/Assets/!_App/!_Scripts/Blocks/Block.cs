using UnityEngine;

public class Block : MonoBehaviour, IExplosionable
{
    [SerializeField] private int _durability;
    [SerializeField] private GameObject _damageParticles;
    [SerializeField] private GameObject _hitParticles;
    [SerializeField] private AudioClip[] _damageSounds;
    [SerializeField] private AudioClip _destroySound;
    [SerializeField] protected Transform _particleSpawnPoint;
    [SerializeField] private int _coinsAmount;
    [SerializeField] private bool _canBeExploded = true;

    private AudioHandler _audioHandler;
    private MoneySystem _moneySystem;

    private void Start()
    {
        _audioHandler = AudioHandler.Instance;
        _moneySystem = MoneySystem.Instance;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
        {
            return;
        }

        _durability -= damage;
        Instantiate(_damageParticles, _particleSpawnPoint.position, Quaternion.identity);
        Instantiate(_hitParticles, _particleSpawnPoint.position, Quaternion.identity);

        if (_durability > 0)
        {
            _audioHandler.PlaySound(_damageSounds[Random.Range(0, _damageSounds.Length)]);
        }

        if (_durability <= 0)
        {
            DestroyBlock();
        }
    }

    protected virtual void DestroyBlock()
    {
        _audioHandler.PlaySound(_destroySound);
        _moneySystem.TryAddCoins(_coinsAmount);
        gameObject.SetActive(false);
    }

    public void DestroyObject()
    {
        if (_canBeExploded)
        {
            _moneySystem.TryAddCoins(_coinsAmount);
            gameObject.SetActive(false);
        }
    }
}
