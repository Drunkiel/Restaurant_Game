using UnityEngine;

public class AutoSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = GetScale();
        transform.localPosition = GetPosition();
    }

    private Vector3 GetScale()
    {
        return transform.parent.GetComponent<PlacableObject>().size + new Vector3(0.01f, 0.01f, 0.01f);
    }

    private Vector3 GetPosition()
    {
        return transform.localPosition + transform.parent.GetComponent<BoxCollider>().center;
    }

    public void AutoDestroy()
    {
        Destroy(gameObject);
    }
}
