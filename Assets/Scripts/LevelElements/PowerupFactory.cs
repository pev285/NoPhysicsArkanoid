using NoPhysArkanoid.Forms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NoPhysArkanoid.LevelElements
{
	public class PowerupFactory : MonoBehaviour 
	{
		[SerializeField]
		private GameObject _prefab;

		private void Start()
		{
			EventBuss.PowerupCreationRequested += CreateNew;
		}

		private void OnDestroy()
		{
			EventBuss.PowerupCreationRequested -= CreateNew;
		}

		public void CreateNew(Powerup.Kind kind, Vector3 position)
		{
			var obj = Instantiate(_prefab, position, Quaternion.identity);
			var pw = obj.GetComponent<Powerup>();

			pw.PowerupKind = kind;
		}
	}
} 


