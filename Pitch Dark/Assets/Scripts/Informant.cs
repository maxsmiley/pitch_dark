using UnityEngine;
using System.Collections;

namespace Game {
	public class Informant : MonoBehaviour {
		protected UIManager ui_manager;

		public void setUIManager(UIManager manager) {
			this.ui_manager = manager;
		}
	}
}
