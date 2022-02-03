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

    public IEnumerator Show(Text text, Scrollbar sc, Action callback = null)
    {
        //Animate the text
        {
            float counter = 0;

            while (counter < time)
            {
                //Animate bar

                counter += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        {
            float counter = 0;
        }

        yield return null;
    }
}