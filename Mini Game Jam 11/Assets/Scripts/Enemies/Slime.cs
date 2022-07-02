using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy{
	

	// Start is called before the first frame update
	protected override void Start(){
		base.Start();
	}

	// Update is called once per frame
	protected override void Update(){
		base.Update();
	}

	protected override void Move(){
		// determines the direction the slime wants to move in
		moveDirection = Vector3.zero;
		if(playerDirection.x > 0.1f){
			moveDirection = transform.right; 
		}
		else if(playerDirection.x < -0.1f){
			moveDirection = -transform.right;
		}

		// moves the slime
		transform.position += Time.deltaTime * speed * moveDirection;
	}

	protected override void Animate(){
		base.Animate();

		// changes the way the slime is facing
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
