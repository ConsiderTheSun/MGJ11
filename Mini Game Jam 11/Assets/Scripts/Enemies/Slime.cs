using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy{
	public enum SlimeColor {Pink, Blue, Purple};
	[Header("Set in Inspector")]

	public SlimeColor slimeColor;

	// Start is called before the first frame update
	protected override void Start(){
		base.Start();
		//slimeColor = SlimeColor.Blue;
		float randColorValue = Random.value;

		if(randColorValue < 0.33f){
			slimeColor = SlimeColor.Pink;
		}
		else if(randColorValue < 0.66f){
			slimeColor = SlimeColor.Blue;
		}
		else{
			slimeColor = SlimeColor.Purple;
		}
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
			animationFrame = (animationFrame+1)%2;

			int offset = 0;
			if(slimeColor == SlimeColor.Blue) offset = 1;
			else if(slimeColor == SlimeColor.Purple) offset  = 2;

			enemySprite.sprite = movementSprites[animationFrame + 2*offset];
		}

	}
}
