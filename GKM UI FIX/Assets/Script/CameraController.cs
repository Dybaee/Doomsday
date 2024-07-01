using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // Variables
    [SerializeField] Transform followTarget;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float distance = 5;
    [SerializeField] float minVerticalAngle = -45;
    [SerializeField] float maxVerticalAngle = 45;
    [SerializeField] Vector2 framingOffset;
    [SerializeField] bool invertX;
    [SerializeField] bool invertY;
    [SerializeField] float collisionCam = 0.2f; 

    private bool isPause = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        // Rotate the camera if the game is not paused
        if (!isPause)
        {
            RotateCamera();
        }
    }

    public void Pause()
    {
        isPause = !isPause;

        // Pause the game & unlock the cursor
        if (isPause)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Unpause the game & lock the cursor
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void RotateCamera()
    {
        // Determine the inversion values
        float invertXVal = invertX ? -1 : 1;
        float invertYVal = invertY ? -1 : 1;

        // Calculate the new rotation angles
        float rotationX = transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * rotationSpeed * invertYVal;
        rotationX = Mathf.Clamp(rotationX > 180 ? rotationX - 360 : rotationX, minVerticalAngle, maxVerticalAngle);

        float rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed * invertXVal;

        // Create the target rotation based on the calculated angles
        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y, 0);

        // Calculate desired camera position
        Vector3 cullingCam = focusPosition - targetRotation * Vector3.forward * distance;

        // Check for collisions 
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("Default");  // To set camera not auto zoom 
        if (Physics.Raycast(focusPosition, cullingCam - focusPosition, out hit, distance, layerMask))
        {
            // Ignore triggers
            if (hit.collider.isTrigger)
            {
                // Set the camera to the desired position if the hit collider is a trigger
                transform.position = cullingCam;
            }
            else
            {
                // Adjust the camera position if a collision with a non-trigger collider is detected
                transform.position = hit.point - (cullingCam - focusPosition).normalized * collisionCam;
            }
        }
        else
        {
            // Set the camera to the desired position if no collision is detected
            transform.position = cullingCam;
        }

        // Set the camera rotation to the target rotation
        transform.rotation = targetRotation;
    }
}
