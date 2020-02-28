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

		public bool CheckCollision(CircleFigure circle, out Vector3 touchPoint, out EdgeAngle wallAngle)
		{
			RecalculateLimits();

			touchPoint = Vector3.zero;
			wallAngle = EdgeAngle.Zero;

			var radius = circle.Radius;

			var position = circle.Position; //-- TODO: Use old and new positions to check trajectory --
			var nextPosition = circle.NextPosition;

			if (IsInExtendedBox(radius, nextPosition) == false)
				return false;

			if (ClosestIsCorner(ref touchPoint, ref wallAngle, nextPosition))
				return IsCloseEnough(touchPoint, nextPosition, radius);


			if (IsOutOfLimits(ref touchPoint, ref wallAngle, nextPosition))
				return true;

			DefineByNearestEdge(ref touchPoint, ref wallAngle, position);
			return true;
		}

		private void DefineByNearestEdge(ref Vector3 touchPoint, ref EdgeAngle wallAngle, Vector3 position)
		{
			Vector3 leftPoint = Line.CreateFromTwoPoints(_tl, _bl).GetProjectionOf(position);
			Vector3 rightPoint = Line.CreateFromTwoPoints(_tr, _br).GetProjectionOf(position);
			Vector3 topPoint = Line.CreateFromTwoPoints(_tl, _tr).GetProjectionOf(position);
			Vector3 bottomPoint = Line.CreateFromTwoPoints(_bl, _br).GetProjectionOf(position);

			var leftDistance = (leftPoint - position).magnitude;
			var rightDistance = (position - rightPoint).magnitude;

			var topDistance = (position - topPoint).magnitude;
			var bottomDistance = (position - bottomPoint).magnitude;

			touchPoint = leftPoint;
			wallAngle = EdgeAngle.D90;
			var distance = leftDistance;


			if (rightDistance < distance)
			{
				touchPoint = rightPoint;
				wallAngle = EdgeAngle.D90;
				distance = rightDistance;
			}

			if (topDistance < distance)
			{
				touchPoint = topPoint;
				wallAngle = EdgeAngle.Zero;
				distance = topDistance;
			}

			if (bottomDistance < distance)
			{
				touchPoint = bottomPoint;
				wallAngle = EdgeAngle.Zero;
				distance = bottomDistance;
			}
		}

		private bool IsOutOfLimits(ref Vector3 touchPoint, ref EdgeAngle wallAngle, Vector3 position)
		{
			if (IsOutOfXLimits(ref touchPoint, ref wallAngle, position))
				return true;

			if (IsOutOfYLimits(ref touchPoint, ref wallAngle, position))
				return true;

			return false;
		}

		private bool IsOutOfXLimits(ref Vector3 touchPoint, ref EdgeAngle wallAngle, Vector3 position)
		{
			if (position.x > _left && position.x < _right)
				return false;

			Line line = null;

			if (position.x >= _right)
				line = Line.CreateFromTwoPoints(_tr, _br);
			else
				line = Line.CreateFromTwoPoints(_tl, _bl);

			wallAngle = EdgeAngle.D90;
			touchPoint = line.GetProjectionOf(position);

			return true;
		}

		private bool IsOutOfYLimits(ref Vector3 touchPoint, ref EdgeAngle wallAngle, Vector3 position)
		{
			if (position.y > _bottom && position.y < _top)
				return false;

			Line line = null;

			if (position.y >= _top)
				line = Line.CreateFromTwoPoints(_tl, _tr);
			else
				line = Line.CreateFromTwoPoints(_bl, _br);

			wallAngle = EdgeAngle.Zero;
			touchPoint = line.GetProjectionOf(position);

			return true;
		}

		private static bool IsCloseEnough(Vector3 point1, Vector3 point2, float maxDistance)
		{
			var dist = (point2 - point1).magnitude;

			if (dist > maxDistance)
				return false;

			return true;
		}

		private bool ClosestIsCorner(ref Vector3 point, ref EdgeAngle angle, Vector3 nextPosition)
		{
			var thePointIsCorner = false;

			if (nextPosition.x > _right && nextPosition.y > _top)
			{
				point = _tr;
				angle = EdgeAngle.D45;

				thePointIsCorner = true;
			}

			if (nextPosition.x < _left && nextPosition.y > _top)
			{
				point = _tl;
				angle = EdgeAngle.D135;

				thePointIsCorner = true;
			}

			if (nextPosition.x > _right && nextPosition.y < _bottom)
			{
				point = _br;
				angle = EdgeAngle.D135;

				thePointIsCorner = true;
			}

			if (nextPosition.x < _left && nextPosition.y < _bottom)
			{
				point = _bl;
				angle = EdgeAngle.D45;

				thePointIsCorner = true;
			}

			return thePointIsCorner;
		}

		private bool IsInExtendedBox(float extension, Vector2 point)
		{
			var eleft = _left - extension;
			var eright = _right + extension;
			var etop = _top + extension;
			var ebottom = _bottom - extension;

			if (point.x < eleft || point.x > eright)
				return false;

			if (point.y < ebottom || point.y > etop)
				return false;

			return true;
		}
	}
} 


