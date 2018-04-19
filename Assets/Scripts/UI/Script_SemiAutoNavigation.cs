using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Script_SemiAutoNavigation : Button
{
	public Selectable upSelectable;
	public Selectable downSelectable;
	public Selectable leftSelectable;
	public Selectable rightSelectable;

	public override Selectable FindSelectableOnUp()
	{
		return upSelectable != null ? upSelectable : base.FindSelectableOnUp();
	}

	public override Selectable FindSelectableOnDown()
	{
		return downSelectable != null ? downSelectable : base.FindSelectableOnDown();
	}

	public override Selectable FindSelectableOnLeft()
	{
		return leftSelectable != null ? leftSelectable : base.FindSelectableOnLeft();
	}

	public override Selectable FindSelectableOnRight()
	{
		return rightSelectable != null ? rightSelectable : base.FindSelectableOnRight();
	}
}
#if UNITY_EDITOR
 [CustomEditor(typeof(Script_SemiAutoNavigation))]
 public class Script_CustomInpector : Editor
 {
      public override void OnInspectorGUI()
      {
           base.OnInspectorGUI();
      }
 }
 #endif

 // check back button on game options
 // select default button when navigate