using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 100f;

    private Vector2 movement;

    [SerializeField] private Rigidbody rgBody;

    private void Update()
    {
        // Obs�uga ruchu w p�aszczy�nie poziomej
        movement = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        // Ograniczenie pr�dko�ci
        if (rgBody.velocity.magnitude > speed) rgBody.velocity = rgBody.velocity.normalized * speed;

        Vector3 move = new Vector3(movement.x, 0, movement.y).normalized;
        rgBody.AddForce(move * speed);

        // Obracanie postaci tylko w kierunku, w kt�rym idzie
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            toRotation.x = 0f;
            toRotation.z = 0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
