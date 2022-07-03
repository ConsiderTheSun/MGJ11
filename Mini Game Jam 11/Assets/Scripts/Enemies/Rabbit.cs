using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : Enemy{
	

	// Start is called before the first frame update
	protected override void Start(){
		base.Start();
	}

	// Update is called once per frame
	protected override void Update(){
		base.Update();
    //checks if the rabbit is touching the ground
		grounded = Physics2D.OverlapBox((transform.position - new Vector3(0, 0.51f, 0)),
																		new Vector2(0.5f, 0.5f), 0f, (LayerMask.GetMask("Platforms") |
                                    LayerMask.GetMask("Package1") | LayerMask.GetMask("Package2")
                                    | LayerMask.GetMask("Package3")));
    jumpTimer += Time.deltaTime;
    if(grounded && jumpTimer >= jumpCooldown){
			GetComponent<Rigidbody2D>().AddForce(jumpHeight * transform.up, ForceMode2D.Impulse);
			jumpTimer = 0;
		}
	}

	protected override void Move(){
		// determines the direction the rabbit wants to move in
		moveDirection = Vector3.zero;
		if(playerDirection.x > 0.1f){
			moveDirection = transform.right; 
		}
		else if(playerDirection.x < -0.1f){
			moveDirection = -transform.right;
		}

		// moves the rabbit
		transform.position += Time.deltaTime * speed * moveDirection;
	}

	protected override void Animate(){
		base.Animate();

		// changes the way the rabbit is facing
		if(moveDirection.x > 0.1f){
			enemySprite.flipX = true;
		}
		else if(moveDirection.x < -0.1f){
			enemySprite.flipX = false;
		}

		//checks if it is time to advance the animation
		if(animationTimer > moveAnimTime){
			// resets the timer
			animationTimer = 0;

			// updates the current frame and sets it
			animationFrame = (animationFrame+1)%movementSprites.Length;
			enemySprite.sprite = movementSprites[animationFrame];
		}

	}
}
