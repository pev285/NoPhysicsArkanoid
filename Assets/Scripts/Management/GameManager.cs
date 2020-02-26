using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NPArkanoid.Management
{
	public class GameManager : MonoBehaviour 
	{
		[SerializeField]
		private GameSpaceController _spaceController;

		[SerializeField]
		private string[] _levelNames;

		private int _currentLevelIndex;

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
			Subscribe();
		}

		private void Start()
		{
			if (_levelNames.Length < 1)
			{
				Exit();
				return;
			}

			LoadFirstScene();
			_spaceController.SetupEnvironment();
		}

		private void OnDestroy()
		{
			Unsubscribe();
		}


		private void Subscribe()
		{
			EventBuss.LevelIsCleared += LoadNextLevel;
			EventBuss.LevelIsFailed += ReloadCurrentLevel;
		}

		private void Unsubscribe()
		{
			EventBuss.LevelIsCleared -= LoadNextLevel;
			EventBuss.LevelIsFailed -= ReloadCurrentLevel;
		}


		private void LoadFirstScene()
		{
			_currentLevelIndex = 0;
			SceneLoader.LoadScene(_levelNames[_currentLevelIndex]);
		}

		private void LoadNextLevel()
		{
			var op = SceneLoader.UnloadScene(_levelNames[_currentLevelIndex]);
			_currentLevelIndex++;

			if (_currentLevelIndex >= _levelNames.Length)
			{
				Exit();
				return;
			}

			op.completed += (obj) => SceneLoader.LoadScene(_levelNames[_currentLevelIndex]);
		}

		private void ReloadCurrentLevel()
		{
			string levelName = _levelNames[_currentLevelIndex];

			var op = SceneLoader.UnloadScene(levelName);
			op.completed += (obj) => SceneLoader.LoadScene(levelName);
		}

		private void Exit()
		{
#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}

	} 
} 


