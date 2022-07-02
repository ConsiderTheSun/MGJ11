using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour{
	[Header("Set in Inspector")]
	public Transform playerTransform;
	
	public Sprite[] movementSprites;
	public float moveAnimTime = 1f;
	public float jumpHeight = 50f;
	public float speed = 0.01f;
	public float jumpCooldown = 0.2f;

	[Header("Set Dynamically")]
	protected SpriteRenderer enemySprite;

	protected Vector3 playerDirection;
	protected Vector3 moveDirection;

	protected float animationTimer = 0f;
	protected int animationFrame = 0;
	protected bool grounded = false;
	protected float jumpTimer = 0f;

	public enum Mode {Idle, Moving};
	protected Mode currentMode = Mode.Idle;

	// Start is called before the first frame update
	protected virtual void Start(){
		enemySprite = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	protected virtual void Update(){
		// finds the direction the player is from the enemy
		playerDirection = Vector3.Normalize(playerTransform.position - transform.position);


		Move();
		Animate();
	}


	protected abstract void Move();

	protected virtual void Animate(){
		animationTimer += Time.deltaTime;
	}
}
