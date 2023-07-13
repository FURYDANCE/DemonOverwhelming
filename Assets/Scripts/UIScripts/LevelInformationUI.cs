using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LevelInformationUI : MonoBehaviour
{
    public TextMeshProUGUI levelNameText;
    public string sceneName;
    public Image levelSpriteImage;
    public TextMeshProUGUI levelDescriptionText;
    public TextMeshProUGUI levelTargetText;

    public void SetInformation(LevelBtn information)
    {
        levelNameText.text = information.levelName;
        sceneName = information.targetSceneName;
        levelSpriteImage.sprite = information.levelSprite;
        levelDescriptionText.text = information.levelDescription;
        levelTargetText.text = information.leveltargetDescription;
    }
    public void IntoLevel()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void ReSelect()
    {
        SceneManager_MapScene.instance.ReSelectLevel();
    }
}
