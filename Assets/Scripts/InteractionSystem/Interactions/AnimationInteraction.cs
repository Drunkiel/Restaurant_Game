using UnityEngine;

public class AnimationInteraction : MonoBehaviour
{
    public bool isOpen;
    public string[] animationTriggers;

    [SerializeField] private Animator animator;
    private HintEvent _hintEvent;

    private void Start()
    {
        if (TryGetComponent(out HintEvent _hintEvent))
            this._hintEvent = _hintEvent;
    }

    public void ChangeAnimation()
    {
        isOpen = !isOpen;
        animator.SetTrigger(animationTriggers[isOpen ? 0 : 1]);

        if (_hintEvent != null)
            _hintEvent.addOne = isOpen;
    }

    public void PlayAnimationIndex(int index)
    {
        animator.SetTrigger(animationTriggers[index]);
    }
}
