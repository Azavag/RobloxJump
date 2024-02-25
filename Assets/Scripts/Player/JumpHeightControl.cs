using ECM.Components;
using ECM.Controllers;
using System;
using TMPro;
using UnityEngine;

public class JumpHeightControl : MonoBehaviour
{
    [Header("SPEED")]
    [SerializeField]
    private bool isJumpIncrease;
    [SerializeField]
    private uint minLevelJump;
    [SerializeField]
    private int jumpKoeficient;

    private float jumpIncreaseTimer = 0f;
    private float jumpIncreaseInterval = 1f;

    [SerializeField]
    private BaseCharacterController characterController;
    [SerializeField]
    private PlayerAnimatorController playerAnimatorController;

    uint upgradesPassiveJumpIncrease;
    uint upgradesActiveJumpIncrease;
    uint skinsPassiveJumpIncrease;
    uint skinsActiveJumpIncrease;
    uint passiveJumpIncrease;
    uint activeJumpIncrease;
    uint currentJumpHeight;

    public static event Action JumpIncreasesChanged;
    public static event Action CurrentJumpChanged;
    [SerializeField]
    private SkinCharacteristics[] skinsCharacteristics;

    float saveTimer;
    float saveInterval = 3;
    private void Awake()
    {
        characterController = FindObjectOfType<BaseCharacterController>();
        playerAnimatorController = FindObjectOfType<PlayerAnimatorController>();
        
    }
    private void OnEnable()
    {
        CourseFinish.CourseFinished += ResetJumpToMinLevel;
        SkinCharacteristics.CharacteristicsChanged += OnSkinsCharacteristicsChanged;
    }
    private void OnDisable()
    {
        CourseFinish.CourseFinished -= ResetJumpToMinLevel;
        SkinCharacteristics.CharacteristicsChanged -= OnSkinsCharacteristicsChanged;
    }

    void Start()
    {
        Initialization();
        ResetTimer();
        saveTimer = saveInterval;
    }
    void Initialization()
    {
        currentJumpHeight = Bank.Instance.playerInfo.currentJump;
        currentJumpHeight = (uint)Mathf.Max(currentJumpHeight, minLevelJump);
        skinsPassiveJumpIncrease = Bank.Instance.playerInfo.skinsPassiveJumpIncrease;
        skinsActiveJumpIncrease = Bank.Instance.playerInfo.skinsActiveJumpIncrease;
        upgradesPassiveJumpIncrease = Bank.Instance.playerInfo.upgradePassiveJumpIncrease;   
        upgradesActiveJumpIncrease = Bank.Instance.playerInfo.upgradeActiveJumpIncrease;

        passiveJumpIncrease = upgradesPassiveJumpIncrease + skinsPassiveJumpIncrease;
        activeJumpIncrease = upgradesActiveJumpIncrease + skinsActiveJumpIncrease;
        ChangePlayerCurrentJump();
        JumpIncreasesChanged?.Invoke();
    }
  
    void Update()
    {
        if (!isJumpIncrease)
            return;
        SaveTimer();
        if (isTimeToIncrease() )
        {
            PassiveIncreaseCurrentSpeed();
            ResetTimer();
        }
    }
    //По ивенту
    void OnSkinsCharacteristicsChanged()
    {
        skinsPassiveJumpIncrease = 0;
        skinsActiveJumpIncrease = 0;
        foreach (var characteristic in skinsCharacteristics)
        {
            skinsPassiveJumpIncrease += characteristic.GetPassiveStats();
            skinsActiveJumpIncrease += characteristic.GetActiveStats();
        }

        Bank.Instance.playerInfo.skinsActiveJumpIncrease = skinsActiveJumpIncrease;
        Bank.Instance.playerInfo.skinsPassiveJumpIncrease = skinsPassiveJumpIncrease;
        YandexSDK.Save();
        ChangeJumpIncreases();
    }

    void PassiveIncreaseCurrentSpeed()
    {
        currentJumpHeight += passiveJumpIncrease;
        Bank.Instance.playerInfo.overallJump += passiveJumpIncrease;
        ChangePlayerCurrentJump();
    }
    public void ActiveIncreaseCurrentSpeed(int streakMultiply)
    {
        currentJumpHeight += (uint)((activeJumpIncrease) * streakMultiply);
        Bank.Instance.playerInfo.overallJump += (uint)((activeJumpIncrease) * streakMultiply);
        ChangePlayerCurrentJump();
    }

    public void ChangeUpgradesPassiveSpeedIncrease(int upgradesDiff)
    {
        upgradesPassiveJumpIncrease += (uint)upgradesDiff;
        Bank.Instance.playerInfo.upgradePassiveJumpIncrease = upgradesPassiveJumpIncrease;
        YandexSDK.Save();
        ChangeJumpIncreases();
    }
    public void ChangeUpgradesActiveSpeedIncrease(int upgradesDiff)
    {
        upgradesActiveJumpIncrease += (uint)upgradesDiff;
        Bank.Instance.playerInfo.upgradeActiveJumpIncrease = upgradesActiveJumpIncrease;
        YandexSDK.Save();
        ChangeJumpIncreases();
    }
    void ChangeJumpIncreases()
    {       
        passiveJumpIncrease = skinsPassiveJumpIncrease + upgradesPassiveJumpIncrease;
        activeJumpIncrease = skinsActiveJumpIncrease + upgradesActiveJumpIncrease;
        JumpIncreasesChanged?.Invoke();
    }

    void ChangePlayerCurrentJump()
    {
        Bank.Instance.playerInfo.currentJump = currentJumpHeight;
        characterController.baseJumpHeight = (float)currentJumpHeight / jumpKoeficient;

       // UpdateSpeedAnimation();
        CurrentJumpChanged?.Invoke();
    }

    void UpdateSpeedAnimation()
    {
        float speedAnimationMultiplier = (float)currentJumpHeight / minLevelJump / 5;
        speedAnimationMultiplier = Mathf.Clamp(speedAnimationMultiplier, 1f, 3f);
        playerAnimatorController.SetSpeedMultiplier(speedAnimationMultiplier);
    }
    public void ResetJumpToMinLevel(int diff)
    {
        currentJumpHeight = minLevelJump;
        ChangePlayerCurrentJump();
    }

    bool isTimeToIncrease()
    {
        jumpIncreaseTimer -= Time.deltaTime;
        return jumpIncreaseTimer <= 0;
    }
    void ResetTimer()
    {
        jumpIncreaseTimer = jumpIncreaseInterval;
    }

    void SaveTimer()
    {
        saveTimer -= Time.deltaTime;
        if (saveTimer <= 0)
        {
            YandexSDK.Save();
            saveTimer = saveInterval;
        }
    }
    public int GetLevelKoeficient()
    {
        return jumpKoeficient;
    }


}
