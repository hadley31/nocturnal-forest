using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPush : EntityBase
{
	public Vector2 push;

    public void Push(Entity entity)
    {
        Rigidbody2D rb = entity.GetComponent<Rigidbody2D>();
		rb.AddForce(push - rb.velocity);
    }
}
