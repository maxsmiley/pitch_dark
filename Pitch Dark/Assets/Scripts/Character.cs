using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game {
	public class Character : GridThing {

		// Control
		public Action action;

		// Stats
		public int move_speed = 5;
		public List<Tile> move_range = null;

		// Grid Organization
		private Grid grid;
		public Tile current_tile = null;

		// UI stuff
		private float color_cycle = .8f;
		private bool color_cycle_up = false;
		// Use this for initialization
		void Start () {
			GetComponent<ParticleSystem>().enableEmission = isSelected();
		}
		
		// Update is called once per frame
		void Update () {
			updateColor ();
		}

		public void setGrid(Grid grid) {
			this.grid = grid;
		}

		public override void setTile(Tile tile){
			if (this.current_tile != null) {
				this.current_tile.character = null;
			}
			this.current_tile = tile;
			this.current_tile.character = this;
			
			this.move_range = Grid.getMovementRange(grid, this);
		}

		protected override void highlightChanged ()
		{
			// TODO: Move this logic to UIManager?
			// TODO: decide if should call -> base.highlightChanged ();
			GetComponent<ParticleSystem>().enableEmission = isSelected();
			updateColor ();
		}

		protected override void updateColor() {
			if (isSelected ()) {
				color_cycle += color_cycle_up ? .007f : -.004f;
				color_cycle_up =  color_cycle_up ? color_cycle <= 1.1f : !(color_cycle >= .82f);
				GetComponent<SpriteRenderer> ().color = new Color (1.7f-color_cycle, 1.7f-color_cycle, color_cycle,1f);
			} else if (isHighlighted()) {
				color_cycle += color_cycle_up ? .007f : -.004f;
				color_cycle_up =  color_cycle_up ? color_cycle <= 1.1f : !(color_cycle >= .82f);
				GetComponent<SpriteRenderer> ().color = new Color (1.7f-color_cycle, 1.7f-color_cycle, color_cycle,1f);
			} else {
				GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f,1f);
			}
		}
	}
}
