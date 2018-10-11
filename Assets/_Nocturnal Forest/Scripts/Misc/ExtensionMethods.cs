using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
	public static T PickRandom<T>(this List<T> list)
	{
		return list.Count > 0 ? list[Random.Range(0, list.Count)] : default(T);
	}
}
