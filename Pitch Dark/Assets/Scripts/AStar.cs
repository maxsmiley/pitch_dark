using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Game {
	public class AStar {
		//TODO: Make this more generic by creating a class / interface that all astarable objects extend instead of using Tile
		// TIle ---> node, Grid --> Graph
		public static List<Tile> getShortestPath(Grid grid, Tile start, Tile goal) {
//		Fancy code for setting a unit for path prediction -- I could do this better... wrapper?
//			if start.unit:
//				unit = start.unit
//					else:
//					unit = None
			if (start == goal) {
				return new List<Tile> (){start, goal};
			}
			List<Tile> closedset = new List<Tile> (); // The set of nodes already evaluated.
			//TODO: Improve performance by making this a heap
			List<Tile> openset = new List<Tile> (){start}; // The set of tentative nodes to be evaluated, initially containing the start node
			// The map of navigated nodes.
			Dictionary<Tile, Tile> came_from = new Dictionary<Tile, Tile> (); 
			// Cost from start along best known path
			Dictionary<Tile, float> g_score = new Dictionary<Tile, float> ();
			g_score [start] = 0f;
			// Estimated total cost from start to goal through y
			Dictionary<Tile, float> f_score = new Dictionary<Tile, float> ();
			f_score [start] = g_score [start] + heuristic_cost_estimate (start, goal);
			while (openset.Count > 0) {
				// the node in openset having the lowest f_score[] value
				openset.Sort ( (IComparer<Tile>)new f_score_comparer(f_score));
				Tile current = openset[0];
				if (current == goal) {
					return reconstruct_path(came_from, goal);
				}
				// Move current from openset to closed set
				openset.Remove(current);
				closedset.Add(current);
				foreach (Tile neighbor in Grid.getNeighboringTiles(grid, current)) {
					if (closedset.Contains(neighbor)) {
						continue;
					}
					float tentative_g_score = g_score[current] + dist_between(current, neighbor);
					if (!openset.Contains(neighbor) || tentative_g_score < g_score[neighbor]) {
						came_from[neighbor] = current;
						g_score[neighbor] = tentative_g_score;
						f_score[neighbor] = g_score[neighbor] + heuristic_cost_estimate(neighbor, goal);
						//TODO: special case where you limit this by movement range:
						// if unit.movement_range.contains(neighbor)
						if (!openset.Contains(neighbor)){
							openset.Add(neighbor);
						}
					}
				}

			}
			return new List<Tile> ();
		}

		private static float heuristic_cost_estimate (Tile current, Tile goal) {
			//TODO: include in here slow terrain, jumps, etc
			float cost = dist_between (current, goal);
			return cost;
		}

		private static List<Tile> reconstruct_path(Dictionary<Tile, Tile> came_from, Tile current_node) {
			//TODO: should this be contains KEY or contains VALUE?
			if (came_from.ContainsKey (came_from [current_node])) {
				List<Tile> p = reconstruct_path (came_from, came_from [current_node]);
				p.Add (current_node);      
				return p;                                   
			} else {
				return new List<Tile>(){current_node};
			}
		}

		private class f_score_comparer : IComparer<Tile> {
			private Dictionary<Tile, float> f_score;
			public f_score_comparer (Dictionary<Tile, float> f_score){
				this.f_score = f_score;
			}
			int IComparer<Tile>.Compare(Tile t1, Tile t2) {
				return f_score[t1].CompareTo(f_score[t2]);
			}
		}

		public static float dist_between (Tile a, Tile b) {
			return Mathf.Sqrt ( Mathf.Pow ((a.grid_x - b.grid_x), 2) +
			                  Mathf.Pow ((a.grid_y - b.grid_y), 2));
		}

	}
}
