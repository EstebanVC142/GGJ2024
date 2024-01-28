using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Olfateo))]
public class OlfateadorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Olfatear"))
		{
			Olfateo o = (Olfateo)target;
			o.Olfatear();
		}
	}
}
