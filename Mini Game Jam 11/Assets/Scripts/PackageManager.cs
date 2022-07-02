using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageManager : MonoBehaviour{
	[Header("Set in Inspector")]
	public PlayerController player;
	public Transform dropLocation1;
	public Transform dropLocation2;
	public Transform dropLocation3;

	[Header("Set Dynamically")]
	List<GameObject> packageList = new List<GameObject>();
	// Start is called before the first frame update
	void Start(){
		GameObject testPackage = GameObject.Find("thisIsAPackage");
		if(testPackage == null) 
			Debug.Log("Couldn't find test package");
		else
			packageList.Add(testPackage);

		testPackage = GameObject.Find("thisIsAPackageToo");

		if(testPackage == null) 
			Debug.Log("Couldn't find test package 2");
		else
			packageList.Add(testPackage);
	}

	// Update is called once per frame
	void Update(){
		Collider2D deliveredPackage = Physics2D.OverlapBox(dropLocation1.position,
																		new Vector2(1f,2f), 0f,LayerMask.GetMask("Package"));


		if(deliveredPackage != null){
			Debug.Log(deliveredPackage.gameObject.name + " " + deliveredPackage.gameObject.layer);
			player.RemovePackage();
			packageList.Remove(deliveredPackage.gameObject);
			Destroy(deliveredPackage.gameObject);
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
