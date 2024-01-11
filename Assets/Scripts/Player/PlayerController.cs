using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;

    private Vector2 movement;
    public bool isMoving;

    [SerializeField] private Rigidbody rgBody;
    [SerializeField] private Animator anim;

    private void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        isMoving = movement.magnitude > 0.1f;
        anim.SetFloat("Movement", movement.magnitude);

        if (rgBody.velocity.magnitude >= speed)
            rgBody.velocity = Vector3.ClampMagnitude(rgBody.velocity, speed);
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(movement.x, 0, movement.y).normalized;
        rgBody.AddForce(move * speed);

        if (isMoving)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            toRotation.x = 0f;
            toRotation.z = 0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
