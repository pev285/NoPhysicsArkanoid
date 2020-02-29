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
			OutOfScreen,
		}

		private State _state;

		private float _speed;
		private Vector3 _direction;

		private float _destroyDelay = 1f;
		private float _destroingTimer = 0;

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
			Level.Instance.Stats.Modified += UpdateParameters;
		}

		private void Unsubscribe()
		{
			Level.Instance.Stats.Modified -= UpdateParameters;
		}

		private void ResetBall()
		{
			_state = State.Slave;
			NextPosition = Position;
		}

		private void UpdateParameters()
		{
			var stats = Level.Instance.Stats;

			_speed = stats.BallSpeed;
			Radius = stats.BallRadius;
		}


		public void StartBall(Vector3 direction)
		{
			_direction = direction.normalized;
			TransitionToState(State.Moving);
		}

		public void ExpectColliderHit(Vector3 point, EdgeAngle angle)
		{
			//Debug.Log($"** point={point}, angle={angle.ToString()}, currentDirection = {_direction}");

			NextPosition = point;
			//NextPosition = (Position + NextPosition)/2;

			switch (angle)
			{
				case EdgeAngle.Zero:
					_direction.y = -_direction.y;
					break;
				case EdgeAngle.D90:
					_direction.x = -_direction.x;
					break;
				case EdgeAngle.D45:
					Swap(ref _direction.x, ref _direction.y);
					_direction = -_direction;
					break;
				case EdgeAngle.D135:
					Swap(ref _direction.x, ref _direction.y);
					break;
				default:
					throw new ArgumentException("Unexpected wall angle");
			}
		}

		public void MarkOutOfScreen()
		{
			TransitionToState(State.OutOfScreen);
		}

		private void TransitionToState(State state)
		{
			_state = state;

			if (_state == State.OutOfScreen)
				_destroingTimer = 0;
		}

		private void Swap(ref float a, ref float b)
		{
			var tmp = a;
			a = b;
			b = tmp;
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
				case State.OutOfScreen:
					_destroingTimer += Time.deltaTime;
					if (_destroingTimer >= _destroyDelay)
						Destroy(gameObject);
					break;
				default:
					throw new ArgumentException("Unexpected state");
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


