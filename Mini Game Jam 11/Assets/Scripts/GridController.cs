using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour{

	[Header("Set in Inspector")]
	public Tile[] tileArray;
	public Tilemap platforms;
	public PlayerController player;

	// Start is called before the first frame update
	void Start(){

	}

	// Update is called once per frame
	void Update(){

	}


	public void LoadMap(){
		platforms.SetTile(new Vector3Int(0,0,0) , tileArray[3]);

		// loads the level data from a text file
		string lvlName = "Level_v0.1";
		TextAsset lvlData = Resources.Load(lvlName) as TextAsset;
		if(lvlData == null){
			Debug.Log("Error: unknown level");
			lvlData = Resources.Load("Level_v0.1") as TextAsset;
		}

		string[] rows = lvlData.text.Split('\n');
		int lvlH = rows.Length;
		int lvlW = rows[0].Length-1;

		// puts layout into an array
		char[,] lvlDataArray = new char[lvlW,lvlH];
		for(int i=0; i< lvlH; i++){
			char[] currRow = rows[lvlH - (i+1)].ToCharArray();
			for(int j=0; j < lvlW; j++){
				lvlDataArray[j,i] = currRow[j];
			}
		}


		// creates tiles based on lvlDataArray data		
		for(int i=0; i< lvlH; i++){
			for(int j=0; j < lvlW; j++){
				int tileID = GetTileID(lvlDataArray[j,i]);
				if(tileID == -1) {
					continue;
				}
				// places the player
				if(tileID == -2){
					player.SetPlayerPostion(GetComponentInParent<GridLayout>().CellToWorld(new Vector3Int(j,i,0)) + new Vector3(1f,1f,0f));
					continue;
				}
				PlaceTile(j,i,tileID);
			}
		}

	}

	int GetTileID(char tileCharacter){
		switch(tileCharacter){
			case '/':
				return 0;
			case '-':
				return 0;
			case '_':
				return 1;
			case 'a':
				return 3;
			case 'b':
				return 3;
			case 'c':
				return 3;
			case 'd':
				return 3;
			case 'e':
				return 3;
			case 'f':
				return 3;
			case 'h':
				return 0;
			case 'g':
				return -2;
			case '.':
			default:
				return -1;
		}
	}
	void PlaceTile(int x, int y, int tileID){
		//Debug.Log("(" + x + ", " + y + ") " + tileID);
		platforms.SetTile(new Vector3Int(x,y,0) , tileArray[tileID]);
		
	}
}
