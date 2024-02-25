using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyLevel : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField]
    private UINavigation uiNavigation;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private SceneSwapper sceneSwapper;
    [SerializeField]
    private SoundController soundController;

    [Header("UI Elements")]
    [SerializeField]
    private TextMeshProUGUI priceText;
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private Button cancelButton;

    Tween shakeTween;
    public static event Action<int> LevelUnlocked;

    private void OnEnable()
    {
        buyButton.onClick.AddListener(OnClickBuyButton);
        cancelButton.onClick.AddListener(OnClickCancelButton);

    }
    private void OnDisable()
    {
        buyButton.onClick.RemoveListener(OnClickBuyButton);
        cancelButton.onClick.RemoveListener(OnClickCancelButton);
    }
    private void OnDestroy()
    {
        shakeTween.Kill();
        buyButton.transform.DOKill();
    }

    private void Start()
    {
        priceText.text = sceneSwapper.GetUnlockPrice().ToString();
        shakeTween = ButtonAnimation.ShakeAnimation(buyButton.transform);
    }
    
    public void OpenBuyLevelPanel()
    {
        soundController.MakeClickSound();
        PlayerController.IsBusy = true;
        playerController.BlockPlayersInput(true);
        uiNavigation.ToggleOpenLevelCanvas(true);
        uiNavigation.ToggleJoystickCanvas(false);
        CursorLocking.LockCursor(false);
    }
    public void CloseBuyLevelPanel()
    {
        soundController.MakeClickSound();
        PlayerController.IsBusy = false;
        playerController.BlockPlayersInput(false);
        uiNavigation.ToggleOpenLevelCanvas(false);
        uiNavigation.ToggleJoystickCanvas(true);
        CursorLocking.LockCursor(true);
    }

    void OnClickBuyButton()
    {
        if(Bank.Instance.playerInfo.coins >= sceneSwapper.GetUnlockPrice())
        {
            ConfirmBuy();
            return;
        }
        DenyBuy();
    }
    void OnClickCancelButton()
    {
        CloseBuyLevelPanel();
    }

    void ConfirmBuy()
    {
        soundController.Play("ConfirmBuy");
        LevelUnlocked?.Invoke(-sceneSwapper.GetUnlockPrice());
        sceneSwapper.UnlockLevel();
        CloseBuyLevelPanel();
        sceneSwapper.SwapScene();
    }
    void DenyBuy()
    {
        soundController.Play("DeclineBuy");
        shakeTween.Pause();
        shakeTween.Rewind();
        shakeTween.Play();
    }
}
