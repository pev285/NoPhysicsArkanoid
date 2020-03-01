using NoPhysArkanoid.Forms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.LevelElements
{
	public class BallStarter : MonoBehaviour 
	{
		private enum StarterState
		{
			Active,
			Inactive
		}

		[SerializeField]
		private Transform _arrow;
		[SerializeField]
		private float _angleVariance = 45f;

		[SerializeField]
		private float _startAngle = 0f;
		[SerializeField]
		private float _changeSpeed = 30f;

		public Vector3 Direction
		{
			get
			{
				return _arrow.up;
			}
		}

		public Vector3 Position
		{
			get
			{
				return _transform.position;
			}
		}

		private float _angle;
		private float _changeSign;

		private StarterState _state;

		private GameObject _go;
		private Transform _transform;

		private void Awake()
		{
			_go = gameObject;
			_transform = transform;

			Deactivate();
		}

		public void Activate()
		{
			_changeSign = 1;
			_angle = _startAngle;

			ApplyRotation();

			Show();
			_state = StarterState.Active;
		}

		public void Deactivate()
		{
			_state = StarterState.Inactive;
			Hide();
		}

		private void Update()
		{
			switch (_state)
			{
				case StarterState.Active:
					UpdateAngle();
					ApplyRotation();
					break;
				case StarterState.Inactive:
					break;
			}
		}

		private void ApplyRotation()
		{
			_transform.rotation = Quaternion.Euler(0, 0, -_angle);
		}

		private void UpdateAngle()
		{
			_angle += _changeSign * Time.deltaTime * _changeSpeed;

			if (_angle >= _angleVariance || _angle <= -_angleVariance)
			{
				_angle = Mathf.Clamp(_angle, -_angleVariance, _angleVariance);
				_changeSign = -_changeSign;
			}
		}

		private void Show()
		{
			_go.SetActive(true);
		}

		private void Hide()
		{
			_go.SetActive(false);
		}
	} 
} 


