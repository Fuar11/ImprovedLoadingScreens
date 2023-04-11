using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Il2Cpp;
using HarmonyLib;
using UnityEngine;
using Random = System.Random;
using LocalizationUtilities;
using Stream = System.IO.Stream;
using StreamReader = System.IO.StreamReader;
using MelonLoader;

namespace ImprovedLoadingScreens
{
    internal class Patches
    {

        private static List<String> allowedTitles = new List<String>();
        private static List<String> allowedHints = new List<String>();

        private static string tempRegion = "";

        //private static List<String> allowedRegionHints;
        //private static List<String> allowedRegionTitles;

        [HarmonyPatch(typeof(Panel_Loading), nameof(Panel_Loading.QueueHintLabel))]
        internal class Panel_Loading_QueueHintLabel
        {
            private static void Prefix(ref string textLocId, ref String titleLocId)
            {

                if (Settings.settings.active == Active.Disabled) return;

                if (Settings.settings.hints && Settings.settings.backgrounds)
                {

                    string region = GetRegion();

                    //if hints are disabled, don't load the list
                    if (!Settings.settings.hints) return;

                    FillGeneralLists();

                    if (region != "" || region != null)
                    {
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

      
        [HarmonyPatch(typeof(Panel_Loading), nameof(Panel_Loading.SetBackgroundData))]

        internal class Panel_Loading_SetBackgroundData
        { 

            private static List<String> allowedBackgrounds = new List<String>();

      /*      private static void Prefix(ref string name)
            {

                allowedBackgrounds.Clear();

                if (Settings.settings.active == Active.Disabled) return;

                if (Settings.settings.regionBackgrounds)
                {

                    MelonLoader.MelonLogger.Msg("Current Region: {0}", GetRegion());

                    switch (GetRegion())
                    {
                        case "LakeRegion":
                            allowedBackgrounds.Add("Ep2_LoadingBackgroundTexture_1");
                            allowedBackgrounds.Add("Ep2_LoadingBackgroundTexture_2");
                            allowedBackgrounds.Add("Ep2_LoadingBackgroundTexture_3");
                            allowedBackgrounds.Add("Ep2_LoadingBackgroundTexture_5");
                            break;
                        case "RuralRegion":
                            allowedBackgrounds.Add("Ep3_LoadingBackgroundTexture_1");
                            allowedBackgrounds.Add("Ep3_LoadingBackgroundTexture_3");
                            allowedBackgrounds.Add("Ep3_LoadingBackgroundTexture_5");
                            allowedBackgrounds.Add("Ep3_LoadingBackgroundTexture_6");
                            allowedBackgrounds.Add("Ep1_LoadingBackgroundTexture_5");
                            break;
                        case "MountainTownRegion":
                            allowedBackgrounds.Add("Ep1_LoadingBackgroundTexture_1");
                            allowedBackgrounds.Add("Ep1_LoadingBackgroundTexture_2");
                            allowedBackgrounds.Add("Ep1_LoadingBackgroundTexture_3");
                            allowedBackgrounds.Add("Ep1_LoadingBackgroundTexture_4");
                            allowedBackgrounds.Add("Ep1_LoadingBackgroundTexture_6");
                            break;
                        case "RiverValleyRegion":
                            allowedBackgrounds.Add("Ep1_LoadingBackgroundTexture_5");
                            break;
                        case "TracksRegion":
                            allowedBackgrounds.Add("Ep2_LoadingBackgroundTexture_4");
                            allowedBackgrounds.Add("Ep2_LoadingBackgroundTexture_6");
                            break;
                        case "AshCanyonRegion":
                            allowedBackgrounds.Add("Ep1_LoadingBackgroundTexture_5");
                            break;
                        default:
                            allowedBackgrounds.Add("LoadingBackgroundTexture_1");
                            allowedBackgrounds.Add("LoadingBackgroundTexture_2");
                            allowedBackgrounds.Add("LoadingBackgroundTexture_3");
                            allowedBackgrounds.Add("LoadingBackgroundTexture_4");
                            allowedBackgrounds.Add("LoadingBackgroundTexture_5");
                            allowedBackgrounds.Add("LoadingBackgroundTexture_6");
                            break;
                    }
                }
                else
                {
                    allowedBackgrounds.Add("LoadingBackgroundTexture_1");
                    allowedBackgrounds.Add("LoadingBackgroundTexture_2");
                    allowedBackgrounds.Add("LoadingBackgroundTexture_3");
                    allowedBackgrounds.Add("LoadingBackgroundTexture_4");
                    allowedBackgrounds.Add("LoadingBackgroundTexture_5");
                    allowedBackgrounds.Add("LoadingBackgroundTexture_6");
                }

                //override the loading screen

                Random rando = new Random();
                int index = rando.Next(0, allowedBackgrounds.Count);

                if (!Settings.settings.backgrounds) index = 999;

                name = allowedBackgrounds.ElementAt(index);

            } */

            private static void Postfix(Panel_Loading __instance)
            {

                allowedBackgrounds.Clear();

                if (Settings.settings.active == Active.Disabled) return;

                if (!Settings.settings.regionBackgrounds) return;

                    string region = GetRegion();

                    switch (region)
                    {
                        case "LakeRegion":
                            allowedBackgrounds.Add("LakeRegion1");
                            allowedBackgrounds.Add("LakeRegion2");
                            allowedBackgrounds.Add("LakeRegion3");
                            allowedBackgrounds.Add("LakeRegion4");
                            allowedBackgrounds.Add("LakeRegion5");
                            allowedBackgrounds.Add("LakeRegion6");
                            break;
                        case "RuralRegion":
                            allowedBackgrounds.Add("RuralRegion1");
                            allowedBackgrounds.Add("RuralRegion2");
                            allowedBackgrounds.Add("RuralRegion3");
                            allowedBackgrounds.Add("RuralRegion4");
                            allowedBackgrounds.Add("RuralRegion5");
                            allowedBackgrounds.Add("RuralRegion6");
                            allowedBackgrounds.Add("RuralRegion7");
                            allowedBackgrounds.Add("RuralRegion8");
                            allowedBackgrounds.Add("RuralRegion9");
                            allowedBackgrounds.Add("RuralRegion10");
                            allowedBackgrounds.Add("RuralRegion11");
                            allowedBackgrounds.Add("RuralRegion12");
                            allowedBackgrounds.Add("RuralRegion13");
                            break;
                        case "MountainTownRegion":
                            allowedBackgrounds.Add("MiltonRegion1");
                            allowedBackgrounds.Add("MiltonRegion2");
                            allowedBackgrounds.Add("MiltonRegion3");
                            allowedBackgrounds.Add("MiltonRegion4");
                            allowedBackgrounds.Add("MiltonRegion5");
                            allowedBackgrounds.Add("MiltonRegion6");
                            allowedBackgrounds.Add("MiltonRegion7");
                            allowedBackgrounds.Add("MiltonRegion8");
                            allowedBackgrounds.Add("MiltonRegion9");
                            allowedBackgrounds.Add("MiltonRegion10");
                            break;
                        case "CoastalRegion":
                            allowedBackgrounds.Add("CoastalRegion1");
                            allowedBackgrounds.Add("CoastalRegion2");
                            allowedBackgrounds.Add("CoastalRegion3");
                            allowedBackgrounds.Add("CoastalRegion4");
                            allowedBackgrounds.Add("CoastalRegion5");
                            allowedBackgrounds.Add("CoastalRegion6");
                            allowedBackgrounds.Add("CoastalRegion7");
                            allowedBackgrounds.Add("CoastalRegion8");
                            break;
                        case "CrashMountainRegion":
                            allowedBackgrounds.Add("CrashMountainRegion1");
                            break;
                        case "MarshRegion":
                            allowedBackgrounds.Add("MarshRegion1");
                            allowedBackgrounds.Add("MarshRegion2");
                            allowedBackgrounds.Add("MarshRegion3");
                            allowedBackgrounds.Add("MarshRegion4");
                            break;
                        case "WhalingStationRegion":
                            allowedBackgrounds.Add("DesolationPoint1");
                            allowedBackgrounds.Add("DesolationPoint2");
                            allowedBackgrounds.Add("DesolationPoint3");
                            allowedBackgrounds.Add("DesolationPoint4");
                            allowedBackgrounds.Add("DesolationPoint5");
                            break;
                        case "LongRailTransitionZone":
                            allowedBackgrounds.Add("FarRange1");
                            allowedBackgrounds.Add("FarRange2");
                            allowedBackgrounds.Add("FarRange3");
                            allowedBackgrounds.Add("FarRange4");
                            allowedBackgrounds.Add("FarRange5");
                            allowedBackgrounds.Add("FarRange6");
                            allowedBackgrounds.Add("FarRange7");
                            allowedBackgrounds.Add("FarRange8");
                            allowedBackgrounds.Add("FarRange9");
                            allowedBackgrounds.Add("FarRange10");
                            break;
                        case "RiverValleyRegion":
                            allowedBackgrounds.Add("RiverValley2");
                            allowedBackgrounds.Add("RiverValley3");
                            allowedBackgrounds.Add("RiverValley4");
                            allowedBackgrounds.Add("RiverValley5");
                            allowedBackgrounds.Add("RiverValley6");
                            allowedBackgrounds.Add("RiverValley7");
                            break;
                        case "TracksRegion":
                            allowedBackgrounds.Add("TracksRegion1");
                            allowedBackgrounds.Add("TracksRegion2");
                            allowedBackgrounds.Add("TracksRegion3");
                            allowedBackgrounds.Add("TracksRegion4");
                            allowedBackgrounds.Add("TracksRegion5");
                            break;
                        case "RavineTransitionZone":
                            allowedBackgrounds.Add("Ravine1");
                            allowedBackgrounds.Add("Ravine2");
                            allowedBackgrounds.Add("Ravine3");
                            allowedBackgrounds.Add("Ravine4");
                            allowedBackgrounds.Add("Ravine5");
                            break;
                        case "AshCanyonRegion":
                            allowedBackgrounds.Add("AshCanyonRegion1");
                            allowedBackgrounds.Add("AshCanyonRegion2");
                            allowedBackgrounds.Add("AshCanyonRegion3");
                            allowedBackgrounds.Add("AshCanyonRegion4");
                            allowedBackgrounds.Add("AshCanyonRegion5");
                            allowedBackgrounds.Add("AshCanyonRegion6");
                            allowedBackgrounds.Add("AshCanyonRegion7");
                            break;
                        case "CanneryRegion":
                            allowedBackgrounds.Add("BleakInlet1");
                            allowedBackgrounds.Add("BleakInlet2");
                            allowedBackgrounds.Add("BleakInlet3");
                            allowedBackgrounds.Add("BleakInlet4");
                            allowedBackgrounds.Add("BleakInlet5");
                            allowedBackgrounds.Add("BleakInlet6");
                            allowedBackgrounds.Add("BleakInlet7");
                            allowedBackgrounds.Add("BleakInlet8");
                            allowedBackgrounds.Add("BleakInlet9");
                            allowedBackgrounds.Add("BleakInlet10");
                            allowedBackgrounds.Add("BleakInlet11");
                            allowedBackgrounds.Add("BleakInlet12");
                            break;
                         case "HubRegion":
                             allowedBackgrounds.Add("HubRegion1");
                             allowedBackgrounds.Add("HubRegion2");
                             allowedBackgrounds.Add("HubRegion3");
                            break;
                        case "DamRiverTransitionZoneB":
                            allowedBackgrounds.Add("WindingRiver1");
                            allowedBackgrounds.Add("WindingRiver2");
                            allowedBackgrounds.Add("WindingRiver3");
                            allowedBackgrounds.Add("WindingRiver4");
                            break;
                        case "AirfieldRegion":
                            allowedBackgrounds.Add("AirfieldRegion1");
                            allowedBackgrounds.Add("AirfieldRegion2");
                            allowedBackgrounds.Add("AirfieldRegion3");
                            allowedBackgrounds.Add("AirfieldRegion4");
                            allowedBackgrounds.Add("AirfieldRegion5");
                            allowedBackgrounds.Add("AirfieldRegion6");
                            allowedBackgrounds.Add("AirfieldRegion7");
                            allowedBackgrounds.Add("AirfieldRegion8");
                            allowedBackgrounds.Add("AirfieldRegion9");
                            allowedBackgrounds.Add("AirfieldRegion10");
                            allowedBackgrounds.Add("AirfieldRegion11");
                            allowedBackgrounds.Add("AirfieldRegion12");
                            allowedBackgrounds.Add("AirfieldRegion13");
                            allowedBackgrounds.Add("AirfieldRegion14");
                            allowedBackgrounds.Add("AirfieldRegion15");
                            allowedBackgrounds.Add("AirfieldRegion16");
                            break;
                        case "BlackrockRegion":
                            allowedBackgrounds.Add("Blackrock1");
                            allowedBackgrounds.Add("Blackrock2");
                            allowedBackgrounds.Add("Blackrock3");
                            allowedBackgrounds.Add("Blackrock4");
                            allowedBackgrounds.Add("Blackrock5");
                            allowedBackgrounds.Add("Blackrock6");

                            allowedBackgrounds.Add("KeepersPass1");
                            allowedBackgrounds.Add("KeepersPass2");
                            allowedBackgrounds.Add("KeepersPass3");
                            allowedBackgrounds.Add("KeepersPass4");
                            break;
                        default:
                                //add region non-specific backgrounds
                            break;
                    }
                
                

                //override the loading screen

                Random rando = new Random();
                int index = rando.Next(0, allowedBackgrounds.Count + 1);

                if (!Settings.settings.backgrounds)
                {
                    index = 999;
                }

                __instance.m_BackgroundTexture.mainTexture = Implementation.BackgroundsAssetBundle.LoadAsset<Texture2D>(allowedBackgrounds.ElementAt(index));
            }

        }

        public static string GetRegion()
        {

            string scene = GameManager.m_ActiveScene;
            MelonLoader.MelonLogger.Msg("Current Scene: {0}", scene);
            List<string> regions = GetAllRegions();

            if (regions.Any(r => scene.Contains(r.ToString())))
            {
                tempRegion = scene;
            }
            else
            {
                if (!regions.Any(r => scene.Contains(r.ToString()))) scene = tempRegion;
            }

            return scene;
        }

        public static List<string> GetAllRegions()
        {
            List<string> names = Enum.GetNames(typeof(GameRegion)).ToList();

            names.Add("LongRailTransitionZone");
            names.Add("RavineTransitionZone");
            names.Add("DamRiverTransitionZoneB");
            names.Add("HubRegion");

            return names;
        }

        public static void FillGeneralLists()
        {

            allowedHints.Clear();
            allowedTitles.Clear();

            //generic

            allowedHints.Add("HINT_FireColdBurnTimes");
            allowedTitles.Add("GAMEPLAY_Fuel");
            allowedHints.Add("HINT_CookingWarmupBuff");
            allowedTitles.Add("HINT_CookingWarmupBuff_title");
            allowedHints.Add("HINT_Moose");
            allowedTitles.Add("HINT_Moose_title");
            allowedHints.Add("HINT_Quartering");
            allowedTitles.Add("HINT_Quartering_title");
            allowedHints.Add("HINT_RawMeatScent");
            allowedTitles.Add("HINT_WolvesPatrol_title");
            allowedHints.Add("HINT_DistressPistol");
            allowedTitles.Add("HINT_DistressPistol_title");
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
            allowedHints.Add("HINT_Temperature");
            allowedTitles.Add("HINT_Temperature_title");
            allowedHints.Add("HINT_Wind");
            allowedTitles.Add("HINT_Wind_title");
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
            allowedHints.Add("HINT_Shivering");
            allowedTitles.Add("HINT_Shivering_title");
            allowedHints.Add("HINT_Revolver");
            allowedTitles.Add("HINT_Revolver_title");
            allowedHints.Add("HINT_SprainPain");
            allowedTitles.Add("HINT_SprainPain_title");
            allowedHints.Add("HINT_BirchTea");
            allowedTitles.Add("HINT_BirchTea_title");

            if (Settings.settings.cHints)
            {
                allowedHints.Add("HINT_Wolves");
                allowedTitles.Add("FUAR_WolvesTitle");
                allowedHints.Add("FUAR_CrowsHint");
                allowedTitles.Add("FUAR_CrowsTitle");
                allowedHints.Add("FUAR_BearCrowsHint");
                allowedTitles.Add("FUAR_BearCrowsTitle");
                allowedHints.Add("FUAR_AuroraHint");
                allowedTitles.Add("FUAR_AuroraTitle");
                allowedHints.Add("FUAR_DawnHint");
                allowedTitles.Add("FUAR_DawnTitle");
                allowedHints.Add("FUAR_CartographyHint");
                allowedTitles.Add("FUAR_CartographyTitle");
                allowedHints.Add("FUAR_RopeHint");
                allowedTitles.Add("FUAR_RopeTitle");
                allowedHints.Add("FUAR_CacheHint");
                allowedTitles.Add("FUAR_CacheTitle");
            }


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

            //add region hints to the general list

            switch (region)
            {
                case "LakeRegion":
                    //add ML hints to main list
                    allowedHints.Add("HINT_LakeRegion");
                    allowedTitles.Add("GAMEPLAY_MysteryLake");
                    allowedHints.Add("HINT_CarterDam");
                    allowedTitles.Add("GAMEPLAY_CarterHydroDam");
                    if (Settings.settings.cHints)
                    {
                        allowedHints.Add("FUAR_LakeRegionLore1");
                        allowedTitles.Add("GAMEPLAY_MysteryLake");
                        allowedHints.Add("FUAR_CarterDamLore1");
                        allowedTitles.Add("GAMEPLAY_CarterHydroDam");
                    }
                    break;
                case "RuralRegion":
                    //add PV hints to main list
                    allowedHints.Add("SUBTITLE_SMFatE32430");
                    allowedTitles.Add("SCENENAME_RuralRegion");
                    allowedHints.Add("HINT_RuralRegion");
                    allowedTitles.Add("SCENENAME_RuralRegion");
                    if (Settings.settings.cHints)
                    {
                        allowedHints.Add("FUAR_RuralRegionHint1");
                        allowedTitles.Add("SCENENAME_RuralRegion");
                        allowedHints.Add("FUAR_RuralRegionLore1");
                        allowedTitles.Add("FUAR_ThompsonCrossing");
                        allowedHints.Add("FUAR_RuralRegionLore2");
                        allowedTitles.Add("GAMEPLAY_RuralRadioTower");
                    }
                    break;
                case "MountainTownRegion":
                    //add Milton hints to main list
                    allowedHints.Add("HINT_MountainTownRegion");
                    allowedTitles.Add("SCENENAME_MountainTownRegion");
                    allowedHints.Add("HINT_MountainTownRegion");
                    allowedTitles.Add("STORY_jnl_LeaveMilton_Title");
                    allowedHints.Add("HINT_MiltonOrigins");
                    allowedTitles.Add("HINT_MiltonOrigins_title");
                    if (Settings.settings.cHints)
                    {
                        allowedHints.Add("FUAR_MountainTownRegionHint1");
                        allowedTitles.Add("SCENENAME_MountainTownRegion");
                        allowedHints.Add("FUAR_MountainTownRegionLore1");
                        allowedTitles.Add("HINT_MiltonOrigins_title");
                        allowedHints.Add("FUAR_MountainTownRegionLore2");
                        allowedTitles.Add("GAMEPLAY_mtTownCentre");
                    }
                    break;
                case "CoastalRegion":
                    //add CH hints to main list
                    allowedHints.Add("GAMEPLAY_CoastalHighwayDescriptionUnlocked");
                    allowedTitles.Add("SCENENAME_CoastalHighway");
                    allowedHints.Add("HINT_LoreMining");
                    allowedTitles.Add("SCENENAME_WorldMap");
                    if (Settings.settings.cHints)
                    {
                        allowedHints.Add("FUAR_CoastalRegionHint1");
                        allowedTitles.Add("SCENENAME_CoastalHighway");
                        allowedHints.Add("FUAR_CoastalRegionLore1");
                        allowedTitles.Add("SCENENAME_CoastalHighway");
                    }
                    break;
                case "CrashMountainRegion":
                    //add TWM hints to main list
                    allowedHints.Add("HINT_CrashMountainRegion");
                    allowedTitles.Add("SCENENAME_CrashMountainRegion");
                    allowedHints.Add("HINT_RegionDifference");
                    allowedTitles.Add("SCENENAME_CrashMountainRegion");
                    if (Settings.settings.cHints)
                    {
                        allowedHints.Add("FUAR_CrashMountainRegionHint1");
                        allowedTitles.Add("SCENENAME_CrashMountainRegion");
                        allowedHints.Add("FUAR_CrashMountainRegionLore1");
                        allowedTitles.Add("SCENENAME_CrashMountainRegion");
                        allowedHints.Add("FUAR_CrashMountainRegionLore2");
                        allowedTitles.Add("SCENENAME_CrashMountainRegion");
                    }
                    break;
                case "MarshRegion":
                    //add FM hints to main list
                    allowedHints.Add("HINT_MarshRegion");
                    allowedTitles.Add("SCENENAME_Marsh");
                    allowedHints.Add("HINT_RegionDifference");
                    allowedTitles.Add("SCENENAME_Marsh");
                    if (Settings.settings.cHints)
                    {
                        allowedHints.Add("FUAR_MarshRegionHint1");
                        allowedTitles.Add("SCENENAME_Marsh");
                        allowedHints.Add("FUAR_MarshRegionLore1");
                        allowedTitles.Add("SCENENAME_Marsh");
                    }
                    break;
                case "WhalingStationRegion":
                    //add DP hints to main list
                    allowedHints.Add("HINT_WhalingStationRegion");
                    allowedTitles.Add("SCENENAME_WhalingStation");
                    allowedHints.Add("HINT_LoreMining");
                    allowedTitles.Add("SCENENAME_WorldMap");
                    if (Settings.settings.cHints)
                    {
                        allowedHints.Add("FUAR_WhalingStationRegionHint1");
                        allowedTitles.Add("SCENENAME_WhalingStation");
                        allowedHints.Add("FUAR_WhalingStationRegionLore1");
                        allowedTitles.Add("SCENENAME_WhalingStation");
                        allowedHints.Add("FUAR_WhalingStationRegionLore2");
                        allowedTitles.Add("SCENENAME_WhalingStation");
                    }
                    break;
                case "RiverValleyRegion":
                    //add HRV hints to main list
                    allowedHints.Add("HINT_RiverValleyRegion");
                    allowedTitles.Add("SCENENAME_RiverValleyRegion");
                    allowedHints.Add("HINT_RegionDifference");
                    allowedTitles.Add("SCENENAME_RiverValleyRegion");
                    if (Settings.settings.cHints)
                    {
                        allowedHints.Add("FUAR_RiverValleyRegionHint1");
                        allowedTitles.Add("SCENENAME_RiverValleyRegion");
                        allowedHints.Add("FUAR_RiverValleyRegionHint2");
                        allowedTitles.Add("SCENENAME_RiverValleyRegion");
                        allowedHints.Add("FUAR_RiverValleyRegionHint3");
                        allowedTitles.Add("FUAR_RiverValleyRegionHint3Title");
                    }
                    break;
                case "TracksRegion":
                    //add BR hints to main list
                    allowedHints.Add("HINT_TracksRegion");
                    allowedTitles.Add("SCENENAME_Railway");
                    allowedHints.Add("HINT_LoreEarthquakes");
                    allowedTitles.Add("SCENENAME_Railway");
                    if (Settings.settings.cHints)
                    {
                        allowedHints.Add("FUAR_TracksRegionHint1");
                        allowedTitles.Add("SCENENAME_Railway");
                        allowedHints.Add("FUAR_TracksRegionLore1");
                        allowedTitles.Add("SCENENAME_Railway");
                    }
                    break;
                case "RavineTransitionRegion":
                    //add Ravine hints to main list
                    allowedHints.Add("HINT_LakeRegion");
                    allowedTitles.Add("GAMEPLAY_MysteryLake");
                    break;
                case "AshCanyonRegion":
                    //add Ravine hints to main list
                    allowedHints.Add("HINT_RegionDifference");
                    allowedTitles.Add("SCENENAME_AshCanyon");
                    if (Settings.settings.cHints)
                    {
                        allowedHints.Add("FUAR_AshCanyonRegionHint1");
                        allowedTitles.Add("SCENENAME_AshCanyon");
                    }
                    break;
                case "CanneryRegion":
                    //add BI hints to main list
                    if (Settings.settings.cHints)
                    {
                        allowedHints.Add("FUAR_BleakInletRegionHint1");
                        allowedTitles.Add("SCENENAME_CanneryRegion");
                        allowedHints.Add("FUAR_BleakInletRegionLore1");
                        allowedTitles.Add("SCENENAME_CanneryRegion");
                    }
                    break;
                case "BlackrockRegion":
                    //add BR hints to main list
                    allowedHints.Add("HINT_BlackrockMountain");
                    allowedTitles.Add("HINT_BlackrockMountain_title");
                    allowedHints.Add("HINT_BlackrockBrokenRoads");
                    allowedTitles.Add("HINT_BlackrockBrokenRoads_title");
                    if (Settings.settings.cHints)
                    {
                        allowedHints.Add("HINT_BlackrockLore1");
                        allowedTitles.Add("STORY_jnl_BlackRock_Title");
                    }
                    break;
                case "LongRailTransitionZone":
                    allowedHints.Add("FUAR_FarRangeBranchLineHint1");
                    allowedTitles.Add("FUAR_FarRangeBranchLineTitle");
                    allowedHints.Add("FUAR_FarRangeBranchLineHint2");
                    allowedTitles.Add("FUAR_FarRangeBranchLineTitle");
                    allowedHints.Add("FUAR_FarTerritoryLore1");
                    allowedTitles.Add("FUAR_FarTerritoryTitle");
                    allowedHints.Add("FUAR_FarRangeLore1");
                    allowedTitles.Add("FUAR_FarRangeTitle");
                    break;
                case "HubRegion":
                    allowedHints.Add("FUAR_FarTerritoryLore1");
                    allowedTitles.Add("FUAR_FarTerritoryTitle");
                    allowedHints.Add("FUAR_FarRangeLore1");
                    allowedTitles.Add("FUAR_FarRangeTitle");
                    allowedHints.Add("FUAR_TransferPassHint");
                    allowedTitles.Add("FUAR_TransferPassTitle");
                    break;
                case "AirfieldRegion":
                    allowedHints.Add("FUAR_FarTerritoryLore1");
                    allowedTitles.Add("FUAR_FarTerritoryTitle");
                    allowedHints.Add("FUAR_FarRangeLore1");
                    allowedTitles.Add("FUAR_FarRangeTitle");
                    allowedHints.Add("FUAR_AirfieldHint1");
                    allowedTitles.Add("FUAR_AirfieldTitle");
                    allowedHints.Add("FUAR_AirfieldHint2");
                    allowedTitles.Add("FUAR_AirfieldTitle");
                    allowedHints.Add("FUAR_AirfieldLore1");
                    allowedTitles.Add("FUAR_AirfieldLoreTitle");
                    allowedHints.Add("FUAR_GlimmerFogHint1");
                    allowedTitles.Add("FUAR_GlimmerFogTitle");
                    allowedHints.Add("FUAR_GlimmerFogHint2");
                    allowedTitles.Add("FUAR_GlimmerFogTitle");
                    allowedHints.Add("FUAR_InsomniaHint");
                    allowedTitles.Add("FUAR_InsomniaTitle");
                    break;
                default:
                    allowedHints.Add("HINT_RegionDifference");
                    allowedTitles.Add("SCENENAME_WorldMap");
                    break;

            }
        }
        public static void LoadLocalizations()
        {
            var JSONfile = "ImprovedLoadingScreens.Localization.json";

            String results = "";

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(JSONfile))
            using (StreamReader reader = new StreamReader(stream))
            {
                results = reader.ReadToEnd();
            }

            LocalizationManager.LoadJsonLocalization(results);

        }
    }
}
