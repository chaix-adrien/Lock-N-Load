using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using UnityEditor;
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

 [CustomEditor(typeof(Script_SemiAutoNavigation))]
 public class Script_CustomInpector : Editor
 {
      public override void OnInspectorGUI()
      {
           base.OnInspectorGUI();
      }
 }

 // check back button on game options
 // select default button when navigate