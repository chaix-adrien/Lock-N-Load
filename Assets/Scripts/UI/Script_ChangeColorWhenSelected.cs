using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class Script_ChangeColorWhenSelected : MonoBehaviour, IDeselectHandler, ISelectHandler {
	public Color onSelect = Color.white;
	public Image toChange;
	private Color oldColor;
	void Start () {
		oldColor = toChange.color;
	}
	
	public void OnSelect(BaseEventData eventData)
     {
		toChange.color = onSelect;
     }
	 public void OnDeselect(BaseEventData eventData)
     {
		 toChange.color = oldColor;
     }
}
