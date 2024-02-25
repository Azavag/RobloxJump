using Cinemachine;
using UnityEngine;

public class SkinShopTrigger : MonoBehaviour
{
    [SerializeField] SkinShop skinShop;
    [SerializeField] Transform viewPoint;
    [SerializeField] CinemachineVirtualCamera shopCamera;
    [SerializeField] GameObject triggerVisual;
    private void Awake()
    {
        shopCamera.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            MovePlayerToPoint(other.transform);
            skinShop.OpenSkinShop();
            ToggleSkinShopView(true);
        }
    }

    void MovePlayerToPoint(Transform playerTransform)
    {
        playerTransform.position = viewPoint.position;
        playerTransform.rotation = viewPoint.rotation;
    }

    public void ToggleSkinShopView(bool state)
    {
        shopCamera.enabled = state;
        triggerVisual.SetActive(!state);
    }


}
