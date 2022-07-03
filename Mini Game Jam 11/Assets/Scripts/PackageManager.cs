using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoBehaviour{
	[Header("Set in Inspector")]
	public GameController gameController;
	public GUIController guiController;
	public PlayerController player;
	public Transform spawnLocation;
	public Transform dropLocation1;
	public Transform dropLocation2;
	public Transform dropLocation3;

	public int[] spawnOrder;
	public GameObject packagePrefab;
	public Sprite[] packageSprites;
	public float packageScale = 1f;

	[Header("Set Dynamically")]
	List<GameObject> packageList = new List<GameObject>();
	int spawnIndex = 0;

	// Start is called before the first frame update
	void Start(){
		guiController.SetPackagesRemaining(spawnOrder.Length - spawnIndex);
		SpawnPackage();
	}

	// Update is called once per frame
	void Update(){

		CheckForDelivery();
		
	}
	void SpawnPackage(){
		GameObject newPackage = Instantiate(packagePrefab,spawnLocation);
		newPackage.GetComponent<SpriteRenderer>().sprite = packageSprites[spawnOrder[spawnIndex]];
		newPackage.transform.SetParent(null);
		newPackage.transform.position = spawnLocation.position;
		newPackage.transform.localScale = new Vector3(packageScale,packageScale,1f);

		newPackage.layer = LayerMask.NameToLayer("Package"+(spawnOrder[spawnIndex]+1));

		spawnIndex++;


		packageList.Add(newPackage);
	}
	void CheckForDelivery(){
		Collider2D deliveredPackage = Physics2D.OverlapBox(dropLocation1.position,
			new Vector2(dropLocation1.localScale.x,dropLocation1.localScale.y), 
			0f,LayerMask.GetMask("Package1"));

		if(deliveredPackage == null){
			deliveredPackage = Physics2D.OverlapBox(dropLocation2.position,
																		new Vector2(dropLocation2.localScale.x,dropLocation2.localScale.y), 
																		0f,LayerMask.GetMask("Package2"));
		}
		if(deliveredPackage == null){
			deliveredPackage = Physics2D.OverlapBox(dropLocation3.position,
																		new Vector2(dropLocation3.localScale.x,dropLocation3.localScale.y), 
																		0f,LayerMask.GetMask("Package3"));
		}

		if(deliveredPackage != null){
			//Debug.Log(deliveredPackage.gameObject.name + " " + deliveredPackage.gameObject.layer);

			packageList.Remove(deliveredPackage.gameObject);
			Destroy(deliveredPackage.gameObject);

			guiController.SetPackagesRemaining(spawnOrder.Length - spawnIndex);
			if(spawnIndex < spawnOrder.Length){
				SpawnPackage();
			}
			else{
				//gameController.EndGame(true);
				Debug.Log("WIN");
			}
		}
	}

	public GameObject GetClosestPackage(Vector3 goalPos){
		GameObject closestPackage = null;
		float bestDistance = Mathf.Infinity;
		foreach(GameObject package in packageList){
			
			float newDistance = Vector3.Distance(goalPos,package.transform.position);
			if(newDistance < bestDistance){
				bestDistance = newDistance;
				closestPackage = package;
			}
			//Debug.Log(package.name + " distance:" + newDistance);
		}
		return closestPackage;
	}
}
