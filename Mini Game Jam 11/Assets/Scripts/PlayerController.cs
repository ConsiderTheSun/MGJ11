using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

	[Header("Set in Inspector")]
	public GameController gameController;
	public PackageManager packageManager;
	public float speed = 0.01f;
	public float grabRange = 1f;
	[Header("Set Dynamically")]
	public int health = 3;

	GameObject heldPackage = null;

	// Start is called before the first frame update
	void Start(){

	}

	public void UpdatePlayer(){
		// if player clicks, try to grab/drop a package
		if(Input.GetMouseButtonDown(0)){
			if(heldPackage == null){
				GrabPackage();
			}
			else{
				DropPackage();
			}
		}

		if(heldPackage != null){
			int flip = GetComponent<SpriteRenderer>().flipX ? -1:1;
			heldPackage.transform.position = transform.position + flip*transform.right;
		}
	}
	public void FixedUpdatePlayer(){
		//transform.position += Time.deltaTime * transform.right * 0.1f;
		Move();
	}

	void Move(){
		// makes sure the player doesn't bug out
		if(GetComponent<Rigidbody2D>().velocity.magnitude > 15)
			GetComponent<Rigidbody2D>().velocity = 15 * GetComponent<Rigidbody2D>().velocity.normalized;

		// checks if moving
		Vector3 direction = Vector3.zero; 
		if(Input.GetKey("a")){
			direction = -transform.right;
		}
		else if(Input.GetKey("d")){
			direction = transform.right;
		}

		//temp direction
		if(Input.GetKey("w")){
			direction += transform.up;
		}


		GetComponent<Rigidbody2D>().AddForce(speed*direction);
	}


	public void SetPlayerPostion(Vector3 pos){
		transform.position = pos;
	}

	void GrabPackage(){
		// finds the closest package to the player
		GameObject package = packageManager.GetClosestPackage(transform.position);

		// if there are no packages on the map return
		if(package == null){
			return;
		}
		Debug.Log("Closest Package: " + package.name);

		//checks if the player is close enough to grab the package
		if(grabRange > Vector3.Distance(transform.position,package.transform.position)){
			Debug.Log("Grab!");
			heldPackage = package;
			// heldPackage.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	void DropPackage(){
		heldPackage.GetComponent<Rigidbody2D>().AddForce(10f* (new Vector3(1f,1f,0f)),ForceMode2D.Impulse);
		heldPackage = null;
	}

	// used to check if the player was hit
	private void OnCollisionEnter2D(Collision2D collision){

		if( collision.gameObject.layer == LayerMask.NameToLayer("Enemy")){
			gameController.TakeHit();
		}
	}
}
