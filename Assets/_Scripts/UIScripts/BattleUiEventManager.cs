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
		//���¼�
		_eventManager.__AttachEvent("ChangeCameaSliderPersentage", ChangeCameaSliderPersentage);
	}

	
	/// <summary>
	/// ��ʼ��Ϸ
	/// </summary>
	/// <param name="o">O.</param>
	public void ChangeCameaSliderPersentage(object o)
	{
		GetComponentInChildren<CameraBoundCaculate>().ChangeCameraPostion();
	}
}

