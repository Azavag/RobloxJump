using ECM.Components;
using ECM.Controllers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerAnimatorController playerAnimatorController;
    private CharacterMovement characterMovement;
    private BaseCharacterController characterController;
    private CameraSensivityControl cameraSensivityControl;
    private bool inAir;

    public static bool IsBusy = false;
    private void OnEnable()
    {
        NotJumpZone.EnterNotJumpZone += delegate { BlockJump(true); };
        NotJumpZone.ExitNotJumpZone += delegate { BlockJump(false); };
    }
    private void OnDisable()
    {
        NotJumpZone.EnterNotJumpZone -= delegate { BlockJump(true); };
        NotJumpZone.ExitNotJumpZone -= delegate { BlockJump(false); };
    }
    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        characterController = GetComponent<BaseCharacterController>();
        playerAnimatorController = GetComponent<PlayerAnimatorController>();
        cameraSensivityControl = FindObjectOfType<CameraSensivityControl>();
    }
    private void Start()
    {
        IsBusy = false;
    }

    private void FixedUpdate()
    {      
        if (transform.position.y < -20)
            transform.position = Vector3.zero;
    } 

    private void LateUpdate()
    {
        playerAnimatorController.PlayRunAnimation(characterController.moveDirection.magnitude);
        playerAnimatorController.PlayJumpAnimation(characterController.isJumping);
        playerAnimatorController.PlayFallAnimation(characterController.isFalling);
    }

    public void BlockJump(bool state)
    {
        characterController.blockJump = state;
    }
    public void BlockPlayersInput(bool state)
    {
        characterMovement.velocity = Vector3.zero;
        characterController.moveDirection = Vector3.zero;
        characterController.isBlockInput = state;
        if (state)
            cameraSensivityControl.DisableCamera();
        else
            cameraSensivityControl.EnableCamera();
        BlockJump(state);
    }

    public void TrainSpeed(bool state)
    {
        BlockPlayersInput(state);
        playerAnimatorController.PlayTrainAnimation(state);
    }

    public void MuliplierTrainSpeed(float multiplier)
    {
        playerAnimatorController.MultiplierTrainAnimation(multiplier);
    }

}
