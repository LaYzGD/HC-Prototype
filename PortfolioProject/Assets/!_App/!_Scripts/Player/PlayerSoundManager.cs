using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _footSteps;

    private AudioHandler _audioHandler;

    private void Start()
    {
        _audioHandler = AudioHandler.Instance;
    }

    private void PlayRandomStepSound()
    {
        var randomSound = _footSteps[Random.Range(0, _footSteps.Length)];
        _audioHandler.PlaySound(randomSound);
    }
}
