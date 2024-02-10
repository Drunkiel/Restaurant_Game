using UnityEditor.Build;
using UnityEngine;

public class AnimationInteraction : MonoBehaviour
{
    public string[] animationNames;

    private bool isOpen;
    [SerializeField] private Animator animator;

    public void ChangeAnimation()
    {
        isOpen = !isOpen;
        animator.SetTrigger(animationNames[isOpen ? 0 : 1]);
    }
}
