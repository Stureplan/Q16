using UnityEngine;
using System.Collections;

public static class MessageLog
{
    static string[] Messages;

    public static void InitializeLog()
    {
        Messages = new string[0];
    }

    public static void AddMessage(string msg)
    {
        int lastMsg = Messages.Length + 1;
        Messages = new string[lastMsg];

        Messages[lastMsg-1] = msg;
    }

    public static string GetLastMessage()
    {
        if (Messages.Length > 0)
        {
            return Messages[Messages.Length - 1];
            
        }
        else
        {
            return null;
        }
    }

    public static string[] GetLastMessages(int amt)
    {
        if (Messages.Length >= amt)
        {
            string[] msgs = new string[amt];
            for (int i = 0; i < amt; i++)
            {
                msgs[i] = Messages[Messages.Length - i];
            }
        }

        return null;
    }

    public static int GetLastMessageID()
    {
        return Messages.Length;
    }
}
