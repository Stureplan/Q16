using UnityEngine;
using System.Collections;

public static class Stats
{
    public static Data info;

    public struct Data
    {
        public int timesLoaded;
        public int amountHit;
        public int amountShot;
        public int enemiesKilled;
        public int secretsFound;
    }

    static Stats()
    {
        ResetData();
    }

    public static void ResetData()
    {
        info.timesLoaded = 0;
        info.amountHit = 0;
        info.amountShot = 0;
        info.enemiesKilled = 0;
        info.secretsFound = 0;
    }
}
