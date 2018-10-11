using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathNode : MonoBehaviour
{
	[SerializeField] private List<PathNode> connections;

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;

		connections.RemoveAll(x => x == null);

		foreach (var node in connections)
		{
			float offset = 0.1f;
			if (transform.position.x > node.transform.position.x)
				offset *= -1;

			Gizmos.DrawLine(transform.position + Vector3.up * offset, node.transform.position + Vector3.up * offset);
		}
	}

	public void ClearConnections()
	{
		connections?.Clear();
	}

	public void AddConnection(PathNode node)
	{
		connections?.Add(node);
	}

	public void RemoveConnection(PathNode node)
	{
		connections?.Remove(node);
	}

	public bool ConnectedTo(PathNode other)
	{
		return connections?.Contains(other) ?? false;
	}

	public float DistanceTo(PathNode other)
	{
		return Vector2.Distance(transform.position, other.transform.position);
	}

	public List<PathNode> GetConnections()
	{
		return connections;
	}

	public Queue<PathNode> Search(PathNode target)
	{
		if (target == null) { return null; }

		List<PathNode> pathNodes = new List<PathNode>();
		Search(this, target, pathNodes);

		Queue<PathNode> queue = new Queue<PathNode>();
		for (int i = pathNodes.Count - 1; i >= 0; i--)
		{
			queue.Enqueue(pathNodes[i]);
		}

		return queue;
	}

	private bool Search(PathNode from, PathNode target, List<PathNode> list)
	{
		if (this.ConnectedTo(target))
		{
			return true;
		}
		else
		{
			foreach (PathNode node in GetConnectionsExcept(from))
			{
				if (node.Search(this, target, list))
				{
					list.Add(node);
					return true;
				}
			}
		}

		return false;
	}

	private List<PathNode> GetConnectionsExcept(params PathNode[] exceptions)
	{
		return connections.FindAll(x => !exceptions.ToList().Contains(x));
	}
}
