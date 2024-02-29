using UnityEngine;

public class AdvManager : MonoBehaviour
{
    float advTimer;
    float advBreak = 60f;
    AdvAlert advAlert;
    bool isCounterToAdv;

    PlayerController playerController;
    public static bool isAdvOpen = false;
    private void Awake()
    {
        transform.SetParent(null);
        advAlert = GetComponent<AdvAlert>();
        playerController = FindObjectOfType<PlayerController>();
    }
    private void Start()
    {
        ResetTimer();
#if !UNITY_EDITOR
        AdvPauseGame();
        ShowAdv();
#endif
    }
    private void Update()
    {
        advTimer -= Time.deltaTime;
        if (advTimer <= 0 && !isCounterToAdv && !AdvZone.insideNoAdvZone && !PlayerController.IsBusy)
        {
            AdvPauseGame();
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
        AdvPauseGame();
        YandexSDK.ShowRewardedADV();
    }

    public void AdvPauseGame()
    {
        isAdvOpen = true;
        Debug.Log("isAdvOpen " + isAdvOpen);
        playerController.BlockPlayersInput(true);
        CursorLocking.LockCursor(false);
    }
    //� Jslib
    public void AdvContinueGame()
    {
        isAdvOpen = false;
        if (PlayerController.IsBusy || UINavigation.isSettingsOpen)
            return;
        playerController.BlockPlayersInput(false);
        CursorLocking.LockCursor(true);
    }

    public void ResetTimer()
    {
        isCounterToAdv = false;
        advTimer = advBreak;
    }
}

public static class AdvZone
{
    public static bool insideNoAdvZone;
}

