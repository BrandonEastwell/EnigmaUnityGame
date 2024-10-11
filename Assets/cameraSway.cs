using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSway : MonoBehaviour
{
    public float sensitivity = 3.0f; // Mouse sensitivity
    public float smoothing = 2.0f;  // Mouse smoothing
    private Vector2 smoothMouse;
    private Vector2 currentMouse;
    private Vector2 playerRotation;

    private float swayAmount = 0.07f; // Base sway amount
    public float maxSwayAmount = 0.75f; // Maximum sway amount
    public float swaySpeed = 1.35f;  // Adjust the speed of the sway

    private Vector3 initialPosition;

    private float lastMouseX;

    public float tiltSpeed = 20.0f;
    private float currentTilt = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        initialPosition = transform.localPosition;
        lastMouseX = Input.mousePosition.x;
    }

    void Update()
    {
        // Mouse Input
        currentMouse = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Calculate the speed of mouse movement
        float mouseSpeed = Mathf.Abs(currentMouse.x - lastMouseX);

        // Adjust sway based on mouse speed
        float adjustedSwayAmount = Mathf.Lerp(swayAmount, maxSwayAmount, mouseSpeed * 2);

        // Smooth the mouse input
        smoothMouse.x = Mathf.Lerp(smoothMouse.x, currentMouse.x, 1f / smoothing);
        smoothMouse.y = Mathf.Lerp(smoothMouse.y, currentMouse.y, 1f / smoothing);


        //Tilt the camera based on mouse movement
        currentTilt += currentMouse.x * tiltSpeed * Time.deltaTime;
        currentTilt = Mathf.Clamp(currentTilt, -85.0f, 85.0f); // Limit the tilt angle
        Quaternion targetRotation = Quaternion.Euler(playerRotation.y, playerRotation.x, currentTilt);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);

        playerRotation += smoothMouse * sensitivity;

        // Apply rotation to the player object
        transform.localRotation = Quaternion.AngleAxis(-playerRotation.y, Vector3.right);

        // Camera Sway
        float swayX = Mathf.Sin(Time.time * swaySpeed) * adjustedSwayAmount;
        float swayY = Mathf.Sin(Time.time * swaySpeed * 2) * adjustedSwayAmount;
        Vector3 newLocalPosition = new Vector3(initialPosition.x + swayX, initialPosition.y + swayY, initialPosition.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, newLocalPosition, Time.deltaTime);

        // Update the last mouse X position
        lastMouseX = currentMouse.x;
    }
}
