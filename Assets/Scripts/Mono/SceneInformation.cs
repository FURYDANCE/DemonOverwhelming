using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SceneInformation : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;
    public Image panel;
  

     public void SetInformation(Sprite sprite,string information)
    {
        image.sprite = sprite;
        text.text = information;
    }
}
