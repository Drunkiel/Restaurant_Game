using UnityEngine;

public class RepeatActions : MonoBehaviour
{
    public void RepeatAction()
    {
        ActionController _controller = GetComponent<ActionController>();
        _controller.actionIndex = 0;
    }
}
