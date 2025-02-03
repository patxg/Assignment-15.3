using UnityEngine;

public class CharacterAnimationSystem : MonoBehaviour
{
    public CharacterAnimationConfig animationConfig;
    private Animator animator;
    private CharacterState currentState;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component is missing!");
        }
    }

    public void ChangeState(CharacterState newState)
    {
        if (currentState == newState) return; // Prevent unnecessary animation switches

        currentState = newState;
        PlayAnimation(newState);
    }

    private void PlayAnimation(CharacterState state)
    {
        if (animationConfig == null || animator == null)
        {
            Debug.LogError("AnimationConfig or Animator is missing!");
            return;
        }

        Debug.Log($"🎬 Playing Animation: {state}");

        switch (state)
        {
            case CharacterState.Idle:
                animator.SetFloat("Speed", 0f);
                break;

            case CharacterState.Walking:
                animator.SetFloat("Speed", 1f);
                break;

            case CharacterState.Running:
                animator.SetBool("IsRunning", true);
                break;

            case CharacterState.Sprinting:
                animator.SetBool("IsRunning", true);
                break;

            case CharacterState.Jumping:
                animator.SetBool("IsGrounded", false);
                animator.SetFloat("VelocityY", 1f);
                break;

            case CharacterState.JumpFall:
                animator.SetFloat("VelocityY", -1f);
                break;

            case CharacterState.Landing:
                animator.SetBool("IsGrounded", true);
                break;

            case CharacterState.Dashing:
                animator.SetBool("IsDashing", true);
                break;

            case CharacterState.Attacking:
                animator.SetBool("IsAttacking", true);
                break;

            case CharacterState.Crouching:
                animator.SetBool("IsCrouching", true);
                break;

            case CharacterState.Rolling:
                animator.SetTrigger("Roll");
                break;

            default:
                animator.SetFloat("Speed", 0f);
                break;
        }
    }

    public void SetAnimatorParameter(string paramName, object value)
    {
        if (animator == null) return;

        if (value is float)
        {
            animator.SetFloat(paramName, (float)value);
        }
        else if (value is bool)
        {
            animator.SetBool(paramName, (bool)value);
        }
        else if (value is int)
        {
            animator.SetInteger(paramName, (int)value);
        }
        else
        {
            Debug.LogWarning($"Unknown Animator parameter type for `{paramName}`.");
        }

        Debug.Log($"Set Animator Parameter: {paramName} = {value}");
    }
}
