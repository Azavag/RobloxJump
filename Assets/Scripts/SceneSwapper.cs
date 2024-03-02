using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwapper : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private int nextSceneNumber;
    [SerializeField]
    private TextMeshProUGUI nextSceneNumberText;
    [SerializeField]
    private int unlockPrice;
    [SerializeField]
    private TextMeshProUGUI unlockPriceText;
    [SerializeField]
    private Image coinImage;
    [SerializeField]
    private bool isLevelUnlock;
    [Header("Refs")]
    [SerializeField]
    private BuyLevel buyLevel;
    [SerializeField]
    private FadeScreen fadeScreen;
    private SoundController soundController;

    string levelInterText;

    private void Awake()
    {
        soundController = FindObjectOfType<SoundController>();

        if (Language.Instance.languageName == LanguageName.Rus)
            levelInterText = "Уровень";
        else levelInterText = "Level";
    }
    private void Start()
    {
        isLevelUnlock = Bank.Instance.playerInfo.areLevelsUnlock[nextSceneNumber-1];
        if (isLevelUnlock)
            HidePriceElements();

        unlockPriceText.text = unlockPrice.ToString();
        nextSceneNumberText.text = $"{levelInterText} {nextSceneNumber}";
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!isLevelUnlock)
        {
            buyLevel.OpenBuyLevelPanel();
        }
        if(other.CompareTag("Player") && isLevelUnlock)
        {
            SwapScene();
        }
    }

    public void SwapScene()
    {
        soundController.Play("SceneSwap");
        fadeScreen.ExitLevelFadeIn(() => SceneManager.LoadScene(nextSceneNumber));
        Bank.Instance.playerInfo.currentLevelNumber = nextSceneNumber;
        YandexSDK.Save();
    }
    public int GetUnlockPrice()
    {
        return unlockPrice;
    }

    public void UnlockLevel()
    {
        isLevelUnlock = true;
        Bank.Instance.playerInfo.areLevelsUnlock[nextSceneNumber-1] = true;
    }
    void HidePriceElements()
    {
        unlockPriceText.enabled = false;
        coinImage.enabled = false;
    }
}
