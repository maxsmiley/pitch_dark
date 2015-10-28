using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game {
	public class UIGridPath {

		public List<UIGridPathSegment> segments = new List<UIGridPathSegment>();

		public UIGridPath(ObjectFactory factory, Grid grid, List<Tile> path){
			segments.Clear (); //TODO: ensure factory destroying of gameobjects
			// Assumptions:
			//  - Paths are at least 2 segments long
			//  - Segments are unilaterally adjacent
			foreach (var tile in path) {
				int index = path.IndexOf(tile);
				var segment = factory.createUIGridPathSegment(grid, tile.grid_x, tile.grid_y);
				UIGridPathSegment.Type type = UIGridPathSegment.Type.none;
				Direction dir_from = Direction.none;
				Direction dir_to = Direction.none;

				if (index != 0 ) {
					dir_from = DirectionMethods.getDirection(tile, path[index - 1]);
				} 
				if (index != path.Count - 1){
					dir_to = DirectionMethods.getDirection(tile, path[index + 1]);
				}

			    if (dir_to == Direction.none && dir_from == Direction.none) {
					type = UIGridPathSegment.Type.singular;
				} else if (index == 0 || index == path.Count - 1) {
					type = UIGridPathSegment.Type.terminal;
				} else if (dir_to == dir_from.rotate(ClockDirection.clockwise) 
					    || dir_to == dir_from.rotate(ClockDirection.counterclockwise)) {
					type = UIGridPathSegment.Type.bend;
				}  else {
					type = UIGridPathSegment.Type.straight;
				}
				segment.setup(type, dir_to, dir_from);
				segments.Add(segment);
			}
		}

		public void dispose(){
			foreach (var segment in segments) {
				segment.dispose();
			}
		}
	}
}

