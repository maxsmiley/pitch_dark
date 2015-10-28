using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game {
public class UIGridPathSegment : GridThing {

		public Sprite straight; // --
		public Sprite bend; // _|
		public Sprite terminal; // -•

		public enum Type {
			straight,
			bend,
			terminal,
			none
		}

		public Type type;

		// Direction refers to the direction to the following segment.
		public Direction direction;

		public void setup(Type type, Direction dir_to, Direction dir_from){
			this.type = type;
			this.direction = dir_to;
			switch (type) {
			case Type.straight:
				GetComponent<SpriteRenderer>().sprite = straight;
				if (dir_to == Direction.up || dir_to == Direction.down) {
					rotate (-90);
				}
				break;
			case Type.bend:
				GetComponent<SpriteRenderer>().sprite = bend;
				if (dir_to == Direction.up && dir_from == Direction.right || 
				    dir_to == Direction.right && dir_from == Direction.up) {
					rotate (90);
				} else if (dir_to == Direction.down && dir_from == Direction.right || 
				           dir_to == Direction.right && dir_from == Direction.down) {
					rotate (180);
				} else if (dir_to == Direction.down && dir_from == Direction.left || 
				           dir_to == Direction.left && dir_from == Direction.down) {
					rotate (270);
				} 
				break;
			case Type.terminal:
				GetComponent<SpriteRenderer>().sprite = terminal;
				Direction dir = dir_to != Direction.none ? dir_to : dir_from;
				if (dir == Direction.up) {
					rotate (90);
				} else if (dir == Direction.right) {
					rotate (180);
				} else if (dir == Direction.down) {
					rotate (270);
				}
				break;
			default:
				Debug.LogError("UIGridSegment not given direction");
				break;
			}
		}

		private void rotate(int degrees){
			transform.Rotate (Vector3.forward * -degrees);
		}
		
		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}
