using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: think about if we want UnitAction vs GameAction
namespace Game {
	public enum ActionType {
		none,
		move,
	}
    //Some actions may require a callback on completion
    public delegate void ActionCallback(Dictionary<string, object> payload);
    public class Action  {
		public ActionType action_type = ActionType.none;

        /* Payload per action:
		 *  - ActionType action
		 *  - ActionCallback callback        
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
            if (payload.ContainsKey("callback"))
            {
                ActionCallback cb = payload["callback"] as ActionCallback;
                cb(payload);
            }
            payload = null;
		}

	}
}
