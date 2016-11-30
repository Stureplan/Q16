using UnityEngine;
using System.Collections;

public class ActionMsg : ActionObject
{
    public string message;

    public override void Action(SenderInfo sender)
    {
        MessageLog.Message(message);
    }
}
