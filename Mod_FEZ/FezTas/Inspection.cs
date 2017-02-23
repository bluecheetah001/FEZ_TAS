using FezEngine.Services;
using FezEngine.Tools;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FezTas
{
    public static class Inspection
    {
        public static GameComponentCollection Components { get; private set; }
        public static IContentManagerProvider CMProvider { get; private set; }

        public static void Initialize(Game game)
        {
            Components = game.Components;
            CMProvider = GetService<IContentManagerProvider>();
        }

        public static T GetService<T>() where T : class
        {
            return ServiceHelper.Get<T>();
        }

        public static T GetComponent<T>() where T : class
        {
            foreach (IGameComponent obj in Components)
            {
                if (obj is T)
                {
                    return obj as T;
                }
            }
            return null;
        }

        public static List<T> GetComponents<T>() where T : class
        {
            List<T> elems = new List<T>();
            foreach (IGameComponent obj in Components)
            {
                if (obj is T)
                {
                    elems.Add(obj as T);
                }
            }
            return elems;
        }

        public static T Load<T>(string value)
        {
            return CMProvider.Global.Load<T>(value);
        }
    }
}
