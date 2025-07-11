using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("start Timer...");
        StartCoroutine("CountdownTimer");
    }

    private IEnumerator CountdownTimer()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("Timer Complete");
    }
}
