using TMPro;
using UnityEngine;

public class OverallSpeedValue : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI overallSpeedText;


    private void OnEnable()
    {
        JumpHeightControl.CurrentJumpChanged += OnCurrentSpeedChange;
    }
    private void OnDisable()
    {
        JumpHeightControl.CurrentJumpChanged -= OnCurrentSpeedChange;
    }
    void OnCurrentSpeedChange()
    {
        overallSpeedText.text = Bank.Instance.playerInfo.overallJump.ToString();
        YandexSDK.SetNewLeaderboardValue(Bank.Instance.playerInfo.overallJump);
    }
}
