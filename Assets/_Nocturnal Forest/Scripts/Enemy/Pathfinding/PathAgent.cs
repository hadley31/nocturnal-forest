using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathAgent : MonoBehaviour
{
	private Queue<PathNode> path;


	public PathNode CurrentNode
	{
		get;
		private set;
	}

	private void OnEnable()
	{
		SetCurrentToClosest();
	}

	private void SetCurrentToClosest()
	{
		PathNode closest = null;
		float dist = Mathf.Infinity;
		foreach (PathNode node in FindObjectsOfType<PathNode>())
		{
			float d = Vector2.SqrMagnitude(node.transform.position - transform.position);
			if (d < dist)
			{
				dist = d;
				closest = node;
			}
		}

		CurrentNode = closest;
	}

	private void Update()
	{
		if (CurrentNode == null)
		{
			SetCurrentToClosest();
		}
		else if (path != null && path.Count > 0)
		{
			if (Vector2.SqrMagnitude(transform.position - CurrentNode.transform.position) < 0.5f)
			{
				CurrentNode = path?.Dequeue();
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (CurrentNode == null)
		{
			return;
		}
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, CurrentNode.transform.position);

		if (path == null || !path.Any())
		{
			return;
		}

		Gizmos.DrawLine(CurrentNode.transform.position, path.ElementAt(0).transform.position);

		for (int i = 0; i < path.Count - 1; i++)
		{
			Gizmos.DrawLine(path.ElementAt(i).transform.position, path.ElementAt(i + 1).transform.position);
		}
	}


	public void SetDestination(PathNode node)
	{
		path = CalculatePath(node) ?? null;
	}


	private Queue<PathNode> CalculatePath(PathNode to)
	{
		return CurrentNode?.Search(to);
	}
}
