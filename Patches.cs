using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using Random = System.Random;

namespace ImprovedLoadingScreens
{
    internal class Patches
    {

        private static List<String> allowedTitles = new List<String>();
        private static List<String> allowedHints = new List<String>();
        //private static List<String> allowedRegionHints;
        //alprivate static List<String> allowedRegionTitles;
       
        private static String lastRegion = "Default";

        [HarmonyPatch(typeof(GameManager), "Awake")]
        internal class GameManager_Awake
        {
            private static void Postfix()
            {

                //if mod is disabled, skip
                if (Settings.settings.active == Active.Disabled) return;

                //if hints are disabled, don't load the list
                if (!Settings.settings.hints) return;

                //if custom hints are enabled, pull from the JSON file
                if (Settings.settings.cHints)
                {
                    var assembly = Assembly.GetExecutingAssembly();

                    var JSONfile = assembly.GetManifestResourceNames().Single(str => str.EndsWith("Localization.json"));

                    String results = "";

                    using (Stream stream = assembly.GetManifestResourceStream(JSONfile))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        results = reader.ReadToEnd();
                    }

                    MelonLoader.MelonLogger.Msg("JSON Contents: {0}", results);

                    LocalizationUtilities.LocalizationManager.LoadJSONLocalization(results);
                }

                FillGeneralLists();
                MelonLoader.MelonLogger.Msg("Filling general lists");
            }
        }

        [HarmonyPatch(typeof(Panel_Loading), "QueueHintLabel")]
        internal class Panel_Loading_QueueHintLabel
        {
            private static String region = "Default";
           //private static String scene = "Default";

            private static void Prefix(Panel_Loading __instance, ref string textLocId, ref String titleLocId)
            {

                if (Settings.settings.active == Active.Disabled) return;

                //scene = InterfaceManager.GetNameForScene(__instance.m_Info.m_MainScene); unsued for now

                if (Settings.settings.hints)
                {

                    //get the region from LoadingSceneInfo
                    try
                    {
                        region = __instance.m_Info.m_Region;

                        if (region == "")
                        {
                            region = lastRegion;
                        }
                        else
                        {
                            lastRegion = region;
                        }
                    }
                    catch (NullReferenceException e)
                    {
                        MelonLoader.MelonLogger.Msg("Error: Region not found.");
                        MelonLoader.MelonLogger.Msg("Caught Exception: {0}", e.Message);
                    }

                    if (region != "" || region != null)
                    {
                        MelonLoader.MelonLogger.Msg("Region Log: {0} ", region);
                        AddRegionHints(region);
                    }

                    Random rando = new Random();
                    int index = rando.Next(0, allowedHints.Count);

                    //override parameters

                    textLocId = allowedHints.ElementAt(index);
                    titleLocId = allowedTitles.ElementAt(index);
                }
                else
                {
                    textLocId = "";
                    titleLocId = "";
                }
          
            }

        }

        public static void FillGeneralLists()
        {

            allowedHints.Clear();
            allowedTitles.Clear();

            //generic

            allowedHints.Add("HINT_VehiclesRest");
            allowedTitles.Add("GAMEPLAY_Vehicle");
            allowedHints.Add("HINT_VehiclesLoot");
            allowedTitles.Add("HINT_VehiclesLoot_title");
            allowedHints.Add("HINT_VehiclesLoot_2");
            allowedTitles.Add("HINT_VehiclesLoot_title");
            allowedHints.Add("HINT_FireColdBurnTimes");
            allowedTitles.Add("GAMEPLAY_Fuel");
            allowedHints.Add("HINT_CookingWarmupBuff");
            allowedTitles.Add("HINT_CookingWarmupBuff_title");
            allowedHints.Add("HINT_Moose");
            allowedTitles.Add("HINT_Moose_title");
            allowedHints.Add("HINT_Wolves");
            allowedTitles.Add("FUAR_WolvesTitle");
            allowedHints.Add("HINT_Quartering");
            allowedTitles.Add("HINT_Quartering_title");
            allowedHints.Add("HINT_RawMeatScent");
            allowedTitles.Add("HINT_WolvesPatrol_title");
            allowedHints.Add("HINT_DistressPistol");
            allowedTitles.Add("HINT_DistressPistol_title");
            allowedHints.Add("HINT_FlaresTorches");
            allowedTitles.Add("HINT_FlaresTorches_title");
            allowedHints.Add("HINT_CraftingSites");
            allowedTitles.Add("HINT_CraftingSites_title");
            allowedHints.Add("HINT_ToolsRepair");
            allowedTitles.Add("GAMEPLAY_RadialTools");
            allowedHints.Add("HINT_CraftingSnowShelter");
            allowedTitles.Add("GAMEPLAY_RadialSnowShelter");
            allowedHints.Add("HINT_Frostbite");
            allowedTitles.Add("GAMEPLAY_Stat_Frostbite");
            allowedHints.Add("HINT_Hypothermia");
            allowedTitles.Add("GAMEPLAY_Hypothermia");
            allowedHints.Add("HINT_Freezing");
            allowedTitles.Add("GAMEPLAY_CustomModeAdjustFreezingNearbyFire");
            allowedHints.Add("HINT_Starving");
            allowedTitles.Add("GAMEPLAY_Hunger");
            allowedHints.Add("HINT_Dehydration");
            allowedTitles.Add("GAMEPLAY_SymptomDehydration");
            allowedHints.Add("HINT_Exhaustion");
            allowedTitles.Add("GAMEPLAY_SymptomFatigue");
            allowedHints.Add("HINT_Temperature");
            allowedTitles.Add("HINT_Temperature_title");
            allowedHints.Add("HINT_Wind");
            allowedTitles.Add("HINT_Wind_title");
            allowedHints.Add("HINT_WolfDeterrence");
            allowedTitles.Add("HINT_WolfDeterrence_title");
            allowedHints.Add("HINT_WoodHarvest");
            allowedTitles.Add("HINT_WoodHarvest_title");
            allowedHints.Add("HINT_IceFishing");
            allowedTitles.Add("HINT_IceFishing_title");
            allowedHints.Add("HINT_WetFrozenClothing");
            allowedTitles.Add("HINT_WetFrozenClothing_title");
            allowedHints.Add("HINT_MaintainClothing");
            allowedTitles.Add("HINT_MaintainClothing_title");
            allowedHints.Add("HINT_ExposureFrostbite");
            allowedTitles.Add("HINT_ExposureFrostbite_title");
            allowedHints.Add("HINT_ClothingProtection");
            allowedTitles.Add("HINT_ClothingProtection_title");
            allowedHints.Add("HINT_Hatchets");
            allowedTitles.Add("HINT_WoodHarvest_title");
            allowedHints.Add("HINT_HuntingKnife");
            allowedTitles.Add("GAMEPLAY_HuntingKnife");
            allowedHints.Add("HINT_CanOpener");
            allowedTitles.Add("GAMEPLAY_CanOpener");
            allowedHints.Add("HINT_Prybar");
            allowedTitles.Add("GAMEPLAY_Prybar");
            allowedHints.Add("HINT_Lantern");
            allowedTitles.Add("GAMEPLAY_StormLantern");
            allowedHints.Add("HINT_Blizzards");
            allowedTitles.Add("HINT_Blizzards_title");
            allowedHints.Add("HINT_StormFuel");
            allowedTitles.Add("HINT_WoodHarvest_title");
            allowedHints.Add("HINT_Landmarks");
            allowedTitles.Add("HINT_Landmarks_title");
            allowedHints.Add("HINT_BufferMemories");
            allowedTitles.Add("GAMEPLAY_CollectionFilterAuroraScreens");
            allowedHints.Add("HINT_Shivering");
            allowedTitles.Add("HINT_Shivering_title");
            allowedHints.Add("HINT_Revolver");
            allowedTitles.Add("HINT_Revolver_title");
            allowedHints.Add("HINT_SprainPain");
            allowedTitles.Add("HINT_SprainPain_title");
            allowedHints.Add("GAMEPLAY_tutorialFlashlight_short");
            allowedTitles.Add("GAMEPLAY_Flashlight");
            allowedHints.Add("HINT_BirchTea");
            allowedTitles.Add("HINT_BirchTea_title"); 

            /*allowedHints.Add(""); spare lines
            allowedTitles.Add("");
            allowedHints.Add("");
            allowedTitles.Add("");
            allowedHints.Add("");
            allowedTitles.Add("");
            allowedHints.Add("");
            allowedTitles.Add("");
            allowedHints.Add("");
            allowedTitles.Add("");
            allowedHints.Add("");
            allowedTitles.Add("");
            allowedHints.Add("");
            allowedTitles.Add(""); */
        }

        public static void AddRegionHints(String region)
        {

          /*  allowedRegionHints.Add("HINT_LakeRegion"); //ml
            allowedRegionTitles.Add("GAMEPLAY_MysteryLake");
            allowedRegionHints.Add("HINT_MarshRegion");
            allowedRegionTitles.Add("SCENENAME_Marsh"); //fm
            allowedRegionHints.Add("HINT_TracksRegion");
            allowedRegionTitles.Add("SCENENAME_Railway"); //br
            allowedRegionHints.Add("HINT_RuralRegion");
            allowedRegionTitles.Add("SCENENAME_RuralRegion"); //pv
            allowedRegionHints.Add("SUBTITLE_SMFatE32430");
            allowedRegionTitles.Add("SCENENAME_RuralRegion"); //pv
            allowedRegionHints.Add("HINT_WhalingStationRegion");
            allowedRegionTitles.Add("SCENENAME_WhalingStation");
            allowedRegionHints.Add("HINT_CrashMountainRegion");
            allowedRegionTitles.Add("SCENENAME_CrashMountainRegion");
            allowedRegionHints.Add("HINT_MountainTownRegion");
            allowedRegionTitles.Add("SCENENAME_MountainTownRegion"); //Milton
            allowedRegionHints.Add("HINT_MountainTownRegion");
            allowedRegionTitles.Add("STORY_jnl_LeaveMilton_Title"); //Milton
            allowedRegionHints.Add("HINT_RiverValleyRegion");
            allowedRegionTitles.Add("SCENENAME_RiverValleyRegion"); //HRV
            allowedRegionHints.Add("GAMEPLAY_CoastalHighwayDescriptionUnlocked");
            allowedRegionTitles.Add("SCENENAME_CoastalHighway"); //CH */


            //add region hints to the general list

            switch (region)
            {
                case "LakeRegion":
                    //add ML hints to main list
                    allowedHints.Add("HINT_LakeRegion");
                    allowedTitles.Add("GAMEPLAY_MysteryLake");
                    allowedHints.Add("HINT_CarterDam");
                    allowedTitles.Add("GAMEPLAY_CarterHydroDam");
                    allowedHints.Add("FUAR_LakeRegionLore1");
                    allowedTitles.Add("GAMEPLAY_MysteryLake");
                    allowedHints.Add("FUAR_CarterDamLore1");
                    allowedTitles.Add("GAMEPLAY_CarterHydroDam");
                    break;
                case "RuralRegion":
                    //add PV hints to main list
                    allowedHints.Add("SUBTITLE_SMFatE32430");
                    allowedTitles.Add("SCENENAME_RuralRegion");
                    allowedHints.Add("HINT_RuralRegion");
                    allowedTitles.Add("SCENENAME_RuralRegion");
                    allowedHints.Add("FUAR_RuralRegionHint1");
                    allowedTitles.Add("SCENENAME_RuralRegion");
                    allowedHints.Add("FUAR_RuralRegionLore1");
                    allowedTitles.Add("FUAR_ThompsonCrossing");
                    allowedHints.Add("FUAR_RuralRegionLore2");
                    allowedTitles.Add("GAMEPLAY_RuralRadioTower");
                    break;
                case "MountainTownRegion":
                    //add Milton hints to main list
                    allowedHints.Add("HINT_MountainTownRegion");
                    allowedTitles.Add("SCENENAME_MountainTownRegion");
                    allowedHints.Add("HINT_MountainTownRegion");
                    allowedTitles.Add("STORY_jnl_LeaveMilton_Title");
                    allowedHints.Add("HINT_MiltonOrigins");
                    allowedTitles.Add("HINT_MiltonOrigins_title");
                    allowedHints.Add("FUAR_MountainTownRegionHint1");
                    allowedTitles.Add("SCENENAME_MountainTownRegion");
                    allowedHints.Add("FUAR_MountainTownRegionLore1");
                    allowedTitles.Add("HINT_MiltonOrigins_title");
                    allowedHints.Add("FUAR_MountainTownRegionLore2");
                    allowedTitles.Add("GAMEPLAY_mtTownCentre");
                    break;
                case "CoastalRegion":
                    //add CH hints to main list
                    allowedHints.Add("GAMEPLAY_CoastalHighwayDescriptionUnlocked");
                    allowedTitles.Add("SCENENAME_CoastalHighway");
                    allowedHints.Add("HINT_LoreMining");
                    allowedTitles.Add("SCENENAME_WorldMap");
                    allowedHints.Add("FUAR_CoastalRegionHint1");
                    allowedTitles.Add("SCENENAME_CoastalHighway");
                    allowedHints.Add("FUAR_CoastalRegionLore1");
                    allowedTitles.Add("SCENENAME_CoastalHighway");
                    break;
                case "CrashMountainRegion":
                    //add TWM hints to main list
                    allowedHints.Add("HINT_CrashMountainRegion");
                    allowedTitles.Add("SCENENAME_CrashMountainRegion");
                    allowedHints.Add("HINT_RegionDifference");
                    allowedTitles.Add("SCENENAME_CrashMountainRegion");
                    allowedHints.Add("FUAR_CrashMountainRegionHint1");
                    allowedTitles.Add("SCENENAME_CrashMountainRegion");
                    allowedHints.Add("FUAR_CrashMountainRegionLore1");
                    allowedTitles.Add("SCENENAME_CrashMountainRegion");
                    allowedHints.Add("FUAR_CrashMountainRegionLore2");
                    allowedTitles.Add("SCENENAME_CrashMountainRegion");
                    break;
                case "MarshRegion":
                    //add FM hints to main list
                    allowedHints.Add("HINT_MarshRegion");
                    allowedTitles.Add("SCENENAME_Marsh");
                    allowedHints.Add("HINT_RegionDifference");
                    allowedTitles.Add("SCENENAME_Marsh");
                    allowedHints.Add("FUAR_MarshRegionHint1");
                    allowedTitles.Add("SCENENAME_Marsh");
                    allowedHints.Add("FUAR_MarshRegionLore1");
                    allowedTitles.Add("SCENENAME_Marsh");
                    break;
                case "WhalingStationRegion":
                    //add DP hints to main list
                    allowedHints.Add("HINT_WhalingStationRegion");
                    allowedTitles.Add("SCENENAME_WhalingStation");
                    allowedHints.Add("HINT_LoreMining");
                    allowedTitles.Add("SCENENAME_WorldMap");
                    allowedHints.Add("FUAR_WhalingStationRegionHint1");
                    allowedTitles.Add("SCENENAME_WhalingStation");
                    allowedHints.Add("FUAR_WhalingStationRegionLore1");
                    allowedTitles.Add("SCENENAME_WhalingStation");
                    allowedHints.Add("FUAR_WhalingStationRegionLore2");
                    allowedTitles.Add("SCENENAME_WhalingStation");
                    break;
                case "RiverValleyRegion":
                    //add HRV hints to main list
                    allowedHints.Add("HINT_RiverValleyRegion");
                    allowedTitles.Add("SCENENAME_RiverValleyRegion");
                    allowedHints.Add("HINT_RegionDifference");
                    allowedTitles.Add("SCENENAME_RiverValleyRegion");
                    allowedHints.Add("FUAR_RiverValleyRegionHint1");
                    allowedTitles.Add("SCENENAME_RiverValleyRegion");
                    allowedHints.Add("FUAR_RiverValleyRegionHint2");
                    allowedTitles.Add("SCENENAME_RiverValleyRegion");
                    allowedHints.Add("FUAR_RiverValleyRegionHint3");
                    allowedTitles.Add("FUAR_RiverValleyRegionHint3Title");
                    break;
                case "TracksRegion":
                    //add BR hints to main list
                    allowedHints.Add("HINT_TracksRegion");
                    allowedTitles.Add("SCENENAME_Railway");
                    allowedHints.Add("HINT_LoreEarthquakes");
                    allowedTitles.Add("SCENENAME_Railway");
                    allowedHints.Add("FUAR_TracksRegionHint1");
                    allowedTitles.Add("SCENENAME_Railway");
                    allowedHints.Add("FUAR_TracksRegionLore1");
                    allowedTitles.Add("SCENENAME_Railway");
                    break;
                case "TransitionMLtoCH":
                    //add Ravine hints to main list
                    allowedHints.Add("HINT_LakeRegion");
                    allowedTitles.Add("GAMEPLAY_MysteryLake");
                    break;
                case "AshCanyonRegion":
                    //add Ravine hints to main list
                    allowedHints.Add("HINT_RegionDifference");
                    allowedTitles.Add("SCENENAME_AshCanyon");
                    allowedHints.Add("FUAR_AshCanyonRegionHint1");
                    allowedTitles.Add("SCENENAME_AshCanyon");
                    break;
                case "CanneryRegion":
                    //add BI hints to main list
                    allowedHints.Add("FUAR_BleakInletRegionHint1");
                    allowedTitles.Add("SCENENAME_CanneryRegion");
                    allowedHints.Add("FUAR_BleakInletRegionLore1");
                    allowedTitles.Add("SCENENAME_CanneryRegion");
                    break;
                default:
                    allowedHints.Add("HINT_RegionDifference");
                    allowedTitles.Add("SCENENAME_WorldMap");
                    break;

            }



        }

    }
}
