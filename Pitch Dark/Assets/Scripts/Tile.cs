using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Game
{
	public class Tile : GridThing {

		public Character character; //Make unit???

		//For Rendering Purposes
		// TODO: decide is should keep record per character for efficiency
		private bool walkableForSelectedCharacter = false;

		// Use this for initialization
		void Start () {
			walkableForSelectedCharacter = false;
		}
		
		// Update is called once per frame
		void Update () {

		}

		protected override void updateColor(){
			if (isHighlighted()) {
				GetComponent<SpriteRenderer> ().color = new Color (.8f, .8f, 1f, 1f);
			} else if (walkableForSelectedCharacter) {
				GetComponent<SpriteRenderer> ().color = new Color (.8f, 1f, .8f, 1f);
			} else {
				GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f,1f);
			}
		}

		protected override void highlightChanged ()
		{
			// TODO: decide if should call -> base.highlightChanged ();
			updateColor ();
			if (this.character && this.character.isHighlighted() != isHighlighted()) {
				this.character.setHighlighted (isHighlighted());
			}
		}

		public override void setTile(Tile tile){
			// I AM a tile so stfu
			Debug.Log ("ignoring");
		}

		public Tile(Grid grid, int x_pos, int y_pos) {
			setGridPosition (grid, x_pos, y_pos);
		}

		public bool getWalkableForSelectedCharacter () {
			return walkableForSelectedCharacter;
		}
		public void setWalkableForSelectedCharacter(bool walkable){
			walkableForSelectedCharacter = walkable;
			highlightChanged ();
		}

	}
}
