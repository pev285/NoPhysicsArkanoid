using NoPhysArkanoid.Collisions;
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

		public bool IsMoving
		{
			get { return _state == State.Moving; }
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

		public void SetPosition(Vector3 position)
		{
			_transform.position = position;
		}

		public void StartBall(Vector3 direction)
		{
			TransitionToMovingState(direction);
		}

		public void ExpectColliderHit(Hit hit)
		{
			NextPosition = hit.Position + hit.Normal * Radius * 1.1f;

			switch (hit.Angle)
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

			ApplyDirectionNoise();
		}

		private void ApplyDirectionNoise()
		{
			Vector3 noise = 0.3f * UnityEngine.Random.insideUnitCircle;

			_direction = (_direction + noise).normalized;
		}

		public override void MarkOutOfScreen()
		{
			TransitionToState(State.OutOfScreen);
		}

		private void TransitionToMovingState(Vector3 direction)
		{
			if (_state == State.Slave || _state == State.Paused)
			{
				_state = State.Moving;
				_direction = direction.normalized;
			}
		}

		private void TransitionToState(State state)
		{
			switch (state)
			{
				case State.Slave:
					_state = state;
					break;
				case State.Moving:
					throw new Exception("Use TransitionToMovingState() method!");
				case State.OutOfScreen:
					_destroingTimer = 0;
					_state = state;
					break;
				case State.Paused:
					_state = state;
					break;
				default:
					throw new ArgumentException($"Unexpected state type {state}");
			}
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


