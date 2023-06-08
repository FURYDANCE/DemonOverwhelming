using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUiEventManager : MonoBehaviour 
{
	private _EventManager _eventManager;
	// Use this for initialization
	void Start()
	{
		_eventManager = GameObject.Find("BattleUIEventManager").GetComponent<_EventManager>() as _EventManager;
		//绑定事件
		_eventManager.__AttachEvent("ChangeCameaSliderPersentage", ChangeCameaSliderPersentage);
	}

	
	/// <summary>
	/// 开始游戏
	/// </summary>
	/// <param name="o">O.</param>
	public void ChangeCameaSliderPersentage(object o)
	{
		GetComponentInChildren<CameraBoundCaculate>().ChangeCameraPostion();
	}
}

