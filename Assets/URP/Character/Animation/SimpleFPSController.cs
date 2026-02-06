using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class SimpleFPSController_InputSystem : MonoBehaviour
{
    public Transform movementTotal;
    public Transform movementPartiel;
    public Transform headPivot;
    public float moveSpeed = 5f;
    public float mouseSensitivity = 150f;
    public float gravity = -9.81f;

    public GameObject rayGameObject;

    public float maxPitch = 80f;
    public float minPitch = -80f;
    public float maxYaw = 60f;
    public float minYaw = -60f;

    private CharacterController controller;
    private float pitch = 0f;
    private float yaw = 0f;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yaw = movementPartiel.localEulerAngles.y;
    }

    void Update()
    {
        if (Keyboard.current == null || Mouse.current == null || headPivot == null)
            return;

        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity * Time.deltaTime;
        bool isInspecting = Mouse.current.rightButton.isPressed;

        if (isInspecting)
        {
            yaw += mouseDelta.x;
            yaw = Mathf.Clamp(yaw, minYaw, maxYaw);

            movementPartiel.localEulerAngles = new Vector3(
                movementPartiel.localEulerAngles.x,
                yaw,
                0f
            );

            pitch -= mouseDelta.y;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
            headPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
        else
        {
            movementTotal.Rotate(Vector3.up * mouseDelta.x, Space.World);

            pitch -= mouseDelta.y;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
            headPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);

            yaw = 0f;
        }
    }

    void HandleMovement()
    {
        float h = 0f;
        float v = 0f;

        if (Keyboard.current.wKey.isPressed) v += 1f;
        if (Keyboard.current.sKey.isPressed) v -= 1f;
        if (Keyboard.current.dKey.isPressed) h += 1f;
        if (Keyboard.current.aKey.isPressed) h -= 1f;

        Vector3 forward = movementTotal.forward;
        Vector3 right = movementTotal.right;
        forward.y = 0f;
        right.y = 0f;

        Vector3 move = (forward * v + right * h).normalized;

        if (rayGameObject != null)
        {
            if (move.sqrMagnitude > 0.001f)
                rayGameObject.SetActive(false);
            else
                rayGameObject.SetActive(true);
        }

        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
