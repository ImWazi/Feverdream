using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float     speed;
    [SerializeField] private float     rotationSpeed;
    [SerializeField] private float     jumpForce;
    [SerializeField] private float     bunnyHopSpeedBoost;
    [SerializeField] private bool      shouldFaceMoveDirection = true;

    private Rigidbody rb;
    private Vector3   movement;
    private bool      isGrounded = true;
    private float     currentSpeed;

    void Start() {
       rb = GetComponent<Rigidbody>();
       currentSpeed = speed;
    }

    void Update() {
       Keyboard keyboard = Keyboard.current;

       Vector3 inputDirection = Vector3.zero;

       if (keyboard.wKey.isPressed) inputDirection += Vector3.forward;
       if (keyboard.sKey.isPressed) inputDirection -= Vector3.forward;
       if (keyboard.aKey.isPressed) inputDirection -= Vector3.right;
       if (keyboard.dKey.isPressed) inputDirection += Vector3.right;

       if (inputDirection.magnitude > 0) {
          inputDirection = inputDirection.normalized;
          
          Vector3 forward = cameraTransform.forward;
          Vector3 right   = cameraTransform.right;
          
          forward.y = 0;
          right.y   = 0;
          
          forward.Normalize();
          right.Normalize();
          
          movement = (forward * inputDirection.z + right * inputDirection.x).normalized;
          
          if (shouldFaceMoveDirection && movement.sqrMagnitude > 0.001f) {
             Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
             transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
          }
       } else {
          movement = Vector3.zero;
          currentSpeed = speed; 
       }
       
       
       if (keyboard.spaceKey.wasPressedThisFrame && isGrounded) {
          Jump();
          
          currentSpeed = Mathf.Min(currentSpeed * bunnyHopSpeedBoost, speed * 2f);
       }
       
       
       isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void Jump() {
       rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
       rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void FixedUpdate() {
       Vector3 newPosition = rb.position + movement * (currentSpeed * Time.fixedDeltaTime);
       rb.MovePosition(newPosition);
    }
}