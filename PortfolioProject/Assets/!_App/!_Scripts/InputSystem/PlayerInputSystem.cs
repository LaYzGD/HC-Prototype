using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    public Vector2 MovementDirection { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementDirection = context.ReadValue<Vector2>();
    }
}
