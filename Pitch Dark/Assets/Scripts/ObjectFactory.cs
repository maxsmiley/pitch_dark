using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game {
	public class ObjectFactory : MonoBehaviour {

		// Communication Controllers passed to contrusted objects
		public UIManager ui_manager;

		// Prefabs
		public GameObject TileObject;
		public GameObject CharacterObject; 
		public GameObject UIGridPathSegment; 

		// Records
		private Dictionary<Tile, GameObject> tileObjects = new Dictionary<Tile, GameObject>();
		private Dictionary<UIGridPathSegment, GameObject> UIObjects = new Dictionary<UIGridPathSegment, GameObject>();

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public GameObject createCharacter(Grid grid, int x_pos, int y_pos) {
			var character = Instantiate (CharacterObject);
			character.GetComponent<Character> ().setGrid (grid);
			character.GetComponent<Character>().setGridPosition (grid, x_pos, y_pos);
			//setUpInformant (character); // for now the tile the char is in is handling all UI decisions
			return character;
		}
		
		public Tile createTile(Grid grid, int x_pos, int y_pos) {
			GameObject tile_obj = Instantiate(TileObject);
			Tile tile = tile_obj.GetComponent<Tile> ();
			tileObjects [tile] = tile_obj;
			tile.setGridPosition (grid, x_pos, y_pos);
			setUpInformant (tile);
			return tile;
		}

		// Move to UI Factory?
		public UIGridPathSegment createUIGridPathSegment(Grid grid, int x_pos, int y_pos) {
			GameObject segment_obj = Instantiate(UIGridPathSegment);
			UIGridPathSegment segment = segment_obj.GetComponent<UIGridPathSegment> ();
			UIObjects [segment] = segment_obj;
			segment.setGridPosition (grid, x_pos, y_pos);
			return segment;
		}

		// Connects object to a messagehub to inform listeners
		private void setUpInformant(Informant Informant){
			Informant.setUIManager (ui_manager);
		}


	}
}
