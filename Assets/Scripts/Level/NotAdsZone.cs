using UnityEngine;

public class NotAdsZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AdvZone.insideNoAdvZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AdvZone.insideNoAdvZone = false;
        }
    }
}
