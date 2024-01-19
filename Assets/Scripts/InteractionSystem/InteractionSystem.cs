using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public static InteractionSystem instance;
    public static bool isInteracting;

    public PlacableObject _objectToPlace;

    private void Awake()
    {
        instance = this;
    }
}
