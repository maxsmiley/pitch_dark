using UnityEngine;
using System.Collections.Generic;
namespace Game {

	//TODO: Highlight should be under a new parent class ---> UIElement
	public class GridThing : Informant { // TODO: Turn into abstract - rename GridOriented or something then make GridPlacable its own thing as well 
		public int grid_x;
		public int grid_y;
		public Vector3 display_offset;

		public enum Pathability { //TODO: Move to interface Pathable
			//TODO: make this own class probably
			walkable,
			unwalkable
		}
		public Pathability pathing = Pathability.walkable;

		// UI Stuff
		public enum HighlightType { 
			//TODO: make this own class probably
			highlighted, 
			selected,
			walkableForSelectedCharacter,
			walkableForHighlightedCharacter,
		}

		private Dictionary<HighlightType, bool> highlightStates = new Dictionary<HighlightType, bool>() 
		{
			{HighlightType.highlighted, false},
			{HighlightType.selected, false},
			{HighlightType.walkableForHighlightedCharacter, false},
			{HighlightType.walkableForSelectedCharacter, false}
		};

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
		}

		public virtual void dispose () {
			Destroy (gameObject);
		}



		protected virtual void updateColor() {
			if (isHighlighted()) {
				GetComponent<SpriteRenderer> ().color = new Color (.8f, .8f, 1f, 1f);
			} else {
				GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f,1f);
			}
		}

		public void setGridPosition(Grid grid, int x, int y) {
			//TODO: Check if placement is legal
			this.grid_x = x;
			this.grid_y = y;
			if( typeof(Tile) != this.GetType()) {
				setTile(grid.getTile(x,y));
			}
			allignTranformPositionWithGridPosition ();
		}

		// Inform a tile that this objects grid position has been set to this tile's
		public virtual void setTile(Tile tile) {
			// All extending objects should overwrite this
		}

	
		// Essentially a change listener for highlights to be overriden by extending classes
		protected virtual void highlightChanged() {

		}

		//TODO: decide if objects should allign themselves or if this should be handled by the grid Object
		public void allignTranformPositionWithGridPosition(){
			var transform = GetComponent<Transform> ();
			if (transform != null) {
				transform.position = new Vector3 ((float)grid_x , (float)grid_y , 0f) + display_offset;
			}
			
		}

		public void setHighlighted(bool highlight) {
			setHighlightState (HighlightType.highlighted, highlight);
		}
		public void setSelected(bool highlight) {
			setHighlightState (HighlightType.selected, highlight);
		}
		public void setWalkableForHighlightedCharacter(bool highlight) {
			setHighlightState (HighlightType.walkableForHighlightedCharacter, highlight);
		}
		public void setWalkableForSelectedCharacter(bool highlight) {
			setHighlightState (HighlightType.walkableForSelectedCharacter, highlight);
		}
		private void setHighlightState(HighlightType type, bool state){
			if (highlightStates == null) { //TODO: Solve why the fuck I need to do this... unity u little bitch
				highlightStates = createHighlightStateTable();
			}
			if (this.highlightStates [type] != state) {
				this.highlightStates [type] = state;
				highlightChanged ();
			}
		}
		public bool isHighlighted() {
			return getHighlightState (HighlightType.highlighted);
		}
		public bool isWalkableForSelectedCharacter() {
			return getHighlightState (HighlightType.walkableForSelectedCharacter);
		}
		public bool isWalkableForHighlightedCharacter() {
			return getHighlightState (HighlightType.walkableForHighlightedCharacter);
		}
		public bool isSelected() {
			return getHighlightState (HighlightType.selected);
		}
		private bool getHighlightState(HighlightType type) {
			if (highlightStates == null) { //TODO: Solve why the fuck I need to do this... unity u little bitch
					highlightStates =createHighlightStateTable();
			}
			return highlightStates[type];
		}
		private Dictionary<HighlightType, bool> createHighlightStateTable(){
			return  new Dictionary<HighlightType, bool> () 
			{
				{HighlightType.highlighted, false},
				{HighlightType.selected, false},
				{HighlightType.walkableForHighlightedCharacter, false},
				{HighlightType.walkableForSelectedCharacter, false}
			};
		}

		public Vector3 getRealPosition(){
			Vector3 pos = gameObject.transform.position;
			return pos - display_offset;
		}

		public void setRealPosition(Vector3 pos) {
			gameObject.transform.position = pos + display_offset;
		}

		public void deltaRealPosition(Vector3 delta) {
			gameObject.transform.position += delta;

		}

	}
}
