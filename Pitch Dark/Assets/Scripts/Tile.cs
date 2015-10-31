using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Game
{
	public class Tile : GridThing {

		public Character character; //Make unit???

		//For Rendering Purposes
		// TODO: decide is should keep record per character for efficiency
		// TODO: Move this into Highlight/Pathing??
		private bool walkableForHighlightedCharacter = false;

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {

		}

		protected override void updateColor(){
			if (isHighlighted()) {
				GetComponent<SpriteRenderer> ().color = new Color (.8f, .8f, 1f, 1f);
			} else if (isWalkableForSelectedCharacter()) {
				GetComponent<SpriteRenderer> ().color = new Color (.8f, 1f, .8f, 1f);
			} else if (isWalkableForHighlightedCharacter()) {
				GetComponent<SpriteRenderer> ().color = new Color (.9f, 1f, .9f, 1f);
			} else {
				GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f,1f);
			}
		}

		protected override void highlightChanged ()
		{
			// TODO: move this logic to UIManager?
			// TODO: decide if should call -> base.highlightChanged ();
			updateColor ();
			if (this.character) {
				if (this.character.isSelected () != isSelected ()) {
					this.character.setSelected (isSelected ());
				}
			}
		}

		public override void setTile(Tile tile){
			// I AM a tile so stfu
		}

		public Tile(Grid grid, int x_pos, int y_pos) {
			setGridPosition (grid, x_pos, y_pos);
		}

	}
}
