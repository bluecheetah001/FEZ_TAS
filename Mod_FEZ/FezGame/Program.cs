using Common;
using EasyStorage;
using FezEngine.Tools;
using System;
using System.IO;
using FezGame.Tools;
using System.Threading;
using FezTas;
using MonoMod;

namespace FezGame
{
    public class Program
    {
        [MonoModReplace]
        private static void Main(string[] args)
        {
            if (!Directory.Exists(Tas.Directory))
                Directory.CreateDirectory(Tas.Directory);
            if (File.Exists(Path.Combine(Tas.Directory, "Debug Log.txt")))
            {
                File.Delete(Path.Combine(Tas.Directory, "Debug Log.txt"));
            }
            Logger.Clear();

            // copy alsoft.ini because
            try
            {
                File.Copy("alsoft.ini", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "alsoft.ini"), true);
            }
            catch (Exception ex)
            {
                Logger.Log("alsoft.ini", LogSeverity.Error, ex.ToString());
            }

            try
            {
                ApplyArgs(args);
            }
            catch (Exception e)
            {
                Logger.Log("args", LogSeverity.Error, e.ToString());
            }

            Logger.Try(Run);
            Logger.Log("FEZ", "Exiting.");

            try
            {
                ThreadExecutionState.TearDown();
            }
            catch (Exception ex)
            {
                Logger.Log("ThreadExecutionState", ex.ToString());
            }
        }

        private static void Run()
        {
            Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
            Fez fez = new Fez();
            fez.Run();
            fez.Dispose();
        }

        private static void ApplyArgs(string[] args)
        {
            // -c --clear-save-file
            // where not loading from the correct directory yet
            string dir = Path.Combine(Tas.Directory, "SaveSlot");
            for (int index = -1; index < 3; ++index)
            {
                if (File.Exists(dir + index))
                    File.Delete(dir + index);
            }

            // --force-60hz
            Fez.Force60Hz = true;

            // -st --singlethreaded
            PersistentThreadPool.SingleThreaded = true;

            // -np --no-pause-on-unfocus
            Fez.NoPauseOnUnfocus = true;

            // -ns --no-steamworks
            Fez.NoSteamworks = true;
            PCSaveDevice.DisableCloudSaves = true;

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                switch (arg)
                {
                    // valid original args
                    case "-w":
                    case "--windowed":
                        SettingsManager.Settings.ScreenMode = ScreenMode.Windowed;
                        continue;
                    case "--region":
                        SettingsManager.Settings.Language = (Language)Enum.Parse(typeof(Language), args[++i]);
                        continue;
                    case "--trace":
                        TraceFlags.TraceContentLoad = true;
                        continue;
                    case "--no-lighting":
                        Fez.NoLighting = true;
                        continue;
                    case "--no-music":
                        Fez.NoMusic = true;
                        continue;
                }
            }
        }
    }
}
