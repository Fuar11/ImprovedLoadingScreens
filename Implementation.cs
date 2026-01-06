using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;
using LocalizationUtilities;

namespace ImprovedLoadingScreens
{
    public class Implementation : MelonMod
    {

        private static AssetBundle? assetBundle;

        internal static AssetBundle BackgroundsAssetBundle
        {
            get => assetBundle ?? throw new System.NullReferenceException(nameof(assetBundle));
        }

        public override void OnInitializeMelon()
        {
            Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
            Settings.onLoad();

            assetBundle = LoadAssetBundleFromStream("ImprovedLoadingScreens.improvedloadingscreens");
            //GetAssetNames(assetBundle);

            Patches.LoadLocalizations();

        }


        public static AssetBundle LoadAssetBundleFromStream(string path)
        {
            using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            {
                MemoryStream? memory = new((int)stream.Length);
                stream!.CopyTo(memory);

                Il2CppSystem.IO.MemoryStream memoryStream = new Il2CppSystem.IO.MemoryStream(memory.ToArray());

                AssetBundle loadFromMemoryInternal = AssetBundle.LoadFromStream(memoryStream);
                return loadFromMemoryInternal;
            }
        }
       
    }
}
