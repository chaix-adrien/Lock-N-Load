using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Script_ButtonMenu_Button : MonoBehaviour {
	public float scale = 1.5f;
	private Script_ButtonMenu menu;

	public UnityEvent methods;

	void Start() {
		menu = GetComponentInParent<Script_ButtonMenu>();

	}
	
	void OnMouseEnter() {
		menu.selectObject(gameObject);
	}

	void OnMouseExit() {
		menu.unselectObject(gameObject);
	}

	void OnMouseDown() {
		OnActivate();
	}

	void OnSelect()
	{
		transform.localScale = new Vector3(scale, scale, scale);
	}

	void OnUnselect() {
		transform.localScale = new Vector3(1, 1 ,1);
	}

	public virtual void OnActivate() {
		methods.Invoke();
	}
}
