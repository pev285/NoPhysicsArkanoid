using NoPhysArkanoid.LevelElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Forms
{
	[RequireComponent(typeof(BoxFigure))]
	public abstract class BallsReflector : MonoBehaviour 
	{
		protected abstract void ProcessCollision(Ball ball, Vector3 hitPoint, EdgeAngle angle);


		protected BoxFigure _boxFigure;

		private void Awake()
		{
			_boxFigure = GetComponent<BoxFigure>();
		}

		private void Update()
		{
			if (Level.Instance == null)
				return;

			//_intersections.Clear();
			var balls = Level.Instance.Balls;

			foreach(var ball in balls)
			{
				Vector3 point = Vector3.zero;
				EdgeAngle angle = EdgeAngle.Zero;


				if (_boxFigure.CheckCollision(ball, out point, out angle))
					ProcessCollision(ball, point, angle);

				/*
				bool found = false;

				var ballPosition = ball.Position;
				var ballNextPosition = ball.NextPosition;

				ballPosition.z = 0;
				ballNextPosition.z = 0;

				//TryFindBestIntersection(ref found, ref bestPoint, ref bestAngle, ballPosition, ballNextPosition, _tl, _bl, WallAngle.HalfPi);
				//TryFindBestIntersection(ref found, ref bestPoint, ref bestAngle, ballPosition, ballNextPosition, _tr, _br, WallAngle.HalfPi);
				//TryFindBestIntersection(ref found, ref bestPoint, ref bestAngle, ballPosition, ballNextPosition, _tl, _tr, WallAngle.Zero);
				//TryFindBestIntersection(ref found, ref bestPoint, ref bestAngle, ballPosition, ballNextPosition, _bl, _br, WallAngle.Zero);

				if (found == false)
					return;

				*/

			}

		}

		//private void TryFindBestIntersection(ref bool found, ref Vector3 bestPoint, ref WallAngle bestAngle, 
		//	Vector3 ballPosition, Vector3 ballNextPosition, Vector3 cornerA, Vector3 cornerB, WallAngle candidateAngle)
		//{
		//	Vector3 point;
		//	if (HaveIntersection(ballPosition, ballNextPosition, cornerA, cornerB, out point))
		//	{
		//		if (found)
		//		{
		//			var oldDistance = Vector3.Distance(ballPosition, bestPoint);
		//			var newDistance = Vector3.Distance(ballPosition, point);

		//			if (newDistance < oldDistance)
		//			{
		//				bestPoint = point;
		//				bestAngle = candidateAngle;
		//			}
		//		}
		//		else
		//		{
		//			found = true;
		//			bestPoint = point;
		//			bestAngle = candidateAngle;
		//		}
		//	}
		//}

		//private bool HaveIntersection(Vector3 ballPosition, Vector3 ballNextPosition, Vector3 tl, Vector3 bl, out Vector3 point)
		//{
		//	throw new NotImplementedException();
		//}

	} 
} 


