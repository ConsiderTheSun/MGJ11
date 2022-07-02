using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

	[Header("Set in Inspector")]
	public GameController gameController;
	public PackageManager packageManager;
	public Sprite[] movementSprites;
	public Sprite[] idleSprites;
	public float speed = 0.01f;
	public float jumpStrength = 1f;
	public float grabRange = 1f;
	public float knockbackLR = 300f;
	public float knockbackUD = 100f;
	public float moveAnimTime = 1f;
	public float idleAnimTime = 1f;

	[Header("Set Dynamically")]
	public int health = 3;

	bool grounded = false;
	float jumpCooldown = 0.3f;
	float jumpTimer = 0;
	GameObject heldPackage = null;

	public enum Mode {Idle, Moving};

	Mode currentMode = Mode.Idle;
	float animationTimer = 0f;
	int animationFrame = 0;

	// Start is called before the first frame update
	void Start(){

	}

	public void UpdatePlayer(){
		UpdateMode();
		// if player clicks, try to grab/drop a package
		if(Input.GetMouseButtonDown(0)){
			if(heldPackage == null){
				GrabPackage();
			}
			else{
				DropPackage();
			}
		}

		AnimatePlayer();

	}
	public void FixedUpdatePlayer(){
		Move();
	}

	void UpdateMode(){
		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 0.2f){
			currentMode = Mode.Moving;
		}
		else{
			currentMode = Mode.Idle;
		}
	}

	void AnimatePlayer(){
		animationTimer += Time.deltaTime;

		//checks if it is time to advance the animation
		if(currentMode == Mode.Idle){
			GetComponent<SpriteRenderer>().sprite = idleSprites[0];
		}
		//checks if it is time to advance the animation
		else if(currentMode == Mode.Moving && animationTimer > moveAnimTime){
			// resets the timer
			animationTimer = 0;

			// updates the current frame and sets it
			animationFrame = (animationFrame+1)%movementSprites.Length;
			GetComponent<SpriteRenderer>().sprite = movementSprites[animationFrame];
		}


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
		//transform.position += Time.deltaTime * speed*direction;
		//checks if the player is touching the ground
		grounded = Physics2D.OverlapBox(transform.position - new Vector3(0,1.0f,0),
																		new Vector2(0.3f,0.01f),0f, LayerMask.GetMask("Platforms"));

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
			heldPackage.gameObject.SetActive(false);
		}
	}

	void DropPackage(){
		// int flip = GetComponent<SpriteRenderer>().flipX ? -1:1;
		heldPackage.transform.position = transform.position; // + flip*transform.right;
		heldPackage.gameObject.SetActive(true);
		//heldPackage.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		//heldPackage.GetComponent<Rigidbody2D>().AddForce(10f* (new Vector3(1f,1f,0f)),ForceMode2D.Impulse);
		heldPackage = null;
	}

	void HoldPackage(){
		int flip = GetComponent<SpriteRenderer>().flipX ? -1:1;
		heldPackage.transform.position = transform.position + flip*transform.right;
	}

	// used to check if the player was hit
	private void OnCollisionEnter2D(Collision2D collision){

		if( collision.gameObject.layer == LayerMask.NameToLayer("Enemy")){
			Debug.Log("hit");
			gameController.TakeHit();
			Vector2 difference = (transform.position - collision.transform.position).normalized;
			if(difference.x < 0) {
				GetComponent<Rigidbody2D>().AddForce(transform.up * knockbackUD + (transform.right * knockbackLR) * -1);
			}
			else {
				GetComponent<Rigidbody2D>().AddForce(transform.up * knockbackUD + transform.right * knockbackLR);
			}
		}
	}
}
