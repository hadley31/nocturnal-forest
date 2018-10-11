using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathNode))]
public class PathNodeEditor : Editor
{
	public override void OnInspectorGUI()
	{
		PathNode node = target as PathNode;

		if (GUILayout.Button("Duplicate"))
		{
			PathNode newNode = Instantiate(node);

			newNode.ClearConnections();
			node.AddConnection(newNode);
			newNode.AddConnection(node);
			newNode.name = node.name;

			Selection.activeGameObject = newNode.gameObject;
		}

		base.OnInspectorGUI();
	}
}
