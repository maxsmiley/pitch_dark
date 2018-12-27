using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Game {
	//TODO: rename Interface?
    //TODO: move different pillars of UI into their own classes, for example UIManager_Movement?
	public class UIManager : MonoBehaviour {

		// Instantiated in Inspector
		public Game game;
		public ObjectFactory factory;

		// UI Control flow
		public enum UI_Mode {
			// Each turn there is 
			// The player selects what to do on the current units turn (can look around)
			player_unit_turn_pre_action,
			// The unit requests specific targets/directions for selected action
			player_unit_turn_during_action,
		}
		enum UI_Action {
			none,
			// Selected Character
			move,
		}
		private UI_Mode mode = UI_Mode.player_unit_turn_pre_action;
		private UI_Action action = UI_Action.none;
		private Character selectedCharacter = null; //probably should be handled by game logic?
		private Tile highlightedTile = null; //maybe should be handled by game logic?

		// Data
		public List<Informant> informants = new List<Informant> ();

		// UI Stuff
		// 1. Movement
		private List<Tile> walking_path = null;
		private UIGridPath display_walking_path = null;
		private Tile goal = null;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {

		}

		public void MouseEntered(Informant informant) {
			// Set highlights
			setHighlight (informant, true);
		}

		public void MouseExited(Informant informant) {
			// Remove highlights
			setHighlight (informant, false);
		}

		private void setMode(UI_Mode mode){
			this.mode = mode;
		}

		// TODO: await for a mouse down instead...
		public void MouseDowned(Informant informant) {
			// Move
			if (mode == UI_Mode.player_unit_turn_during_action && action == UI_Action.move) {
				if (IsSameOrSubclass(typeof(Tile), informant.GetType())) {
					Dictionary<string, object> move_payload = new Dictionary<string, object>()
					{
						{"action", ActionType.move},
						{"character", selectedCharacter},
						{"path", walking_path},
					};
					game.addAction(move_payload);
					foreach (var tile in selectedCharacter.move_range) {
						tile.setWalkableForSelectedCharacter(false);
					}
					if (display_walking_path != null) {
						display_walking_path.dispose ();
						display_walking_path = null;
					}
				}
			} else if (mode == UI_Mode.player_unit_turn_pre_action) {
				// Set selections
				setSelect (informant, true);
			}
		}

		private void updateDisplayMovePath() {
			if (selectedCharacter != null) {
				if (selectedCharacter.move_range != null && selectedCharacter.isSelected ())
				foreach (var tile in selectedCharacter.move_range) {
					//TODO: take a look at this implementation, it violates a lot of the hierarchy, 
					if (tile.isHighlighted () && tile != goal) {
						if (display_walking_path != null) {
							display_walking_path.dispose ();
						}
						walking_path = AStar.getShortestPath (game.getGrid(), selectedCharacter.current_tile, tile);
						var cropped_walking_path = walking_path;
						if (cropped_walking_path [0] != selectedCharacter.current_tile) {
							cropped_walking_path.Insert (0, selectedCharacter.current_tile);
						} else {
							cropped_walking_path.Remove (walking_path [walking_path.Count - 1]);
						}
						display_walking_path = new UIGridPath (factory, game.getGrid(), cropped_walking_path);
						break;
					}
				}
			}
		}

		private void setSelect(Informant informant, bool select){
			if (IsSameOrSubclass(typeof(GridThing), informant.GetType())) {
				GridThing gridThing = informant as GridThing;
				gridThing.setSelected(select);
				
				// If tile, that contains a character, set select for character as well
				if (IsSameOrSubclass(typeof(Tile), informant.GetType())) {
					Tile tile = informant as Tile;
					if (tile.character) {
						setSelect(tile.character, select);
					}
				}

				// If character, select tiles in movement range
				if (select && IsSameOrSubclass(typeof(Character), informant.GetType())) {
					Character character = informant as Character;

					//TODO: for now this switches to movement mode, 
					// in the future this should be trigged by a button in the UI or a keypress or something explicit 
					// not just by click a character should it prompt a move...
					if (select) {
						this.selectedCharacter = character;
					} else if (selectedCharacter == character) {
						this.selectedCharacter = null;
					}
					foreach (var tile in character.move_range) {
						tile.setWalkableForSelectedCharacter(select);
					}
					setMode (UI_Mode.player_unit_turn_during_action);
					this.action = UI_Action.move;
				}
			}
		}

		private void setHighlight(Informant informant, bool highlight){
			if (IsSameOrSubclass(typeof(GridThing), informant.GetType())) {
				GridThing gridThing = informant as GridThing;
				gridThing.setHighlighted(highlight);
				
				// If tile, that contains a character, set highlight for character as well
				if (IsSameOrSubclass(typeof(Tile), informant.GetType())) {
					Tile tile = informant as Tile;
					if (tile.character) {
						setHighlight(tile.character, highlight);
					}
				}

				// If character, highlight tiles in movement range
				if (IsSameOrSubclass(typeof(Character), informant.GetType())) {
					Character character = informant as Character;
					foreach (var tile in character.move_range) {
						tile.setWalkableForHighlightedCharacter(highlight);
					}
				}
			}

			// Display Path UI during Move Action
			if (highlight && mode == UI_Mode.player_unit_turn_during_action && action == UI_Action.move) {
				if (IsSameOrSubclass(typeof(Tile), informant.GetType())) {
					Tile tile = informant as Tile;
					if (tile != goal) {
						updateDisplayMovePath();
						goal = tile;
					}
				}
			}
		}

		public bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
		{
			return potentialDescendant.IsSubclassOf(potentialBase)
				|| potentialDescendant == potentialBase;
		}

//		public void addInformant(Informant informant){
//
//		}


	}
}