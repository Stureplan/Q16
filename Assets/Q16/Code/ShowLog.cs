using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShowLog : MonoBehaviour
{
    Text text;
    int maxScreenMessages = 3;
    int currentMessage = 0;

	void Start ()
    {
        text = GetComponent<Text>();
	}
	
	void Update ()
    {
        if (currentMessage != MessageLog.GetLastMessageID())
        {
            string lastMsg = MessageLog.GetLastMessage();
            if (lastMsg != null)
            {
                currentMessage++;
                text.text = lastMsg;
                //TODO: set alpha to full here
            }
        }
    }



}
