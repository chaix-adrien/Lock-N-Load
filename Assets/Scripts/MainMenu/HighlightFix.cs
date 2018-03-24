using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
 
 
 [RequireComponent(typeof(Selectable))]
 public class HighlightFix : MonoBehaviour, IPointerEnterHandler, IDeselectHandler
 {
     public void OnPointerEnter(PointerEventData eventData)
     {
        if (!EventSystem.current.alreadySelecting) {
			EventSystem.current.SetSelectedGameObject(this.gameObject);
			if (GetComponentInParent<Script_NavigatorMenuNavigator>())
				GetComponentInParent<Script_NavigatorMenuNavigator>().selectObject(GetComponentInParent<Script_ButtonMenuNavigation>());
			GetComponentInParent<Script_ButtonMenuNavigation>().selectObject(GetComponent<Button>());

		}
             
			
     }
 
     public void OnDeselect(BaseEventData eventData)
     {
         this.GetComponent<Selectable>().OnPointerExit(null);
     }
 }