using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPArkanoid
{
	public class Ball : MonoBehaviour 
	{
		public float Radius { get; private set; }
		public Vector3 Position 
		{
			get
			{
				return _transform.position;
			}
		}

		protected Transform _transform;

		private void Awake()
		{
			_transform = transform;
			Radius = 0.5f * _transform.localScale.x;
		}


	} 
} 


