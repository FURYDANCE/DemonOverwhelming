using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
public class MoneyBag : MonoBehaviour
{
    public float moneyAmount;
    public MoveToTarget trail;
    private void Start()
    {
        StartCoroutine(waitForOnGround());
    }
    public void StartWork()
    {
        //StartCoroutine(StartWorkIE());
        BattleManager.instance.CreateSceneInformation(gameObject, GetComponent<SpriteRenderer>().sprite, "½ðÇ®+" + moneyAmount);
        GetComponent<Collider>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        BattleManager.instance.AddMoney(moneyAmount);
        Destroy(gameObject);
    }
    IEnumerator waitForOnGround()
    {
        yield return new WaitForSeconds(1.5f);
        GetComponent<Collider>().enabled = true;
    }
    IEnumerator StartWorkIE()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Vector3 P = Camera.main.ScreenToWorldPoint(SceneObjectsManager.instance.moneyText.rectTransform.position);
        Debug.Log(P);
        trail.StartMove(new Vector3(P.x, P.y, P.z), 1);
        yield return new WaitUntil(trail.GetMoving);
        BattleManager.instance.AddMoney(moneyAmount);
        Destroy(gameObject);
    }
}

