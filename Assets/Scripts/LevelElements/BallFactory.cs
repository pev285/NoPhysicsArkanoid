using NoPhysArkanoid.Forms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.Management 
{
	public class BallFactory : MonoBehaviour 
	{
		[SerializeField]
		private GameObject _prefab;

		private void Awake()
		{
			EventBuss.CreateNewBall += CreateNew;	
		}

		private void OnDestroy()
		{
			EventBuss.CreateNewBall -= CreateNew;
		}

		public Ball CreateNew(Vector3 position)
		{
			var obj = Instantiate(_prefab);
			var ball = obj.GetComponent<Ball>();

			ball.SetPosition(position);
			return ball;
		}	
	} 
} 


