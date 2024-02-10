using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    private void Update()
    {
        RotateTowardsPlayer();
    }

    private void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = Vector3.RotateTowards(transform.localPosition, Camera.main.transform.position, 360, 0);

        Quaternion rotationToPlayer = Quaternion.LookRotation(-directionToPlayer, Vector3.up);
        transform.rotation = rotationToPlayer;
    }
}
