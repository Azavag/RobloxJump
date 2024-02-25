using TMPro;
using UnityEngine;

public class UpgradesStatsShow : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI passiveUpgradeStatsText;
    [SerializeField]
    private TextMeshProUGUI activeUpgradeStatsText;

    string secInterText, clickInterText;
    private void OnEnable()
    {
        SetInternationalText();
        JumpHeightControl.JumpIncreasesChanged += OnStatsValueChanged;
    }
    private void OnDisable()
    {
        JumpHeightControl.JumpIncreasesChanged -= OnStatsValueChanged;
    }
    public void UpdateUpgradeText(TextMeshProUGUI _textField, string _text)
    {
        _textField.text = _text;
    }
    void OnStatsValueChanged()
    {
        string _text = $"+{Bank.Instance.playerInfo.upgradePassiveJumpIncrease}/{secInterText}";
        UpdateUpgradeText(passiveUpgradeStatsText, _text);
        _text = $"+{Bank.Instance.playerInfo.upgradeActiveJumpIncrease}/{clickInterText}";
        UpdateUpgradeText(activeUpgradeStatsText, _text);
    }

    void SetInternationalText()
    {
        if (Language.Instance.languageName == LanguageName.Rus)
        {
            secInterText = "сек";
            clickInterText = "клик";
        }
        else
        {
            secInterText = "sec";
            clickInterText = "click";
        }
    }
}
