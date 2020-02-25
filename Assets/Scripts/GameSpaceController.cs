﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPArkanoid
{
	public class GameSpaceController : MonoBehaviour 
	{
		public static Vector3 BottomLeft { get; private set; }
		public static Vector3 UpperRight { get; private set; }

		public float Height
		{
			get
			{
				return UpperRight.z - BottomLeft.z;
			}
		}

		public float Width
		{
			get
			{
				return UpperRight.x - BottomLeft.x;
			}
		}

		public static Vector3 Center
		{
			get
			{
				return 0.5f * (BottomLeft + UpperRight);
			}
		}

		public static Vector3 RightCenter
		{
			get
			{
				return new Vector3(UpperRight.x, UpperRight.y, 0.5f * (UpperRight.z + BottomLeft.z));
			}
		}

		public static Vector3 LeftCenter
		{
			get
			{
				return new Vector3(BottomLeft.x, BottomLeft.y, 0.5f * (UpperRight.z + BottomLeft.z));
			}
		}

		public static Vector3 UpperCenter
		{
			get
			{
				return new Vector3(0.5f * (UpperRight.x + BottomLeft.x), UpperRight.y, UpperRight.z);
			}
		}



		[SerializeField]
		private Camera Camera;

		[SerializeField]
		private Transform Up;
		[SerializeField]
		private Transform Floor;

		[SerializeField]
		private Transform Right;
		[SerializeField]
		private Transform Left;

		private void Start()
		{
			if (Camera == null)
				Camera = Camera.main;

			var width = Screen.width;
			var height = Screen.height;

			BottomLeft = Camera.ScreenToWorldPoint(new Vector2(0, 0));
			UpperRight = Camera.ScreenToWorldPoint(new Vector2(width, height));

			BottomLeft = new Vector3(BottomLeft.x, 0, BottomLeft.z);
			UpperRight = new Vector3(UpperRight.x, 0, UpperRight.z);

			//Debug.Log($"{BottomLeft} --> {UpperRight}");

			SetupEnvironment();
		}

		private void SetupEnvironment()
		{
			Floor.position = Center;
			Floor.localScale = new Vector3(Width, 1, Height);

			Vector3 horizontalWallSize = new Vector3(Width, 2, 1);
			Vector3 verticalWallSize = new Vector3(1, 2, Height);

			Right.position = RightCenter;
			Right.localScale = verticalWallSize;

			Left.position = LeftCenter;
			Left.localScale = verticalWallSize;

			Up.position = UpperCenter;
			Up.localScale = horizontalWallSize;
		}
	} 
} 


