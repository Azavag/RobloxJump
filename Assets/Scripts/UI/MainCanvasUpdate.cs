using TMPro;
using UnityEngine;

public class MainCanvasUpdate : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI currentJumpText;
    [SerializeField]
    private TextMeshProUGUI passiveJumpText;
    [SerializeField]
    private TextMeshProUGUI activeJumpText;
    [SerializeField]
    private TextMeshProUGUI coinsText;


    private void OnEnable()
    {
        JumpHeightControl.JumpIncreasesChanged += OnStatsValueChanged;
        JumpHeightControl.CurrentJumpChanged += OnCurrentSpeedValueChanged;
        Wallet.CoinsChanged += OnCoinsValueChanged;
    }
    private void OnDisable()
    {
        JumpHeightControl.JumpIncreasesChanged -= OnStatsValueChanged;
        JumpHeightControl.CurrentJumpChanged -= OnCurrentSpeedValueChanged;
        Wallet.CoinsChanged -= OnCoinsValueChanged;
    }

    void OnCurrentSpeedValueChanged()
    {
        currentJumpText.text = Bank.Instance.playerInfo.currentJump.ToString();
    }
   
    void OnStatsValueChanged() 
    {
        passiveJumpText.text =
            (Bank.Instance.playerInfo.upgradePassiveJumpIncrease + Bank.Instance.playerInfo.skinsPassiveJumpIncrease)
            .ToString();
        activeJumpText.text = 
            (Bank.Instance.playerInfo.upgradeActiveJumpIncrease + Bank.Instance.playerInfo.skinsActiveJumpIncrease)
            .ToString();
    }

    void OnCoinsValueChanged()
    {
        coinsText.text = Bank.Instance.playerInfo.coins.ToString();
    }
}
