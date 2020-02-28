using NoPhysArkanoid.LevelElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Forms
{
	[RequireComponent(typeof(BoxFigure))]
	public class BallsReflector : MonoBehaviour 
	{
		public event Action<Ball, Vector3, EdgeAngle> ProcessCollision = (a, b, c) => { };

		//protected abstract void ProcessCollision(Ball ball, Vector3 hitPoint, EdgeAngle angle);
		private void ReflectBall(Ball ball, Vector3 touchPoint, EdgeAngle wallAngle)
		{
			ball.ExpectColliderHit(touchPoint, wallAngle);
			ProcessCollision.Invoke(ball, touchPoint, wallAngle);
		}

		protected BoxFigure _boxFigure;

		protected virtual void Awake()
		{
			_boxFigure = GetComponent<BoxFigure>();
		}

		protected virtual void Update()
		{
			if (Level.Instance == null)
				return;

			var balls = Level.Instance.Balls;

			foreach(var ball in balls)
			{
				Vector3 point = Vector3.zero;
				EdgeAngle angle = EdgeAngle.Zero;

				if (_boxFigure.CheckCollision(ball, out point, out angle))
					ReflectBall(ball, point, angle);
			}
		}
	} 
} 


