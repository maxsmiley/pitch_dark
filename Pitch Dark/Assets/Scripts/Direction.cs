using UnityEngine;
using System.Collections;
namespace Game {
	public enum Direction { 
		up,
		down,
		left,
		right,
		none,
	}
	public enum ClockDirection {
		clockwise,
		counterclockwise
	}
	public static class DirectionMethods {
		// Return direction from a to b
		// Contract: a and b must be unilaterally aligned (but not necessarly adjecent), otherwise returns none
		public static Direction getDirection( GridThing a, GridThing b) {
			if (a == null || b == null) {
				return Direction.none;
			}
			if (a.grid_x > b.grid_x) {
				return Direction.left;
			} 
			if (a.grid_x < b.grid_x) {
				return Direction.right;
			}
			if (a.grid_y > b.grid_y) {
				return Direction.down;
			}
			if (a.grid_y < b.grid_y) {
				return Direction.up;
			}
			return Direction.none;
		}

		public static Direction rotate(this Direction dir, ClockDirection cd) {
			if (cd == ClockDirection.clockwise) {
				switch (dir) {
				case Direction.down:
					return Direction.left;
				case Direction.left:
					return Direction.up;
				case Direction.up:
					return Direction.right;
				case Direction.right:
					return Direction.down;
				default:
					break;
				}
			} else if (cd == ClockDirection.counterclockwise) {
				switch (dir) {
				case Direction.down:
					return Direction.right;
				case Direction.right:
					return Direction.up;
				case Direction.up:
					return Direction.left;
				case Direction.left:
					return Direction.down;
				default:
					break;
				}
			}
			return Direction.none;
		}
	}
}
