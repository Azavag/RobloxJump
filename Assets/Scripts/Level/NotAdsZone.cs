using UnityEngine;

public class NotAdsZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnAdvFreeZone.onAdvFreeZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnAdvFreeZone.onAdvFreeZone = false;
        }
    }
}
