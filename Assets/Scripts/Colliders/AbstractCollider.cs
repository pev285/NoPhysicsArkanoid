using NPArkanoid.LevelElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPArkanoid.Colliders
{
	public abstract class AbstractCollider : MonoBehaviour 
	{
		protected abstract void ProcessCollision(Vector3 hitPoint, WallAngle angle);


		protected Ball _ball;

		protected float _width;
		protected float _height;

		protected Vector3 _position;
		protected Transform _transform;

		protected Vector3 _tl;
		protected Vector3 _bl;
		protected Vector3 _tr;
		protected Vector3 _br;

		//protected List<Vector3> _intersections = new List<Vector3>();

		protected virtual void Awake()
		{
			_transform = transform;
		}
	
		protected virtual void Start()
		{
			_ball = Level.Instance.Ball;

			_position = _transform.position;

			_width = _transform.localScale.x;
			_height = _transform.localScale.z;

			_tl = new Vector3(_position.x - 0.5f * _width, 0, _position.z + 0.5f * _height);
			_tr = new Vector3(_position.x + 0.5f * _width, 0, _position.z + 0.5f * _height);
			_bl = new Vector3(_position.x - 0.5f * _width, 0, _position.z - 0.5f * _height);
			_br = new Vector3(_position.x + 0.5f * _width, 0, _position.z - 0.5f * _height);
		}

		private void Update()
		{
			//_intersections.Clear();

			bool found = false;
			Vector3 bestPoint = Vector3.zero;
			WallAngle bestAngle = WallAngle.Zero;

			var ballPosition = _ball.Position;
			var ballNextPosition = _ball.NextPosition;

			ballPosition.y = 0;
			ballNextPosition.y = 0;

			TryFindBestIntersection(ref found, ref bestPoint, ref bestAngle, ballPosition, ballNextPosition, _tl, _bl, WallAngle.HalfPi);
			TryFindBestIntersection(ref found, ref bestPoint, ref bestAngle, ballPosition, ballNextPosition, _tr, _br, WallAngle.HalfPi);
			TryFindBestIntersection(ref found, ref bestPoint, ref bestAngle, ballPosition, ballNextPosition, _tl, _tr, WallAngle.Zero);
			TryFindBestIntersection(ref found, ref bestPoint, ref bestAngle, ballPosition, ballNextPosition, _bl, _br, WallAngle.Zero);

			if (found == false)
				return;

			ProcessCollision(bestPoint, bestAngle);
		}

		private void TryFindBestIntersection(ref bool found, ref Vector3 bestPoint, ref WallAngle bestAngle, 
			Vector3 ballPosition, Vector3 ballNextPosition, Vector3 cornerA, Vector3 cornerB, WallAngle candidateAngle)
		{
			Vector3 point;
			if (HaveIntersection(ballPosition, ballNextPosition, cornerA, cornerB, out point))
			{
				if (found)
				{
					var oldDistance = Vector3.Distance(ballPosition, bestPoint);
					var newDistance = Vector3.Distance(ballPosition, point);

					if (newDistance < oldDistance)
					{
						bestPoint = point;
						bestAngle = candidateAngle;
					}
				}
				else
				{
					found = true;
					bestPoint = point;
					bestAngle = candidateAngle;
				}
			}
		}

		private bool HaveIntersection(Vector3 ballPosition, Vector3 ballNextPosition, Vector3 tl, Vector3 bl, out Vector3 point)
		{
			throw new NotImplementedException();
		}
	} 
} 


