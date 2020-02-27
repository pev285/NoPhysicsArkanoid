using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Forms
{
	public class CircleFigure : MonoBehaviour 
	{
		public float Radius { get; protected set; }

		public Vector3 Position
		{
			get
			{
				return _transform.position;
			}
		}
		public Vector3 NextPosition { get; protected set; }

		protected Transform _transform;

		protected virtual void Awake()
		{
			_transform = transform;
		}
	}
} 


