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

			newNode.transform.SetParent (node.transform.parent);

			Selection.activeGameObject = newNode.gameObject;
		}

		if (GUILayout.Button("Create New"))
		{
			PathNode newNode = Instantiate(node);

			newNode.ClearConnections();
			newNode.name = node.name;

			newNode.transform.SetParent (node.transform.parent);

			Selection.activeGameObject = newNode.gameObject;
		}

		base.OnInspectorGUI();
	}
}
