using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Game 
{
	//TODO: create a Game and seperate UIManager and Game and pass eachother each
	public class Main : MonoBehaviour {

		// Game Logic
		public ArrayList characters; //Allies and Enemies, for tracking all characters
		public ArrayList turnOrder; 
		Grid grid;
		public int gridRowCount = 5;
		public int gridColCount = 5;

		// Instantiated in Inspector
		public ObjectFactory objectFactory; 
		public UIManager ui_manager;


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
			return new Grid (objectFactory, cols, rows);
		}

		public ArrayList getCharacters() {
			ArrayList characters = new ArrayList ();

			//Hardcoded for now
			characters.Add(objectFactory.createCharacter(grid, 0, 0));

			return characters;
		}

		public void createWalkingPath (List<Tile> path){

		}

	}
}
