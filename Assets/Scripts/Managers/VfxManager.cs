using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxManager : MonoBehaviour
{
    public VfxDataObject vfxDatas;

    public GameObject vfx_Buff;
    public GameObject vfx_Aoe;
    public GameObject vfx_Aoe_2;
    public GameObject vfx_Hit;
    public GameObject vfx_fire_1; 

    int createAmount;
    public static VfxManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }
    /// <summary>
    /// 通过id或者名称找到对应的粒子特效
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public VfxData GetVfxByIdOrName(string id)
    {
        VfxData find;
        find = vfxDatas.vfxDatas.Find((VfxData p) => { return p.id == id; });
        if (find == null)
            find = vfxDatas.vfxDatas.Find((VfxData p) => { return p.name == id; });
        if (find != null)
            return find;
        else
            return null;
    }
    /// <summary>
    /// 创建粒子特效并且作为第二个参数的子物体，当其子物体中有同名特效时将不会继续生成新特效
    /// </summary>
    /// <param name="vfx"></param>
    /// <param name="targetTransform"></param>
    /// <param name="scale"></param>
    /// <param name="lifeTime"></param>
    /// <returns></returns>
    public GameObject CreateVfx(GameObject vfx, Transform targetTransform, Vector3 scale, float lifeTime)
    {
        //如果目标的子对象中存在同名的vfx，则不生成
        foreach (Transform child in targetTransform)
        {
            if (child.name.Contains(vfx.name))
                return null;
        }
        createAmount++;
        GameObject go = GameObject.Instantiate(vfx);
        go.name = vfx.name + "_" + createAmount;
        go.transform.position = targetTransform.position;
        go.transform.SetParent(targetTransform);
        go.transform.localScale = scale;
        go.transform.rotation = Quaternion.Euler(-45, 0, 0);
        if (lifeTime > 0)
        {
            go.AddComponent<LifeTime>().lifeTime = lifeTime;
        }
        return go;
    }
    /// <summary>
    /// 在目标位置生成特效
    /// </summary>
    /// <param name="vfx"></param>
    /// <param name="targetPos"></param>
    /// <param name="scale"></param>
    /// <param name="lifeTime"></param>
    /// <returns></returns>
    public GameObject CreateVfx(GameObject vfx, Vector3 targetPos, Vector3 scale, float lifeTime)
    {

        createAmount++;
        GameObject go = GameObject.Instantiate(vfx);
        go.name = vfx.name + "_" + createAmount;
        go.transform.position = targetPos;

        go.transform.localScale = scale;
        go.transform.rotation = Quaternion.Euler(-45, 0, 0);
        if (lifeTime > 0)
        {
            go.AddComponent<LifeTime>().lifeTime = lifeTime;
        }
        return go;
    }

}
[System.Serializable]
public class VfxData
{
    public string id;
    public string name;
    public GameObject vfx;
}