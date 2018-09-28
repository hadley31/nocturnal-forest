using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Entity))]
public class Enemy : MonoBehaviour
{
	public void OnCollisionEnter2D (Collision2D collision)
	{
		Character c = collision.collider.GetComponent<Character> ();

		print ("boom");

		if ( c != null )
		{
			print ("player");
			Vector2 offset = collision.rigidbody.position - collision.otherRigidbody.position;
			print (offset);
			if ( Vector2.Dot (offset, Vector2.up) > 0.85f )
			{
				print ("goomba!");
				GetComponent<Health> ().SetValue (0);
			}
		}
	}
}
