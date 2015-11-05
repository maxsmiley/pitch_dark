using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: think about if we want UnitAction vs GameAction
namespace Game {
	public enum ActionType {
		none,
		move,
	}
	public class Action  {

		public ActionType action_type = ActionType.none;

		/* Payload per action:
		 *  - ActionType action
		 * Move:
		 *  - List<Tile> path
		 *  - GridThing moveable
		 */
		public Dictionary<string, object> payload = null;

		public static float AnimationSpeed = 0.07f;
		private float animationTimer = AnimationSpeed;

		public Action(ActionType type, Dictionary<string, object> payload) {
			this.action_type = type;
			this.payload = payload;
		}

		private bool animationTick(){
			animationTimer -= Time.deltaTime;
			if (animationTimer <= 0) {
				animationTimer = AnimationSpeed;
				return true;
			}
			return false;
		}

		private void ActionComplete(){
			action_type = ActionType.none;
			payload = null;
		}

	}
}
