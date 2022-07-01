using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{
	[Header("Set in Inspector")]
	public PlayerController player;
	public GridController grid;

	// Start is called before the first frame update
	void Start(){
		grid.LoadMap();
	}

	// Update is called once per frame
	void Update(){
		
	}

	void FixedUpdate(){
		player.FixedUpdatePlayer();

	}
}
