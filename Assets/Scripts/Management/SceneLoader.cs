using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NPArkanoid.Management
{
	public static class SceneLoader 
	{
		public static AsyncOperation LoadScene(string name)
		{
			return SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
		}

		public static AsyncOperation UnloadScene(string name)
		{
			return SceneManager.UnloadSceneAsync(name);
		}
	
	} 
} 


