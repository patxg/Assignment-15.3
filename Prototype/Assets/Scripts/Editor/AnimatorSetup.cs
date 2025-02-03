using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class AnimatorSetup : MonoBehaviour
{
    public Animator animator;
    public CharacterAnimationConfig animationConfig;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogError("Animator is missing! Assign the Animator component.");
            return;
        }

        if (animationConfig == null)
        {
            Debug.LogError("AnimationConfig is missing! Assign it in the Inspector.");
            return;
        }

        // Get the Animator Controller
        AnimatorController controller = animator.runtimeAnimatorController as AnimatorController;
        if (controller == null)
        {
            Debug.LogError("No Animator Controller found! Make sure the Animator component has a controller assigned.");
            return;
        }

        AnimatorStateMachine stateMachine = controller.layers[0].stateMachine;

        AddParameter(controller, "Speed", AnimatorControllerParameterType.Float);
        AddParameter(controller, "VelocityY", AnimatorControllerParameterType.Float);
        AddParameter(controller, "IsGrounded", AnimatorControllerParameterType.Bool);
        AddParameter(controller, "IsDashing", AnimatorControllerParameterType.Bool);
        AddParameter(controller, "IsAttacking", AnimatorControllerParameterType.Bool);

        Debug.Log("Animator Setup Completed Without Type Mismatch!");
    }

    void AddParameter(AnimatorController controller, string paramName, AnimatorControllerParameterType type)
    {
        var existingParam = System.Array.Find(controller.parameters, p => p.name == paramName);
        if (existingParam != null && existingParam.type == type)
        {
            Debug.Log($"Parameter '{paramName}' already exists with the correct type.");
            return;
        }

        if (existingParam != null && existingParam.type != type)
        {
            Debug.LogWarning($"Deleting incorrect parameter '{paramName}' ({existingParam.type}) and recreating it as {type}.");
            controller.RemoveParameter(existingParam);
        }

        controller.AddParameter(paramName, type);
        Debug.Log($"Added Animator Parameter: {paramName} ({type})");
    }
}
