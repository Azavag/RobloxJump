using UnityEngine;

public class AdvManager : MonoBehaviour
{
    float advTimer;
    float advBreak = 60f;
    AdvAlert advAlert;
    bool isCounterToAdv;

    public static bool isAdvOpen;
    private void Awake()
    {
        transform.SetParent(null);
        advAlert = GetComponent<AdvAlert>();
    }
    private void Start()
    {
        ResetTimer();
    }
    private void Update()
    {
        advTimer -= Time.deltaTime;
        if(advTimer <= 0 && !isCounterToAdv && !OnAdvFreeZone.onAdvFreeZone)
        {
            isCounterToAdv = true;
            advAlert.ShowAdvAlertPanel();
        }
    }
    public void ShowAdv()
    {
        YandexSDK.ShowADV();
    }

    public void ShowRewardedAdv()
    {       
        YandexSDK.ShowRewardedADV();
    }    

    public void ResetTimer()
    {
        isAdvOpen = false;
        advTimer = advBreak;
        isCounterToAdv = false;
    }
}

public static class OnAdvFreeZone
{
    public static bool onAdvFreeZone;
}

