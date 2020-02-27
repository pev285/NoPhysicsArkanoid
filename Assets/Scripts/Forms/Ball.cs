using NoPhysArkanoid.LevelElements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Forms
{
	public class Ball : CircleFigure
	{
		private enum State
		{
			Slave,
			Moving,
			Paused,
		}

		//public float Radius { get; private set; }
		//public Vector3 Position 
		//{
		//	get
		//	{
		//		return _transform.position;
		//	}
		//}
		//public Vector3 NextPosition { get; private set; }



		private State _state;
		//private Transform _transform;

		private float _speed;
		private float _hitForce;
		private Vector3 _direction;

		protected override void Awake()
		{
			base.Awake();
			ResetBall();
		}

		private void Start()
		{
			Subscribe();
			UpdateParameters();
		}

		private void OnDestroy()
		{
			Unsubscribe();
		}

		private void Subscribe()
		{
			Level.Instance.Stats.Changed += UpdateParameters;
		}

		private void Unsubscribe()
		{
			Level.Instance.Stats.Changed -= UpdateParameters;
		}

		private void ResetBall()
		{
			_state = State.Slave;

			NextPosition = Position;
			Radius = 0.5f * _transform.localScale.x;
		}

		private void UpdateParameters()
		{
			var stats = Level.Instance.Stats;

			_speed = stats.BallSpeed;
			_hitForce = stats.BallForce;
		}


		public void StartBall(Vector3 direction)
		{
			_direction = direction.normalized;
			TransitionToState(State.Moving);
		}

		public void ExpectColliderHit(Vector3 point, EdgeAngle angle)
		{
			Debug.Log($"point={point}, angle={angle.ToString()}");

			NextPosition = point;

			switch (angle)
			{
				case EdgeAngle.Zero:
					_direction.y = -_direction.y;
					break;
				case EdgeAngle.HalfPi:
					_direction.x = -_direction.x;
					break;
				default:
					throw new NotImplementedException("Unexpected wall angle");
			}
		}

		private void TransitionToState(State state)
		{
			_state = state;
		}


		private void Update()
		{
			switch (_state)
			{
				case State.Slave:
					UpdateSlave();
					break;
				case State.Moving:
					UpdateMoving();
					break;
				case State.Paused:
					UpdatePaused();
					break;
				default:
					throw new NotImplementedException("Unexpected state");
			}
		}

		private void UpdatePaused()
		{
			throw new NotImplementedException();
		}

		private void UpdateSlave()
		{
			NextPosition = Position;
		}


		private void UpdateMoving()
		{
			_transform.position = NextPosition; 

			NextPosition = Position + Time.deltaTime * _speed * _direction;
		}
	} 
} 


