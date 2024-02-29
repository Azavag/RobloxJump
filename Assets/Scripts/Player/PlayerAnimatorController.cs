using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] Animator animator;

  
    public void SetSpeedMultiplier(float speedMultiply)
    {
        animator.SetFloat("speedMultipler", speedMultiply);
    }

    public void PlayJumpAnimation(bool jumpState)
    {
        animator.SetBool("isJumping", jumpState);
    }
    public void PlayFallAnimation(bool fallState)
    {
        animator.SetBool("isFalling", fallState);
    }
    public void PlayRunAnimation(float speedValue)
    {
        animator.SetFloat("speed", speedValue);
    }
    public void PlayTrainAnimation(bool trainState)
    {
        if (trainState)
            animator.SetTrigger("train");
        else animator.SetTrigger("not_train");
    }
    
    public void MultiplierTrainAnimation(float multiplier)
    {
        animator.SetFloat("train_multiplier", multiplier);
    }


}
