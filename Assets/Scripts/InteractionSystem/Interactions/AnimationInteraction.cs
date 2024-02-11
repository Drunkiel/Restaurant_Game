using UnityEngine;

public class AnimationInteraction : MonoBehaviour
{
    public bool isOpen;
    public string[] animationNames;

    [SerializeField] private Animator animator;

    public void ChangeAnimation()
    {
        isOpen = !isOpen;
        animator.SetTrigger(animationNames[isOpen ? 0 : 1]);
    }
}
