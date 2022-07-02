using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour{
	[Header("Set in Inspector")]
	public SpriteRenderer[] hpSlots;

	public Sprite[] healthSprites;

	// Start is called before the first frame update
	void Start(){

	}

	// Update is called once per frame
	void Update(){

	}

	public void SetHealth(int hp){

		// updates the sprites on the health slots
		for(int i = 0; i < hpSlots.Length; i++){
			if(i < hp){
				hpSlots[i].sprite = healthSprites[0];
			}
			else{
				hpSlots[i].sprite = healthSprites[1];
			}
		}
	}
}
