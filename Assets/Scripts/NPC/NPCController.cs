using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public bool isNPCNamed;

    public float speed;
    private readonly float maxSpeed = 0.4f;
    public float rotationSpeed;

    public Vector2 movement;
    public bool isMoving;

    [SerializeField] private NPCNamesData _NPCNames;
    [SerializeField] private SkinnedMeshRenderer eyesRenderer;
    [SerializeField] private SkinnedMeshRenderer hairRenderer;
    [SerializeField] private SkinnedMeshRenderer bodyRenderer;

    [SerializeField] private Rigidbody rgBody;
    [SerializeField] private Animator anim;

    private void Start()
    {
        if (!isNPCNamed)
        {
            UpdateName();
            UpdateLook();
            isNPCNamed = true;
        }
    }

    private void Update()
    {
        isMoving = movement.magnitude > 0.01f;

        anim.SetFloat("Movement", movement.magnitude);

        if (rgBody.velocity.magnitude > maxSpeed)
            rgBody.velocity = Vector3.ClampMagnitude(rgBody.velocity, maxSpeed);
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(movement.x, 0, movement.y).normalized;
        rgBody.AddForce(move * speed, ForceMode.Acceleration);

        if (isMoving && move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            toRotation.x = 0f;
            toRotation.z = 0f;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void UpdateName()
    {
        ItemID _itemID = GetComponent<ItemID>();
        _itemID.itemName = _NPCNames.RandomName();
        transform.GetChild(transform.childCount - 1).GetChild(0).GetComponent<TMP_Text>().text = _itemID.itemName;
    }

    private void UpdateLook()
    {
        NPCLookController _controller = NPCLookController.instance;

        Mesh randomMesh = _controller.GetHairMesh();
        Material randomMaterial = _controller.GetMaterial();

        hairRenderer.sharedMesh = randomMesh;
        eyesRenderer.sharedMaterial = randomMaterial;
        hairRenderer.sharedMaterial = randomMaterial;
        bodyRenderer.sharedMaterial = randomMaterial;
    }
}
