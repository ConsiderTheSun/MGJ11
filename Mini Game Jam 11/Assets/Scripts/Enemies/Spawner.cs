using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{
	[Header("Set in Inspector")]
	public GameObject enemyPrefab;

	public float spawnInterval = 1f;
	public int maxSpawned = 5;

	[Header("Set Dynamically")]
	public float spawnTimer = 0f;
	// Start is called before the first frame update
	void Start(){
		
		// newEnemy.GetComponent<SpriteRenderer>().sprite = packageSprites[spawnOrder[spawnIndex]];
		// newEnemy.transform.SetParent(null);
		// newEnemy.transform.position = transform.position;
	}

	// Update is called once per frame
	void Update(){
		spawnTimer += Time.deltaTime;
		if(spawnTimer >= spawnInterval && maxSpawned > transform.childCount){
			//GameObject newEnemy = Instantiate(enemyPrefab,transform.position,Quaternion.identity);
			GameObject newEnemy = Instantiate(enemyPrefab,transform);
			newEnemy.transform.localPosition = Vector3.zero;
			spawnTimer = 0;
		}

	}
}
