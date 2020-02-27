using NoPhysArkanoid.LevelElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Forms
{
	public class Brick : BallsReflector
	{
		protected override void ProcessCollision(Ball ball, Vector3 hitPoint, WallAngle angle)
		{
			throw new System.NotImplementedException();
		}
	}
} 


