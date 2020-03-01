using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Collisions
{
	public class CircleFigure : MonoBehaviour 
	{
		[SerializeField]
		private float _radius;
		public float Radius 
		{ 
			get
			{
				return _radius;
			} 
			protected set
			{
				_radius = value;

				var diam = _radius * 2;
				_transform.localScale = new Vector3(diam, diam, diam);
			} 
		}

		public Vector3 Position
		{
			get
			{
				return _transform.position;
			}
		}
		public Vector3 NextPosition { get; protected set; }

		public Transform Transform { get { return _transform; } }
		protected Transform _transform;

		protected virtual void Awake()
		{
			_transform = transform;
		}

		public virtual void MarkOutOfScreen()
		{
			Destroy(gameObject);
		}
	}
} 


