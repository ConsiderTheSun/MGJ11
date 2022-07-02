using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

	[Header("Set in Inspector")]
	public GameController gameController;
	public PackageManager packageManager;
	public float speed = 0.01f;
	public float jumpStrength = 1f;
	public float grabRange = 1f;
	[Header("Set Dynamically")]
	public int health = 3;

	bool grounded = false;
	float jumpCooldown = 0.3f;
	float jumpTimer = 0;
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
			HoldPackage();
			
		}
	}
	public void FixedUpdatePlayer(){
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
			GetComponent<SpriteRenderer>().flipX = true;
		}
		else if(Input.GetKey("d")){
			direction = transform.right;
			GetComponent<SpriteRenderer>().flipX = false;
		}


		GetComponent<Rigidbody2D>().AddForce(speed*direction);

		//checks if the player is touching the ground
		grounded = Physics2D.OverlapBox(transform.position - new Vector3(0,1.0f,0),
																		new Vector2(0.3f,0.01f),0f, LayerMask.GetMask("Platforms") | LayerMask.GetMask("Package"));

		//jump
		if(Input.GetKey("space") && grounded && jumpTimer >= jumpCooldown){
			//Debug.Log("Jump!");
			GetComponent<Rigidbody2D>().AddForce(jumpStrength*transform.up, ForceMode2D.Impulse);
			jumpTimer = 0;
		}

		if(jumpTimer < jumpCooldown){
			jumpTimer += Time.deltaTime;
		}
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
		//Debug.Log("Closest Package: " + package.name);

		//checks if the player is close enough to grab the package
		if(grabRange > Vector3.Distance(transform.position,package.transform.position)){
			//Debug.Log("Grab!");
			heldPackage = package;
		}
	}

	void DropPackage(){
		heldPackage.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		heldPackage.GetComponent<Rigidbody2D>().AddForce(10f* (new Vector3(1f,1f,0f)),ForceMode2D.Impulse);
		heldPackage = null;
	}

	public void RemovePackage(){
		heldPackage = null;
	}

	void HoldPackage(){
		int flip = GetComponent<SpriteRenderer>().flipX ? -1:1;
		heldPackage.transform.position = transform.position + flip*transform.right;
	}

	// used to check if the player was hit
	private void OnCollisionEnter2D(Collision2D collision){

		if( collision.gameObject.layer == LayerMask.NameToLayer("Enemy")){
			gameController.TakeHit();
		}
	}
}
