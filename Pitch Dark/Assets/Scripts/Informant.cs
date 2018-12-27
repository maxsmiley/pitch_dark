using UnityEngine;
using System.Collections;

// Listener that informs ui_manager of mouse events 
namespace Game {
	public class Informant : MonoBehaviour {
		protected UIManager ui_manager;

		public void setUIManager(UIManager manager) {
			this.ui_manager = manager;
		}

		void OnMouseEnter() { 
			ui_manager.MouseEntered (this);
		} 
		void OnMouseExit() { 
			ui_manager.MouseExited (this);
		} 
		void OnMouseDown(){
			ui_manager.MouseDowned (this);
		}
	}
}
