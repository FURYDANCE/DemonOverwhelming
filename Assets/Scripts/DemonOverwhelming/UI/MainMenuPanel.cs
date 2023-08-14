using BJSYGameCore.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DemonOverwhelming
{
    public class MainMenuPanel : UIPanel
    {
        protected void Awake()
        {
            _startButton.onClick.AddListener(OnClickStartButtonCallback);
            _exitButton.onClick.AddListener(OnClickExitButtonCallback);
        }
        private void OnClickStartButtonCallback()
        {
            OnClickStartButton?.Invoke();
        }
        private void OnClickExitButtonCallback()
        {
            OnClickExitButton?.Invoke();
        }
        public event Action OnClickStartButton;
        public event Action OnClickExitButton;
        [SerializeField]
        private Button _startButton;
        [SerializeField]
        private Button _continueButton;
        [SerializeField]
        private Button _exitButton;
    }
}
