using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCameraController : MonoBehaviour {
	[SerializeField] private float zoomSpeed;
	[SerializeField] private float zoomLerpSpeed;
	[SerializeField] private float minZoom;
	[SerializeField] private float maxZoom;
	
	private InputSystem_Actions actions;

	private CinemachineCamera        cam;
	private CinemachineOrbitalFollow orbital;
	private Vector2                  scrollDelta;

	private float targetZoom;
	private float currentZoom;

	private void Start() {
		actions = new InputSystem_Actions();
		actions.Enable();
		actions.CameraControls.MouseZoom.performed += HandleMouseScroll;
		
		Cursor.lockState = CursorLockMode.Locked;
		
		cam = GetComponent<CinemachineCamera>();
		orbital = cam.GetComponent<CinemachineOrbitalFollow>();

		targetZoom = currentZoom = orbital.Radius;
	}

	private void HandleMouseScroll(InputAction.CallbackContext context) {
		scrollDelta = context.ReadValue<Vector2>();
	}

	void Update() {
		if (scrollDelta.y != 0) {
			if (orbital != null) {
				targetZoom = Mathf.Clamp(orbital.Radius - scrollDelta.y * zoomSpeed, minZoom, maxZoom);
				scrollDelta = Vector2.zero;
			}
				
		}
		
		currentZoom = Mathf.Lerp(currentZoom, targetZoom,Time.deltaTime * zoomLerpSpeed);
		orbital.Radius = currentZoom;
	}
}
