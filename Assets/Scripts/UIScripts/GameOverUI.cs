using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
/// <summary>
/// ��ʱ����Ϸ������UI���ֽű�
/// </summary>
public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI endText;

    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }
}
