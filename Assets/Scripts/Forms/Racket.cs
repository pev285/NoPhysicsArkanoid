using NoPhysArkanoid.LevelElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Forms
{
	public class Racket : BallsReflector
	{
		protected override void ProcessCollision(Ball ball, Vector3 hitPoint, EdgeAngle angle)
		{
			throw new System.NotImplementedException();
		}
	}
} 


