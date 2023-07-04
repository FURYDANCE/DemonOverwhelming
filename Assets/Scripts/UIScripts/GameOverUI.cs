using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// 临时的游戏结束后UI文字脚本
/// </summary>
public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI endText;

    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }
}
