using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float distance = 5;
    [SerializeField] float minVerticalAngle = -45;
    [SerializeField] float maxVerticalAngle = 45;
    [SerializeField] Vector2 framingOffset;
    [SerializeField] bool invertX;
    [SerializeField] bool invertY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float invertXVal = invertX ? -1 : 1;
        float invertYVal = invertY ? -1 : 1;

        float rotationX = transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * rotationSpeed * invertYVal;
        rotationX = Mathf.Clamp(rotationX > 180 ? rotationX - 360 : rotationX, minVerticalAngle, maxVerticalAngle);

        float rotationY = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed * invertXVal;

        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 focusPosition = followTarget.position + new Vector3(framingOffset.x, framingOffset.y, 0);

        transform.position = focusPosition - targetRotation * Vector3.forward * distance;
        transform.rotation = targetRotation;
    }
}