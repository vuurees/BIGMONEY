using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Mouse sensitivity for looking around
    public Transform playerBody;          // Reference to the player object (the one holding the Rigidbody or movement script)

    private float xRotation = 0f;         // Used to clamp vertical camera rotation

    void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the player body around the Y axis (horizontal rotation)
        playerBody.Rotate(Vector3.up * mouseX);

        // Adjust vertical rotation (X axis) and clamp it between -90 and 90 degrees
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply the vertical rotation to the camera (local rotation)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
