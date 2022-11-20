using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _moveAnimationParameter = "";
    [SerializeField] private string _biteAnimationParameter = "";

    public void SetMoveAnimationBoolState(bool isMoving) => _animator.SetBool(_moveAnimationParameter, isMoving);
    public void PlayBiteAnimation() => _animator.SetTrigger(_biteAnimationParameter);
}
