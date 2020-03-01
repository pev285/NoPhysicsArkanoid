using NoPhysArkanoid.Forms;
using NoPhysArkanoid.LevelElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Collisions
{
	[RequireComponent(typeof(BoxFigure))]
	public class BallsReflector : MonoBehaviour 
	{
		public event Action<Ball, Hit> ProcessCollision = (a, b) => { };

		private void ReflectBall(Ball ball, Hit hit)
		{
			ball.ExpectColliderHit(hit);
			ProcessCollision.Invoke(ball, hit);
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
				Hit hit;

				if (_boxFigure.CheckCollision(ball, out hit))
					ReflectBall(ball, hit);
			}
		}
	} 
} 


