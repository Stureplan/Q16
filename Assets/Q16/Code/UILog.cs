using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UILog : MonoBehaviour
{
    public Text notifyText, messageText;

    float notifyDuration = 5.0f;
    float messageDuration = 4.0f;

    int maxScreenMessages = 3;
    int currentMessage = 0;

    int frameCounter = 0;

	void Start ()
    {
	}
	
	void Update ()
    {
        if (frameCounter % 240 == 0)
        {

            frameCounter = 0;
        }

        frameCounter++;

		//TODO: decrease framerate on this update!
        //if (currentMessage != MessageLog.GetLastMessageID())
        {
            //string lastMsg = MessageLog.GetLastMessage();
            //if (lastMsg != null)
            {
                //currentMessage++;
                //text.text = lastMsg;
                //TODO: set alpha to full here
            }
        }
    }

    public void Notify(string msg)
    {
        notifyText.text += msg + "\n";
        StartCoroutine(IETextTimer(notifyText, notifyDuration, true));
    }

    public void Message(string msg)
    {
        messageText.text = msg + "\n";
        StartCoroutine(IETextTimer(messageText, messageDuration, false));

    }




    IEnumerator IETextTimer(Text text, float seconds, bool fade)
    {
        float t = 0.0f;
        text.CrossFadeAlpha(1.0f, 0.001f, false);

        while (t < seconds)
        {
            t += Time.deltaTime;

            yield return null;
        }

        if (fade)
        {
            text.CrossFadeAlpha(0.0f, 0.3f, false);
        }
        else
        {
            text.text = "";
        }

        //done
    }
}
