using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DpsCaculate : MonoBehaviour
{
    public TextMeshProUGUI dpsText;
    public float dps;
    // Update is called once per frame
    void Update()
    {
        dpsText.text="DPS:"+ dps.ToString();
    }
    public void AddDpsAmount(float amount)
    {
        StartCoroutine(AddDps(amount));
    }
    IEnumerator AddDps(float amount)
    {
        dps += amount;
        yield return new WaitForSecondsRealtime(1);
        dps -= amount;
    }
}
