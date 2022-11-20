using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsHandler : MonoBehaviour
{
    [SerializeField] private GameObject _stepParticles;
    [SerializeField] private Transform _stepParticlesPoint;
    private void CreateParticles()
    {
        Instantiate(_stepParticles, _stepParticlesPoint.position, Quaternion.identity);
    }
}
