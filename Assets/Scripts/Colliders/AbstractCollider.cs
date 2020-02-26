using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPArkanoid.Colliders
{
	public abstract class AbstractCollider : MonoBehaviour 
	{
		protected Ball _ball;

		protected abstract void ProcessCollision();
	
	} 
} 


