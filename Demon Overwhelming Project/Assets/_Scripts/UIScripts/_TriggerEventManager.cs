using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TriggerEventManager : MonoBehaviour
{
	
	private _EventManager _eventManager;
	void Awake()
	{
		_eventManager = GetComponent<_EventManager>() as _EventManager;
	}
	/// <summary>
	/// �޲��¼�
	/// </summary>
	/// <param name="eventKey">Event key.</param>
	public void TriggerEventFromNoParam(string eventKey)
	{
		_eventManager.__TriggerEvent(eventKey);

	}
	
}
