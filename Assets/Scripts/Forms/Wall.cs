using NoPhysArkanoid.LevelElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Forms
{
	public class Wall : BallsReflector
	{
		protected override void ProcessCollision(Ball ball, Vector3 hitPoint, EdgeAngle angle)
		{
			ball.ExpectColliderHit(hitPoint, angle);
		}
	}
} 


