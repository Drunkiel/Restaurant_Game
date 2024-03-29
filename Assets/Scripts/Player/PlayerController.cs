using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private readonly float maxSpeed = 1.2f;
    public float rotationSpeed;

    private Vector2 movement;
    public bool isMoving;

    [SerializeField] private Rigidbody rgBody;
    [SerializeField] private Animator anim;

    private void Update()
    {
        ItemHolder _itemHolder = ItemHolder.instance;

        //Movement control and animations
        anim.SetBool("isMaking", InteractionSystem.isInteracting);
        if (InteractionSystem.isInteracting || BuildingSystem.inBuildingMode) return;

        isMoving = movement.magnitude > 0.1f;
        anim.SetFloat("Movement", movement.magnitude);
        anim.SetBool("isHolding", _itemHolder.isHoldingItem || _itemHolder.isHoldingStackableItem);

        if (rgBody.velocity.magnitude > maxSpeed)
            rgBody.velocity = Vector3.ClampMagnitude(rgBody.velocity, maxSpeed);
    }

    private void FixedUpdate()
    {
        if (InteractionSystem.isInteracting || BuildingSystem.inBuildingMode) return;

        Vector3 move = new Vector3(movement.x, 0, movement.y).normalized;
        rgBody.AddForce(move * speed, ForceMode.Acceleration);

        if (isMoving)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            toRotation.x = 0f;
            toRotation.z = 0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();

        switch (CameraController.instance.currentTarget)
        {
            case 0:
                switch (CameraController.instance.state)
                {
                    case 0:
                        movement = inputValue;
                        break;

                    case 1:
                        movement = new Vector2(inputValue.y, -inputValue.x);
                        break;

                    case 2:
                        movement = -inputValue;
                        break;

                    case 3:
                        movement = new Vector2(-inputValue.y, inputValue.x);
                        break;
                }
                break;

            case 1:
                movement = inputValue;
                break;
        }
    }
}
