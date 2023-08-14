using BJSYGameCore;
using BJSYGameCore.UI;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DemonOverwhelming
{
    public class LoadingPanel : UIPanel
    {
        public void Display(params IAsyncOperation[] asyncOperations)
        {
            _asyncOperations = asyncOperations;
            _progressSlider.value = 0;
        }
        protected void Update()
        {
            _progressSlider.value = _asyncOperations.Average(op => op.progress);
        }

        [SerializeField]
        private Slider _progressSlider = null;
        private IAsyncOperation[] _asyncOperations;
    }
}
