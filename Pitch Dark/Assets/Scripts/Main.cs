using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Game 
{
	//TODO: rename to "MAP" or something
	public class Main : MonoBehaviour {

		// Game Logic
		public ArrayList characters; //Allies and Enemies, for tracking all characters
		public ArrayList turnOrder; 
		Grid grid;
		public int gridRowCount = 5;
		public int gridColCount = 5;

		// Prefabs
		public GameObject TileObject;
		public GameObject CharacterObject; 
		public GameObject UI_Sprite; 

		// UI Stuff
		private List<GameObject> walking_path = new List<GameObject> ();

		private Dictionary<Tile, GameObject> tileObjects = new Dictionary<Tile, GameObject>();
		/* Think about how best to represent classes of characters, 
		 * should they extend this object? Or should Character be an interface, or should
		 * it all be handled by like json...
		*/

		// Use this for initialization
		void Start () {
			grid = createGrid (gridColCount, gridRowCount);
			characters = getCharacters ();
		}
		
		// Update is called once per frame
		void Update () {
			//Manage Turn Order
		}

		private Grid createGrid(int cols, int rows) {
			return new Grid (this, cols, rows);
		}

		public ArrayList getCharacters() {
			ArrayList characters = new ArrayList ();

			//Hardcoded for now
			characters.Add(createCharacter(grid, 0, 0));

			return characters;
		}

		public GameObject createCharacter(Grid grid, int x_pos, int y_pos) {
			var character = Instantiate (CharacterObject);
			character.GetComponent<Character> ().setGrid (grid);
			character.GetComponent<Character>().setGridPosition (grid, x_pos, y_pos);
			return character;
		}
		
		public Tile createTile(Grid grid, int x_pos, int y_pos) {
			GameObject tile_obj = Instantiate(TileObject);
			Tile tile = tile_obj.GetComponent<Tile> ();
			tileObjects [tile] = tile_obj;
			tile.setGridPosition (grid, x_pos, y_pos);
			tile.allignTranformPositionWithGridPosition ();
			return tile;
		}

		public void createWalkingPath (List<Tile> path){

		}

	}
}
