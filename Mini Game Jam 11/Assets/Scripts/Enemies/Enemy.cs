using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour{
	[Header("Set in Inspector")]
	
	
	public Sprite[] movementSprites;
	public int hp = 1;
	public float moveAnimTime = 1f;
	public float jumpHeight = 50f;
	public float speed = 0.01f;
	public float jumpCooldown = 0.2f;

	public float damageTime = 0.5f;

	public float knockbackLR = 100f;

	[Header("Set Dynamically")]
	public Transform playerTransform;
	protected SpriteRenderer enemySprite;

	protected Vector3 playerDirection;
	protected Vector3 moveDirection;

	protected float animationTimer = 0f;
	protected int animationFrame = 0;
	protected bool grounded = false;
	protected float jumpTimer = 0f;

	
	protected float damageCountdown = 1f;

	public enum Mode {Idle, Moving};
	protected Mode currentMode = Mode.Idle;
	

	// Start is called before the first frame update
	protected virtual void Start(){
		playerTransform = GameObject.Find("Player").transform;
		enemySprite = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	protected virtual void Update(){
		// finds the direction the player is from the enemy
		playerDirection = Vector3.Normalize(playerTransform.position - transform.position);
		if(damageCountdown < damageTime){
			damageCountdown += Time.deltaTime;
		}
		else{
			GetComponent<SpriteRenderer>().color = Color.white;
		}
		
		Move();
		Animate();
	}


	protected abstract void Move();

	protected virtual void Animate(){
		animationTimer += Time.deltaTime;
	}


	public virtual void Damage(){
		// checks for invin frames
		if(damageCountdown < damageTime){
			return;
		}

		//decrements health
		hp--;

		// changes the color to indicate damage
		damageCountdown = 0f;
		GetComponent<SpriteRenderer>().color = new Color(1f,0.2704402f,0.4190495f);

		//knockbacks the enemy
		Vector2 difference = (transform.position - playerTransform.position).normalized;
		if(difference.x < 0) {
			GetComponent<Rigidbody2D>().AddForce((transform.right * knockbackLR) * -1);
		}
		else {
			GetComponent<Rigidbody2D>().AddForce(transform.right * knockbackLR);
		}

		// checks if the enemy is out of health
		if(hp <= 0){
			Destroy(gameObject);
		}
	}
}
