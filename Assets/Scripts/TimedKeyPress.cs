using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TimedKeyPress
{
    private static float time = 1f;
    private static float timeBuffer = 0.125f;

    [SerializeField] private KeyCode keyCode;
    [SerializeField] private Vector3 triggerPosition;

    public IEnumerator Show(Text text, Slider slider, Action<bool> callback)
    {
        //Animate the bar and text and wait the designated time

        yield return new WaitForSeconds(time - timeBuffer);

        //Set the text to glow and wait for input
        float counter = 0;

        while (counter <= timeBuffer)
        {
            if (Input.GetKey(keyCode))
            {
                callback(true);
                yield break;
            }

            counter += Time.deltaTime;
            yield return null;
        }

        yield return null;
    }
}