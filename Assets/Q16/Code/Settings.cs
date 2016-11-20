using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Settings
{
    public delegate string Command(string value);
    private static Dictionary<string, Command> repository;
    public static Options options;
    public static Cheats cheats;

    #region OPTIONS
    public struct Options
    {
        public bool musicMute;
        public bool soundMute;
        public float musicVolume;
        public float soundVolume;
        public int resW, resH;
        public bool fullscreen;
    }
    #endregion

    #region CHEATS
    public struct Cheats
    {
        public bool god;
    }
    #endregion

    public static void Setup()
    {
        repository = new Dictionary<string, Command>();

        #region ADD_FUNCTIONS
        // SETTINGS
        repository.Add("ToggleMusic",       ToggleMusic);
        repository.Add("MusicVolume",       MusicVolume);
        repository.Add("ChangeResolutionW", ChangeResolutionW);
        repository.Add("ChangeResolutionH", ChangeResolutionW);
        repository.Add("ToggleFullscreen",  ToggleFullscreen);
        repository.Add("ApplyResolution",   ApplyResolution);

        // CHEATS
        repository.Add("Impulse", Impulse);
        repository.Add("God", God);
        #endregion

        #region OPTIONS_VALUES
        options.musicMute = false;
        options.soundMute = false;
        options.musicVolume = 1.0f;
        options.soundVolume = 1.0f;
        options.resW = 1080;
        options.resH = 1920;
        options.fullscreen = true;
        #endregion

        #region CHEATS_VALUES
        cheats.god = false;
        #endregion
    }

    public static void Cmd(string command, string value)
    {
        Command cmd;

        if (repository.TryGetValue(command, out cmd))
        {
            string temp = RegisterCmd(cmd, value);
            
            MessageLog.Notify(temp);
        }
        else
        {
            MessageLog.Notify("Command " + command + " not found.");
        }
    }

    public static string RegisterCmd(Command cmd, string value)
    {
        string t = cmd(value);

        return t;
    }






    public static string ToggleMusic(string value)
    {
        string msg;

        if (value.Length == 0)
        {
            options.musicMute = !options.musicMute;

            AudioSource music = Camera.main.GetComponent<AudioSource>();

            music.mute = options.musicMute;

            msg = "Changed mute music to " + options.musicMute.ToString();
        }
        else
        {
            msg = "Toggle Music can't have values after it.";
        }

        return msg;
    }

    public static string MusicVolume(string value)
    {
        string msg;
        double v;
        float volume;
        bool success;

        success = double.TryParse(value, out v);
        volume = (float)v;
        volume = Mathf.Clamp(volume, 0.0f, 1.0f);

        if (success)
        {
            options.musicVolume = volume;
            msg = "Changed music volume to: " + volume;
        }
        else
        {
            msg = "Couldn't change music volume to value: " + v;
        }

        return msg;
    }

    public static string ChangeResolutionW(string value)
    {
        string msg;
        int w;
        bool success;

        success = int.TryParse(value, out w);

        if (success && w >= 800 && w <= 1920)
        {
            options.resW = w;
            msg = "Changed resolution width to: " + w;
        }
        else
        {
            msg = "Couldn't understand value: " + w;
        }

        return msg;
    }

    public static string ChangeResolutionH(string value)
    {
        string msg;
        int h;
        bool success;

        success = int.TryParse(value, out h);

        if (success && h >= 600 && h <= 1080)
        {
            options.resH = h;
            msg = "Changed resolution height to: " + h;
        }
        else
        {
            msg = "Couldn't understand value: " + h;
        }

        return msg;
    }

    public static string ToggleFullscreen(string value)
    {
        string msg;
        
        
        if (value.Length == 0)
        {
            options.fullscreen = !options.fullscreen;
            msg = "Changed fullscreen to " + options.fullscreen.ToString();
        }
        else
        {
            msg = "Toggle Fullscreen can't have values after it.";
        }

        return msg;
    }

    public static string ApplyResolution(string value)
    {
        string msg;

        if (value.Length == 0)
        {
            Screen.SetResolution(options.resW, options.resH, options.fullscreen);
            msg = "Applied resolution: " + options.resW + " x " + options.resH;
        }
        else
        {
            msg = "Apply Resolution can't have values after it.";
        }

        return msg;
    }

    public static string Impulse(string value)
    {
        string msg;
        int n;
        bool success;

        success = int.TryParse(value, out n);

        if (success)
        {
            switch (n)
            {
                case 101:
                    Object.FindObjectOfType<Inventory>();
                    Inventory inv = GameObject.FindObjectOfType<Inventory>();
                    inv.SetHasAllWeapons(true);
                    msg = "Unlocked all weapons!";
                    break;

                case 666:
                    inv = GameObject.FindObjectOfType<Inventory>();
                    inv.SetItem(Item.SkeletonKey(), true);
                    msg = "Unlocked item: " + Item.SkeletonKey().name;
                    break;

                default:
                    msg = "Impulse " + n + " doesn't exist.";
                    break;
            }
        }
        else
        {
            msg = n + " isn't a proper number. (Use whole numbers only)";
        }

        return msg;
    }

    public static string God(string value)
    {
        string msg;

        if (value.Length == 0)
        {
            cheats.god = !cheats.god;

            msg = "God mode set to " + cheats.god;
        }
        else
        {
            msg = "God mode can't have values after it.";
        }

        return msg;
    }
}
