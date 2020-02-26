using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPArkanoid.Colliders
{
	public class Wall : AbstractCollider
	{
		protected override void ProcessCollision(Vector3 hitPoint, WallAngle angle)
		{
			_ball.ExpectColliderHit(hitPoint, angle);
		}
	}
} 


