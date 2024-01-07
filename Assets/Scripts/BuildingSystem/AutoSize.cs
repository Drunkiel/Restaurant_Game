using UnityEngine;

public class AutoSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = GetScale();
    }

    private Vector3 GetScale()
    {
        return transform.parent.GetComponent<PlacableObject>().size + new Vector3(0.01f, 0.01f, 0.01f);
    }

    public void AutoDestroy()
    {
        Destroy(gameObject);
    }
}
