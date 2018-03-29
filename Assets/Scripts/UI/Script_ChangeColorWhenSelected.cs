using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class Script_ChangeColorWhenSelected : MonoBehaviour, IDeselectHandler, ISelectHandler,  IPointerEnterHandler, IPointerExitHandler {
	public Color onSelect = Color.white;
	public Image toChange;
	private Color oldColor;
	private bool selected = false;
	void Start () {
		oldColor = toChange.color;
	}
	
	public void OnSelect(BaseEventData eventData) {
		toChange.color = onSelect;
		selected = true;
	}
	

	 public void OnPointerEnter(PointerEventData eventData) {
		toChange.color = onSelect;
     }

 	public void OnDeselect(BaseEventData eventData) {
		selected = false;
		toChange.color = oldColor;
	}

	public void OnPointerExit(PointerEventData eventData) {
		Script_ChangeColorWhenSelected script = null;
		if (EventSystem.current.currentSelectedGameObject)
			script = EventSystem.current.currentSelectedGameObject.GetComponent<Script_ChangeColorWhenSelected>();
		if (!selected)
			if (!script || script.toChange != toChange)
				toChange.color = oldColor;	
	}
}