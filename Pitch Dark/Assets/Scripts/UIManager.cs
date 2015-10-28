using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game {
	public class UIManager : MonoBehaviour {

		// Instantiated in Inspector
		public Main game;

		// Data
		public Character selectedCharacter;
		public Character highlightedCharacter;

		// UI Stuff
		private List<GameObject> walking_path = new List<GameObject> ();

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

	

		void highlightCharacter(Character character) {

		}

		// TODO: Remove all this kind of logic from GridThing(s) and move it here!
		public void MouseEntered(Informant informant) {
			//Set highlights
		}

		public void MouseExited(Informant informant) {
			//Set highlights
		}

		void OnMouseDown (){
			//Set selections
		}


	}
}