using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Forms
{
	public class BoxFigure : MonoBehaviour 
	{
		protected float _width;
		protected float _height;

		protected Vector3 _position;
		protected Transform _transform;

		protected float _top;
		protected float _bottom;
		protected float _left;
		protected float _right;


		protected Vector3 _tl;
		protected Vector3 _bl;
		protected Vector3 _tr;
		protected Vector3 _br;

		protected virtual void Awake()
		{
			_transform = transform;
		}

		protected virtual void Start()
		{
			RecalculateLimits();
		}

		public void RecalculateLimits()
		{
			_position = _transform.position;

			_width = _transform.localScale.x;
			_height = _transform.localScale.y;

			_left = _position.x - 0.5f * _width;
			_right = _position.x + 0.5f * _width;
			_top = _position.y + 0.5f * _height;
			_bottom = _position.y - 0.5f * _height;

			_tl = new Vector3(_left, _top);
			_tr = new Vector3(_right, _top);
			_bl = new Vector3(_left, _bottom);
			_br = new Vector3(_right, _bottom);
		}

		public bool CheckCollision(CircleFigure circle, out Vector3 point, out EdgeAngle angle)
		{
			RecalculateLimits();

			point = Vector3.zero;
			angle = EdgeAngle.Zero;
			
			var radius = circle.Radius;

			var position = circle.Position; //-- TODO: Use old and new positions to check trajectory --
			var nextPosition = circle.NextPosition;

			var eleft = _left - radius;
			var eright = _right + radius;
			var etop = _top + radius;
			var ebottom = _bottom - radius;

			if (nextPosition.x < eleft || nextPosition.x > eright)
				return false;

			if (nextPosition.y < ebottom || nextPosition.y > etop)
				return false;

			var thePointIsCorner = false;

			if (nextPosition.x > _right && nextPosition.y > _top)
			{
				point = _tr;
				thePointIsCorner = true;
			}

			if (nextPosition.x < _left && nextPosition.y > _top)
			{
				point = _tl;
				thePointIsCorner = true;
			}

			if (nextPosition.x > _right && nextPosition.y < _bottom)
			{
				point = _br;
				thePointIsCorner = true;
			}

			if (nextPosition.x < _left && nextPosition.y < _bottom)
			{
				point = _bl;
				thePointIsCorner = true;
			}

			if (thePointIsCorner)
			{
				var dist = (nextPosition - point).magnitude;

				if (dist > radius)
					return false;

				return true;
			}

			if (nextPosition.x > _right || nextPosition.x < _left)
			{
				angle = EdgeAngle.HalfPi;
			}
			else if (nextPosition.y > _top || nextPosition.y < _bottom)
			{
				angle = EdgeAngle.Zero;
			}
			else if (position.x > _right || position.x < _left)
			{
				angle = EdgeAngle.HalfPi;
			}
			else if (position.y > _top || position.y < _bottom)
			{
				angle = EdgeAngle.Zero;
			}

			return true;
		}
	}
} 


