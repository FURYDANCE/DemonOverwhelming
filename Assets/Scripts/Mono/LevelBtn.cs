using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBtn : MonoBehaviour
{
    public string levelName;
    public string targetSceneName;
    public Sprite levelSprite;
    [Multiline(5)]
    public string levelDescription;
    [Multiline(3)]
    public string leveltargetDescription;

    public bool Selecting;
    private void OnMouseEnter()
    {
        Selecting = true;
        GetComponent<SpriteRenderer>().material = GameDataManager.instance.materialObject.onMouseCoverMaterial;
    }
    private void OnMouseExit()
    {
        Selecting = false;
        GetComponent<SpriteRenderer>().material = GameDataManager.instance.materialObject.defaultMaterial;

    }
    private void Update()
    {
        if (Selecting == true && Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager_MapScene.instance.CreateLevelInformation(this);
        }
    }
}
