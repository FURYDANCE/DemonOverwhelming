using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// 
/// ��ʵ����д�¼�ϵͳ���������������Լ�д�ĳ������Դ�����ժ����һ����
/// 
/// 
/// �¼����������
/// ���е��¼���������UI�����¼��������ù�����д�¼�����ֹ������Ϸ���Ӷ�����ά�����ң�
/// Ŀ�ģ����еĽ������߼�����
/// ���ฺ�����е��¼��ַ������߼�
/// </summary>
public class _EventManager : MonoBehaviour
{
	/// <summary>
	/// ��Ϣ�б�ÿһ���¼���Ӧһ��ö�����͵���Ϣ��ö���������Ϊ���ͣ�����ÿ���������е������¼���Ϣ�㣬��ֹ���˿�����ͻ
	/// </summary>
	[Header("��Ϣ�����б�,�붨�岻���ظ�����Ϣkey")]
	public string[] __EVENTMSG;

	/// <summary>
	/// ��������������ܲ㣬�ӵ���Ϣ��Ļص�����
	/// ������һ����Ϣ������Ϊ�˸������ע�ᵽͳһ��ں����У��ڶ��������ǻص������Ĳ�������Ϊ��
	/// </summary>
	public delegate void EventHandle(object __PARAM = null);

	/// <summary>
	/// �����¼��ֵ䣬��¼���зַ��е��¼�
	/// ֧��һ���¼����Ŀ����Ӧ
	/// </summary>
	public Dictionary<string, List<EventHandle>> __EVENTDIC;

	void Awake()
	{
		__EVENTDIC = new Dictionary<string, List<EventHandle>>();
		//���¼������ֵ��й���
		__AddEvent();
	}

	/// <summary>
	/// ע���¼�
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
	/// ���¼������ֵ��й���
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
	/// ����һ���¼�
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
	/// �¼���
	/// �¼��Ľ��ղ�Ҫ���������������ȻҲ����ʹ������������󶨷���������Ҫÿһ���¼�дһ������
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
	/// �������룬���е��¼������������Ψһ��
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
	/// ȥ���¼���
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
	/// ���ٹ����������ô��루����ģʽ�������¼��ַ���ʱ�򣬽���ʹ�������٣���Ϊ�������Զ����٣�
	/// </summary>
	public void __Destroy()
	{
		MonoBehaviour.Destroy(this.gameObject);
	}
}