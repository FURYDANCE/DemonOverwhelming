using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMovement : MonoBehaviour
{
    public AnimationCurve curve;
    public Transform target;
    public Vector3 targetV3;
    public float duration;
    public float heightY;
    private void Start()
    {
        curve = new AnimationCurve();
        if (curve.length != 0)
            curve.RemoveKey(0);
        curve.AddKey(0, 0);
        curve.AddKey(0.45f, 1);
        
        curve.AddKey(1, 0);
        if (target != null)
            StartCoroutine(Curve(transform.position, target));
        else
            StartCoroutine(Curve(transform.position, targetV3));

    }
    public void SetTarget(Transform target, float duration, float heightY)
    {
        this.target = target;
        this.duration = duration;
        this.heightY = heightY;
    }
    public void SetTarget(Vector3 target, float duration, float heightY)
    {
        this.targetV3 = target;
        this.duration = duration;
        this.heightY = heightY;
    }
    IEnumerator Curve(Vector3 start, Transform end)
    {
        float timePassed = 0f;
        while (timePassed < duration && end != null)
        {
            timePassed += Time.deltaTime;
            float linarT = timePassed / duration;
            float heightT = curve.Evaluate(linarT);
            float height = Mathf.Lerp(0, heightY, heightT);
            transform.position = Vector3.Lerp(start, end.position, linarT) + new Vector3(0, height, 0);
            yield return null;
        }
    }
    IEnumerator Curve(Vector3 start, Vector3 end)
    {
        float timePassed = 0f;
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linarT = timePassed / duration;
            float heightT = curve.Evaluate(linarT);
            float height = Mathf.Lerp(0, heightY, heightT);
            transform.position = Vector3.Lerp(start, end, linarT) + new Vector3(0, height, 0);
            yield return null;
        }
    }
}
