using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// 
/// （实际上写事件系统还不熟练，担心自己写的出错所以从网上摘抄了一个）
/// 
/// 
/// 事件基类管理器
/// 所有的事件（尤其是UI交互事件都必须用管理器写事件，防止后期游戏复杂度增加维护混乱）
/// 目的：所有的界面与逻辑分离
/// 基类负责所有的事件分发处理逻辑
/// </summary>
public class _EventManager : MonoBehaviour
{
	/// <summary>
	/// 消息列表，每一个事件对应一个枚举类型的消息，枚举类型设计为泛型，方便每个场景都有单独的事件消息层，防止多人开发冲突
	/// </summary>
	[Header("消息类型列表,请定义不能重复的消息key")]
	public string[] __EVENTMSG;

	/// <summary>
	/// 定义代理方法，接受层，接到消息后的回调函数
	/// 保留第一个消息参数，为了更方便的注册到统一入口函数中，第二个参数是回调函数的参数，可为空
	/// </summary>
	public delegate void EventHandle(object __PARAM = null);

	/// <summary>
	/// 定义事件字典，记录所有分发中的事件
	/// 支持一个事件多个目标响应
	/// </summary>
	public Dictionary<string, List<EventHandle>> __EVENTDIC;

	void Awake()
	{
		__EVENTDIC = new Dictionary<string, List<EventHandle>>();
		//将事件放入字典中管理
		__AddEvent();
	}

	/// <summary>
	/// 注册事件
	/// </summary>
	private void __AddEvent()
	{
		if (0 == __EVENTMSG.Length)
			Debug.LogWarning("_EVENTMSG is empty,please define add msg key as a array!!");
		foreach (string __MSG in __EVENTMSG)
		{
			__AddDelegate(__MSG);
		}
	}

	/// <summary>
	/// 将事件放入字典中管理
	/// </summary>
	/// <param name="_eventKey">Event key.</param>
	private void __AddDelegate(string __MSG)
	{
		if (__EVENTDIC.ContainsKey(__MSG))
		{
		}
		else
		{
			__EVENTDIC.Add(__MSG, new List<EventHandle>());
		}
	}

	/// <summary>
	/// 触发一个事件
	/// </summary>
	/// <param name="_eventKey">Event key.</param>
	/// <param name="param">Parameter.</param>
	public void __TriggerEvent(string __MSG, object __param = null)
	{
		if (__EVENTDIC.ContainsKey(__MSG))
		{
			foreach (EventHandle __handle in __EVENTDIC[__MSG])
			{
				__handle(__param);
			}
		}
		else
		{
			Debug.LogError(__MSG + " is undefine from function: _EventBaseManager::_TriigerEvent");
		}
	}
	/// <summary>
	/// 事件绑定
	/// 事件的接收层要调用这个方法，当然也可以使用下面的批量绑定方法，不需要每一个事件写一个函数
	/// </summary>
	public void __AttachEvent(string __MSG, EventHandle __eventHandle)
	{
		if (__EVENTDIC.ContainsKey(__MSG))
		{
			__EVENTDIC[__MSG].Add(__eventHandle);
		}
		else
		{
			Debug.LogError(__MSG + " is undefine from function: _EventBaseManager::_AttachEvent");
		}
	}
	/// <summary>
	/// 批量导入，所有的事件处理函数入口是唯一的
	/// </summary>
	/// <param name="__eventHandle">Event handle.</param>
	public void __BatchAttachEvent(EventHandle __eventHandle)
	{
		foreach (string __MSG in __EVENTMSG)
		{
			__AttachEvent(__MSG, __eventHandle);
		}
	}
	/// <summary>
	/// 去除事件绑定
	/// </summary>
	public void __DetachEvent(string __MSG, EventHandle __eventHandle)
	{
		if (__EVENTDIC.ContainsKey(__MSG))
		{
			__EVENTDIC[__MSG].Remove(__eventHandle);
		}
		else
		{
			Debug.LogError(__MSG + " is undefine from function: _EventBaseManager::_DetachEvent");
		}
	}

	/// <summary>
	/// 销毁管理器，当用代码（单例模式）控制事件分发的时候，结束使用请销毁（因为它不会自动销毁）
	/// </summary>
	public void __Destroy()
	{
		MonoBehaviour.Destroy(this.gameObject);
	}
}