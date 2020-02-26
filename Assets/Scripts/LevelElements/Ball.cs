using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPArkanoid.LevelElements
{
	public class Ball : MonoBehaviour 
	{
		private enum State
		{
			Slave,
			Moving,
			Paused,
		}

		public float Radius { get; private set; }
		public Vector3 Position 
		{
			get
			{
				return _transform.position;
			}
		}
		public Vector3 NextPosition { get; private set; }



		private State _state;
		private Transform _transform;

		private float Speed;
		private Vector3 _direction;

		private void Awake()
		{
			ResetBall();
		}

		private void Start()
		{
			UpdateParameters();
		}

		private void ResetBall()
		{
			_state = State.Slave;

			_transform = transform;
			Radius = 0.5f * _transform.localScale.x;
		}

		private void UpdateParameters()
		{
			var stats = Level.Instance.Stats;

			Speed = stats.BallSpeed;
		}


		public void StartBall(Vector3 direction)
		{
			_direction = direction;
			TransitionToState(State.Moving);
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

			NextPosition = Time.deltaTime * Speed * _direction;
		}
	} 
} 


