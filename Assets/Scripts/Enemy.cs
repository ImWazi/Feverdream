using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField] private float speed            = 3f;
	[SerializeField] private float stoppingDistance = 0.5f;
    
	private Transform player;
	private Rigidbody rb;

	void Start() {
		Player playerScript = FindObjectOfType<Player>();
		if (playerScript != null) {
			player = playerScript.transform;
		}
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		if (player == null) return;
        
		
		Vector3 direction   = (player.position - transform.position).normalized;
		Vector3 newPosition = rb.position + direction * speed * Time.deltaTime;
		rb.MovePosition(newPosition);
        
		
		transform.LookAt(player);
	}
}