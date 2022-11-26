using System;
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    /*public GameObject player;
    private Vector3 offset;*/
    public float mouseSensitivity = 600f;

    public Transform playerBody;

    private float xRotation = 0f;

    void Start () {
        // offset = transform.position - player.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

	private void Update()
	{
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Math.Clamp(xRotation, -90f, 90f);
		
		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		playerBody.Rotate(Vector3.up * mouseX);
	}
}
