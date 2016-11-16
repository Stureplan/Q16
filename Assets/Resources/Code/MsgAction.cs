using UnityEngine;
using System.Collections;

public class MsgAction : ActionObject
{
    public string message;

    public override void Action(SenderInfo sender)
    {
        MessageLog.Message(sender.s_Tag + " spawned " + "Cultist");
    }
}
