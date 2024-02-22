using UnityEngine;

public class AnimationInteraction : MonoBehaviour
{
    public bool isOpen;
    public string[] animationNames;

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
        animator.SetTrigger(animationNames[isOpen ? 0 : 1]);

        if (_hintEvent != null)
            _hintEvent.addOne = isOpen;
    }
}
