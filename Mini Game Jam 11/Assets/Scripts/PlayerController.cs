using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

	[Header("Set in Inspector")]
	public float speed = 0.01f;

	//[Header("Set Dynamically")]
	
	// Start is called before the first frame update
	void Start(){

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
}
