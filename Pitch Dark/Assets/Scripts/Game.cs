using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Game 
{
	//TODO: create a Game and seperate UIManager and Game and pass eachother each
	public class Game : MonoBehaviour {

		// Game Logic
		public ArrayList characters; // Allies and Enemies, for tracking all characters
		public ArrayList turnOrder; 
		private Grid grid;
		public int gridRowCount = 5;
		public int gridColCount = 5;
		public List<Action> actions = new List<Action>();
		public float ActionSpeed = 0.05f; // The speed at which actions update

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
			handleActions ();
			//Manage Turn Order
		}

		// The only way for external classes to update the game state
		// Some actions are handled and executed on the spot, others might take time and are added to the actions list for processesing
		public void addAction(Dictionary<string, object> payload) {
			switch ((ActionType)payload["action"]) {
			case ActionType.move:
				Character character = payload["character"] as Character;
				payload["moveable"] = character;
				//payload["speed"] = character.move_speed;
				actions.Add (new Action(ActionType.move, payload));
				break;
			case ActionType.none:
			default:
				break;
			}
		}

		private void handleActions() {
			List<Action> completed = new List<Action> ();
			foreach (var action in actions) {
				switch (action.action_type) {
				case ActionType.move:
					handleMoveAction (action);
					break;
				case ActionType.none:
					completed.Add(action);
					break;
				default:
					break;
				}
			}
			foreach (var action in completed) {
				actions.Remove(action);
			}
			
		}

		private void handleMoveAction(Action action){
			List<Tile> path = action.payload ["path"] as List<Tile>;
			if (path.Count > 0) {
				Tile goal = path [0];
				GridThing moveable =  action.payload ["moveable"] as GridThing;
				Vector3 moveable_pos = moveable.getRealPosition();
				if (Mathf.Approximately(goal.grid_x,moveable_pos.x) && Mathf.Approximately(goal.grid_y, moveable_pos.y)) {
					path.Remove(goal);
					// TODO: set tile
					moveable.setGridPosition(grid, goal.grid_x, goal.grid_y);
				} else {
					moveable.setRealPosition( Vector3.MoveTowards(moveable_pos, new Vector3(goal.grid_x, goal.grid_y, 0f),ActionSpeed));
				}
			} else {
				ActionComplete(action);
			}
		}

		
		private void ActionComplete(Action action){
			action.action_type = ActionType.none;
		}

		private Grid createGrid(int cols, int rows) {
			return new Grid (objectFactory, cols, rows);
		}

		private ArrayList getCharacters() {
			ArrayList characters = new ArrayList ();

			//Hardcoded for now
			characters.Add(objectFactory.createCharacter(grid, 0, 0));

			return characters;
		}

		public Grid getGrid(){
			return grid;
		}


	}
}
