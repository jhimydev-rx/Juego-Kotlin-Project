using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Oculta el cursor y lo bloquea al centro
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotación vertical de la cámara
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal del cuerpo
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
