using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerUIHandler playerUIHandler))
        {
            playerUIHandler.SetButtonActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerUIHandler playerUIHandler))
        {
            playerUIHandler.SetButtonActive(false);
        }
    }
}
