using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour {
	[SerializeField] private Transform player;
	[SerializeField] private float     mouseSensitivity = 2f;
	[SerializeField] private float     distance         = 5f;
	[SerializeField] private float     height           = 3f;

	private float rotationX = 0f;
	private float rotationY = 0f;

	private void LateUpdate() {
		Vector2 mouseDelta = Mouse.current.delta.ReadValue();
		Debug.Log("Mouse Delta: " + mouseDelta);
   
		rotationY += mouseDelta.x * mouseSensitivity;
		rotationX -= mouseDelta.y * mouseSensitivity;
		rotationX =  Mathf.Clamp(rotationX, -30f, 60f);
   
		Debug.Log("RotationY: " + rotationY);
   
		Vector3 offset = new Vector3(0, height, -distance);
		offset = Quaternion.Euler(rotationX, rotationY, 0) * offset;
   
		transform.position = player.position + offset;
		transform.LookAt(player.position + Vector3.up * (height * 0.5f));
	}
}