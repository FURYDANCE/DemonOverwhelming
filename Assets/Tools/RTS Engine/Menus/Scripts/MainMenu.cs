﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RTSEngine
{
	public class MainMenu : MonoBehaviour {

		public string SinglePlayerSceneName;
		public string MultiplayerSceneName;

        public GameObject multiplayerButton;
        public GameObject webGLMultiplayerMsg;
        public GameObject exitButton;

        private void Awake()
        {
#if UNITY_WEBGL
            multiplayerButton.SetActive(false);
            webGLMultiplayerMsg.SetActive(true);
            exitButton.SetActive(false);
#endif
        }

        public void LeaveGame ()
		{
			Application.Quit ();
		}

		public void SinglePlayerMenu ()
		{
			SceneManager.LoadScene (SinglePlayerSceneName);
		}

		public void MultiplayerMenu ()
		{
			SceneManager.LoadScene (MultiplayerSceneName);
		}
	}
}