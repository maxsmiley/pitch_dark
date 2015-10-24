using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Game 
{
	public class Grid {
		//TODO: Figure out list of Prefab types
		List<List<Tile>> tiles;
		public int cols;
		public int rows;
		public int gridTileWidth = 32;

		public Grid(Main main, int cols, int rows) {
			this.cols = cols;
			this.rows = rows;
			tiles = new List<List<Tile>> ();
			for (int i = 0; i < cols; i++) {
				List<Tile> column = new List<Tile>();
				for (int j = 0; j < rows; j++) {
					column.Add(main.createTile(this, i,j));
				}
				tiles.Add (column);
			}
		}

		public static List<Tile> getNeighboringTiles(Grid grid, Tile tile) {
			List<Tile> neighbors = new List<Tile> ();
			int x = tile.grid_x, y = tile.grid_y;
			for (int i = x-1; i < x+2; i+=2) {
				//If within bounds of grid
				if (inBounds(grid, i, y)) {
//					//TODO: Pathabilty
					neighbors.Add (grid.tiles[i][y]);
				}
			}
			for (int j = y-1; j < y+2; j+=2) {
				//If within bounds of grid
				if (inBounds(grid, x, j)) {
					//TODO: Pathabilty
					neighbors.Add (grid.tiles[x][j]);
				}
			}
			return neighbors;
		}

		public Tile getTile(int x, int y) {
			if (inBounds(this, x, y)) {
//				Debug.Log ("get tile " + x + " " + y);
//				Debug.Log ("cols " + tiles.Count);
//				Debug.Log ("rows " + tiles[0].Count);
				return tiles[x][y];
			}
			return null;
		}

		// Like mini-AStar, maybe move to AStar??
		public static List<Tile> getMovementRange(Grid grid, Character character) {
			int max_dist = character.move_speed;
			List<Tile> stack = new List<Tile> (); // Like open set
			List<Tile> move_range = new List<Tile> (){character.current_tile};
			Dictionary<Tile, int> dist = new Dictionary<Tile, int> (); // Walking distance from character
			foreach (var tile in Grid.getNeighboringTiles(grid, character.current_tile)) {
				dist[tile] = 1;
				stack.Add(tile);
			}	
			while (stack.Count > 0) {
				Tile tile = stack[0];
				stack.Remove(tile);
				if (!move_range.Contains(tile)) {
					move_range.Add(tile);
					if (dist[tile] < max_dist) {
						foreach (var neighbor in Grid.getNeighboringTiles(grid, tile)) {
							//TODO: Limit by vision
							if (!move_range.Contains(neighbor)) {
								dist[neighbor] = dist[tile] + 1;
								stack.Add(neighbor);
							}
						}
					}
				}
			}
			return move_range;
		}

		public static bool inBounds(Grid grid, int x, int y) {
			return (grid.cols > x && grid.rows > y && x >= 0 && y >= 0);
		}

	}
}