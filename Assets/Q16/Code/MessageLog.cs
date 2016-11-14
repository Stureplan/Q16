using UnityEngine;
using System.Collections;

public static class MessageLog
{
    static string[] ConsoleMessages;
    static UILog uiLog;

    public static void InitializeLog(UILog log)
    {
        ConsoleMessages = new string[0];
        uiLog = log;
    }

    public static void AddToConsole(string msg)
    {
        int lastMsg = ConsoleMessages.Length + 1;
        ConsoleMessages = new string[lastMsg];

        ConsoleMessages[lastMsg-1] = msg;
    }

    public static string GetLastMessage()
    {
        if (ConsoleMessages.Length > 0)
        {
            return ConsoleMessages[ConsoleMessages.Length - 1];
            
        }
        else
        {
            return null;
        }
    }

    public static string[] GetLastMessages(int amt)
    {
        if (ConsoleMessages.Length >= amt)
        {
            string[] msgs = new string[amt];
            for (int i = 0; i < amt; i++)
            {
                msgs[i] = ConsoleMessages[ConsoleMessages.Length - i];
            }
        }

        return null;
    }
    
    public static int GetLastMessageID()
    {
        return ConsoleMessages.Length;
    }

    public static void Notify(string msg)
    {
        uiLog.Notify(msg);
        AddToConsole(msg);
    }

    public static void Message(string msg)
    {
        uiLog.Message(msg);
        AddToConsole(msg);
    }
}
