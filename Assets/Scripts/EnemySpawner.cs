using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private Transform  player;
	[SerializeField] private float      spawnRadius;
	[SerializeField] private float      spawnDelay;
	[SerializeField] private float      waveDelay;
	[SerializeField] private float      difficultyScaling;

	private int currentWave        = 0;
	private int baseEnemiesPerWave = 3;

	void Start() {
		StartCoroutine(SpawnWaves());
	}

	IEnumerator SpawnWaves() {
		while (true) {
			currentWave++;
			int enemiesToSpawn = Mathf.RoundToInt(baseEnemiesPerWave * Mathf.Pow(difficultyScaling, currentWave - 1));
            
			Debug.Log("Wave " + currentWave + ": Spawning " + enemiesToSpawn + " enemies");
            
			for (int i = 0; i < enemiesToSpawn; i++) {
				SpawnEnemy();
				yield return new WaitForSeconds(spawnDelay);
			}
            
			yield return new WaitForSeconds(waveDelay);
		}
	}

	void SpawnEnemy() {
		Vector3 randomPosition = player.position + Random.insideUnitSphere * spawnRadius;
		randomPosition.y = 40f;
        
		Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
	}
}