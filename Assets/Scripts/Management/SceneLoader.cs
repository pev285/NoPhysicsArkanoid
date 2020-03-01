using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NoPhysArkanoid.Management
{
	public static class SceneLoader 
	{
		public static AsyncOperation LoadScene(string name)
		{
			//Debug.Log($"Loading \"{name}\"");
			return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
		}

		public static AsyncOperation UnloadScene(string name)
		{
			//Debug.Log($"Unloading \"{name}\"");
			return SceneManager.UnloadSceneAsync(name);
		}
	} 
} 


