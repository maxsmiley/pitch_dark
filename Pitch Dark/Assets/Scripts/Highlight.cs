using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game {
	public class Highlight {

		UIManager manager; // Or UIAgent?

		public enum HighlightType { 
			highlighted, 
			selected
		}

		private Dictionary<HighlightType, bool> highlightStates = new Dictionary<HighlightType, bool>() 
		{
			{HighlightType.highlighted, false},
			{HighlightType.selected, false}
		};
		
		protected virtual void highlightChanged(){
			//Inform UIAgent of change
			//UIAgent then informs manager of change?
		}

		public void setHighlighted(bool highlight) {
			setHighlightState (HighlightType.highlighted, highlight);
		}

		public void setSelected(bool highlight) {
			setHighlightState (HighlightType.selected, highlight);
		}

		private void setHighlightState(HighlightType type, bool state){
			if (highlightStates == null) { //TODO: Solve why the fuck I need to do this... unity u little bitch
				highlightStates = new Dictionary<HighlightType, bool> () 
				{
					{HighlightType.highlighted, false},
					{HighlightType.selected, false}
				};
			}
			if (this.highlightStates [type] != state) {
				this.highlightStates [type] = state;
				highlightChanged ();
			}
		}

		public bool isHighlighted() {
			return getHighlightState (HighlightType.highlighted);
		}

		public bool isSelected() {
			return getHighlightState (HighlightType.selected);
		}

		private bool getHighlightState(HighlightType type) {
			if (highlightStates == null) { //TODO: Solve why the fuck I need to do this... unity u little bitch
				highlightStates = new Dictionary<HighlightType, bool> () 
				{
					{HighlightType.highlighted, false},
					{HighlightType.selected, false}
				};
			}
			return highlightStates[type];
		}
	}
}
