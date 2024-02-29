using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class TrainZone : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private Button quitButton;

    [Header("Objects")]
    [SerializeField]
    private Transform playerInsidePoint;
    [SerializeField]
    private Transform playerOutsidePoint;
    [SerializeField]
    private CinemachineVirtualCamera trainZoneCamera;

    [Header("Click")]
    [SerializeField]
    private Button clickButton;
    private float clickInterval = 0.6f;
    private int streakMultiply = 1;
    private float timeAfterClick;
    private int clickStreak = 0;
    private int maxClickStreak = 60;
    private bool isClickCount;
    private bool isPlayerTrain;

    [Header("Refs")]
    [SerializeField]
    private PlayerController playerController;
    private UINavigation uiNavigation;
    private JumpHeightControl speedControl;
    private ClickStreakAnimation clickStreakAnimation;
    private SoundController soundController;
    private void OnEnable()
    {
        quitButton.onClick.AddListener(RemovePlayerFromTrainZone);
        clickButton.onClick.AddListener(OnClickScreen);
    }
    private void OnDisable()
    {
        quitButton.onClick.RemoveListener(RemovePlayerFromTrainZone);
        clickButton.onClick.RemoveListener(OnClickScreen);
    }
    private void Awake()
    {
        clickStreakAnimation = GetComponent<ClickStreakAnimation>();
        uiNavigation = FindObjectOfType<UINavigation>();
        speedControl = FindObjectOfType<JumpHeightControl>();
        soundController = FindObjectOfType<SoundController>();
    }
    private void Start()
    {
        if(IsMobileController.IsMobile)
            clickButton.gameObject.SetActive(true);
        else
            clickButton.gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        if (!isPlayerTrain)
            return;
        if (!CheckButtonClick())
            return;

        Click();       
    }
 
    void OnClickScreen()
    {
        Click();
    }
    void Click()
    {      
        isClickCount = true;
        speedControl.ActiveIncreaseCurrentSpeed(streakMultiply);
        if (timeAfterClick <= clickInterval)
        {
            clickStreak++;           
            ClickStreakCheck();
            timeAfterClick = 0;
            FillImage();
            ChangeAnimMultiplier();
        }
    }
    private void FixedUpdate()
    {
        if (isClickCount)
        {
            timeAfterClick += Time.deltaTime;
            if (timeAfterClick >= clickInterval)
            {
                timeAfterClick = 0;
                clickStreak = 0;
                streakMultiply = 1;
                isClickCount = false;
                FillImage();
                ChangeAnimMultiplier();
                clickStreakAnimation.DeactivateStreakText(0);
                clickStreakAnimation.DeactivateStreakText(1);
                clickStreakAnimation.DeactivateStreakText(2);
            }
        }
    }


    bool CheckButtonClick()
    {
        return !IsMobileController.IsMobile && Input.GetKeyDown(KeyCode.Space);
    }
    void FillImage()
    {
        float targetValue = 1 - ((float)clickStreak / maxClickStreak);
        fillImage.fillAmount = targetValue;        
    }
    void ChangeAnimMultiplier()
    {
        float animationMultiplier = Mathf.Clamp(clickStreak / 10f, 1, 4);
        playerController.MuliplierTrainSpeed(animationMultiplier);
    }

    void ClickStreakCheck()
    {
        switch (clickStreak)
        {
            case 15:
                streakMultiply = 2;
                soundController.Play("FillUp_1");
                clickStreakAnimation.ActivateStreakText(0);
                break;
            case 30:
                streakMultiply = 3;
                soundController.Play("FillUp_2");
                clickStreakAnimation.DeactivateStreakText(0);
                clickStreakAnimation.ActivateStreakText(1);
                break;
            case 45:
                streakMultiply = 4;
                soundController.Play("FillUp_3");
                clickStreakAnimation.DeactivateStreakText(1);
                clickStreakAnimation.ActivateStreakText(2);
                break;
            default: 
                break;
        }            
    }

    public void MovePlayerToTrainZone()
    {
        PlayerController.IsBusy = true;
        soundController.MakeClickSound();
        playerController.TrainSpeed(true);            
        uiNavigation.ToggleTrainCanvas(true);
        uiNavigation.ToggleJoystickCanvas(false);
        playerController.transform.position = playerInsidePoint.position;
        playerController.transform.eulerAngles = playerInsidePoint.eulerAngles;
        trainZoneCamera.gameObject.SetActive(true);
        isPlayerTrain = true;
        CursorLocking.LockCursor(false);
    }

    void RemovePlayerFromTrainZone()
    {
        PlayerController.IsBusy = false;
        soundController.MakeClickSound();
        isPlayerTrain = false;
        trainZoneCamera.gameObject.SetActive(false);
        uiNavigation.ToggleTrainCanvas(false);
        uiNavigation.ToggleJoystickCanvas(true);
        playerController.TrainSpeed(false);
        playerController.transform.SetPositionAndRotation(playerOutsidePoint.position,
            playerOutsidePoint.rotation);
        CursorLocking.LockCursor(true);
        
    }
}
