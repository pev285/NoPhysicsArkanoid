using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Collisions
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

		private float _sqrt2;

		protected virtual void Awake()
		{
			_sqrt2 = Mathf.Sqrt(2f);
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

		public bool CheckCollision(CircleFigure circle, out Hit hit)
		{
			RecalculateLimits();

			hit = default;
			var radius = circle.Radius;

			var position = circle.Position; //-- TODO: Use old and new positions to check trajectory --
			var nextPosition = circle.NextPosition;

			if (IsInExtendedBox(radius, nextPosition) == false)
				return false;

			if (IsInCloseBox(nextPosition) == false)
				return EvaluateReflection(ref hit, nextPosition, radius);

			if (EvaluateReflection(ref hit, position, radius))
				return true;

			DefineByNearestEdge(ref hit, position, radius);
			return true;
		}

		private bool EvaluateReflection(ref Hit hit, Vector3 position, float radius)
		{
			if (ClosestIsCorner(ref hit, position, radius))
				return IsCloseEnough(hit.Position, position, radius);

			return IsOutOfLimits(ref hit, position, radius);
		}

		private void DefineByNearestEdge(ref Hit hit, Vector3 position, float radius)
		{
			Vector3 leftPoint = Line.CreateFromTwoPoints(_tl, _bl).GetProjectionOf(position);
			Vector3 rightPoint = Line.CreateFromTwoPoints(_tr, _br).GetProjectionOf(position);
			Vector3 topPoint = Line.CreateFromTwoPoints(_tl, _tr).GetProjectionOf(position);
			Vector3 bottomPoint = Line.CreateFromTwoPoints(_bl, _br).GetProjectionOf(position);

			var leftDistance = (leftPoint - position).magnitude;
			var rightDistance = (position - rightPoint).magnitude;

			var topDistance = (position - topPoint).magnitude;
			var bottomDistance = (position - bottomPoint).magnitude;

			hit.Position = leftPoint;
			hit.Normal = Vector3.left;
			hit.Angle = EdgeAngle.D90;

			var distance = leftDistance;

			if (rightDistance < distance)
			{
				hit.Position = rightPoint;
				hit.Normal = Vector3.right;
				hit.Angle = EdgeAngle.D90;

				distance = rightDistance;
			}

			if (topDistance < distance)
			{
				hit.Position = topPoint;
				hit.Normal = Vector3.up;
				hit.Angle = EdgeAngle.Zero;

				distance = topDistance;
			}

			if (bottomDistance < distance)
			{
				hit.Position = bottomPoint;
				hit.Normal = Vector3.down;
				hit.Angle = EdgeAngle.Zero;
			}
		}

		private bool IsOutOfLimits(ref Hit hit, Vector3 position, float radius)
		{
			if (IsOutOfXLimits(ref hit, position, radius))
				return true;

			if (IsOutOfYLimits(ref hit, position, radius))
				return true;

			return false;
		}

		private bool IsOutOfXLimits(ref Hit hit, Vector3 position, float radius)
		{
			if (position.x > _left && position.x < _right)
				return false;

			if (position.x >= _right)
			{
				hit.Normal = Vector3.right;
				hit.Position = Line.CreateFromTwoPoints(_tr, _br).GetProjectionOf(position);
			}
			else
			{
				hit.Normal = Vector3.left;
				hit.Position = Line.CreateFromTwoPoints(_tl, _bl).GetProjectionOf(position);
			}

			hit.Angle = EdgeAngle.D90;
			return true;
		}

		private bool IsOutOfYLimits(ref Hit hit, Vector3 position, float radius)
		{
			if (position.y > _bottom && position.y < _top)
				return false;

			if (position.y >= _top)
			{
				hit.Normal = Vector3.up;
				hit.Position = Line.CreateFromTwoPoints(_tl, _tr).GetProjectionOf(position);
			}
			else
			{
				hit.Normal = Vector3.down;
				hit.Position = Line.CreateFromTwoPoints(_bl, _br).GetProjectionOf(position);
			}

			hit.Angle = EdgeAngle.Zero;
			return true;
		}

		private static bool IsCloseEnough(Vector3 point1, Vector3 point2, float maxDistance)
		{
			var dist = (point2 - point1).magnitude;

			if (dist > maxDistance)
				return false;

			return true;
		}

		private bool ClosestIsCorner(ref Hit hit, Vector3 position, float radius)
		{
			if (position.x > _right && position.y > _top)
			{
				hit.Angle= EdgeAngle.D45;
				hit.Normal = new Vector3(radius, radius).normalized;

				var line = Line.CreateFromPointAndNormal(_tr, hit.Normal);
				hit.Position = line.GetProjectionOf(position);

				return true;
			}

			if (position.x < _left && position.y > _top)
			{
				hit.Angle = EdgeAngle.D135;
				hit.Normal = new Vector3(-radius, radius).normalized;

				var line = Line.CreateFromPointAndNormal(_tl, hit.Normal);
				hit.Position = line.GetProjectionOf(position);

				return true;
			}

			if (position.x > _right && position.y < _bottom)
			{
				hit.Angle = EdgeAngle.D135;
				hit.Normal = new Vector3(radius, -radius).normalized;

				var line = Line.CreateFromPointAndNormal(_br, hit.Normal);
				hit.Position = line.GetProjectionOf(position);

				return true;
			}

			if (position.x < _left && position.y < _bottom)
			{
				hit.Angle = EdgeAngle.D45;
				hit.Normal = new Vector3(-radius, -radius).normalized;

				var line = Line.CreateFromPointAndNormal(_bl, hit.Normal);
				hit.Position = line.GetProjectionOf(position);

				return true;
			}

			return false;
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

		private bool IsInCloseBox(Vector3 point)
		{
			if (point.x < _left || point.x > _right)
				return false;

			if (point.y < _bottom || point.y > _top)
				return false;

			return true;
		}

	}
} 


