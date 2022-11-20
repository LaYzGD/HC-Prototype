using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _natureSoundSource;
    [SerializeField] private AudioClip _startNatureSoundClip;
    public static AudioHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        SetNatureSound(_startNatureSoundClip);
    }

    public void PlaySound(AudioClip soundClip)
    {
        _audioSource.PlayOneShot(soundClip);
    }

    public void SetNatureSound(AudioClip sound)
    {
        _natureSoundSource.clip = sound;
        _natureSoundSource.Play();
    }
}
