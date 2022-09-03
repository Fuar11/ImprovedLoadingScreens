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
using LocalizationUtilities;

namespace ImprovedLoadingScreens
{
    internal class Patches
    {

        private static List<String> allowedTitles = new List<String>();
        private static List<String> allowedHints = new List<String>();

        private static string tempRegion = "";

        //private static List<String> allowedRegionHints;
        //alprivate static List<String> allowedRegionTitles;

        [HarmonyPatch(typeof(Panel_Loading), "QueueHintLabel")]
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

        [HarmonyPatch(typeof(Utils), "GetLoadingBackgroundTexture")]

        internal class Utils_GetLoadingBackgroundTexture
        {

            private static List<String> allowedBackgrounds = new List<String>();
            private static List<String> allowedCustomBackgrounds = new List<String>();

            private static void Prefix(ref string name)
            {

                allowedBackgrounds.Clear();

                if (Settings.settings.active == Active.Disabled) return;

                if (Settings.settings.regionBackgrounds)
                {
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

            }

            private static void Postfix(ref Texture2D __result)
            {
                MelonLoader.MelonLogger.Msg("Postfix starting.");

                allowedCustomBackgrounds.Clear();

                if (Settings.settings.active == Active.Disabled) return;

                if (!Settings.settings.regionBackgrounds) return;

                    if (Settings.settings.customBackgrounds)
                    {

                    string region = GetRegion();

                    Random choiceSelector = new Random();
                    int customBackgroundChoice = choiceSelector.Next(1, 4);
                    if ((customBackgroundChoice % 2 != 0) && (new[] {"LakeRegion", "RuralRegion", "MountainTownRegion", "RiverValleyRegion", "TracksRegion", "AshCanyonRegion"}.Any(s => region.Contains(s)))) return;

                    switch (region)
                        {
                            case "LakeRegion":
                                allowedCustomBackgrounds.Add("LakeRegion1");
                                allowedCustomBackgrounds.Add("LakeRegion2");
                                allowedCustomBackgrounds.Add("LakeRegion3");
                                allowedCustomBackgrounds.Add("LakeRegion4");
                                allowedCustomBackgrounds.Add("LakeRegion5");
                                break;
                            case "RuralRegion":
                                allowedCustomBackgrounds.Add("RuralRegion1");
                                allowedCustomBackgrounds.Add("RuralRegion2");
                                allowedCustomBackgrounds.Add("RuralRegion3");
                                allowedCustomBackgrounds.Add("RuralRegion4");
                                allowedCustomBackgrounds.Add("RuralRegion5");
                                allowedCustomBackgrounds.Add("RuralRegion6");
                                allowedCustomBackgrounds.Add("RuralRegion7");
                                allowedCustomBackgrounds.Add("RuralRegion8");
                                allowedCustomBackgrounds.Add("RuralRegion9");
                                allowedCustomBackgrounds.Add("RuralRegion10");
                                allowedCustomBackgrounds.Add("RuralRegion11");
                                allowedCustomBackgrounds.Add("RuralRegion12");
                                allowedCustomBackgrounds.Add("RuralRegion13");
                                break;
                            case "MountainTownRegion":
                                allowedCustomBackgrounds.Add("MiltonRegion1");
                                allowedCustomBackgrounds.Add("MiltonRegion2");
                                allowedCustomBackgrounds.Add("MiltonRegion3");
                                allowedCustomBackgrounds.Add("MiltonRegion4");
                                allowedCustomBackgrounds.Add("MiltonRegion5");
                                allowedCustomBackgrounds.Add("MiltonRegion6");
                                allowedCustomBackgrounds.Add("MiltonRegion7");
                                allowedCustomBackgrounds.Add("MiltonRegion8");
                                allowedCustomBackgrounds.Add("MiltonRegion9");
                                allowedCustomBackgrounds.Add("MiltonRegion10");
                                break;
                            case "CoastalRegion":
                                allowedCustomBackgrounds.Add("CoastalRegion1");
                                allowedCustomBackgrounds.Add("CoastalRegion2");
                                allowedCustomBackgrounds.Add("CoastalRegion3");
                                allowedCustomBackgrounds.Add("CoastalRegion4");
                                allowedCustomBackgrounds.Add("CoastalRegion5");
                                allowedCustomBackgrounds.Add("CoastalRegion6");
                                allowedCustomBackgrounds.Add("CoastalRegion7");
                                allowedCustomBackgrounds.Add("CoastalRegion8");
                                break;
                            case "CrashMountainRegion":
                                allowedCustomBackgrounds.Add("CrashMountainRegion1");
                                break;
                            case "MarshRegion":
                                allowedCustomBackgrounds.Add("MarshRegion1");
                                allowedCustomBackgrounds.Add("MarshRegion2");
                                allowedCustomBackgrounds.Add("MarshRegion3");
                                allowedCustomBackgrounds.Add("MarshRegion4");
                                break;
                            case "WhalingStationRegion":
                                allowedCustomBackgrounds.Add("DesolationPoint1");
                                allowedCustomBackgrounds.Add("DesolationPoint2");
                                allowedCustomBackgrounds.Add("DesolationPoint3");
                                allowedCustomBackgrounds.Add("DesolationPoint4");
                                allowedCustomBackgrounds.Add("DesolationPoint5");
                                allowedCustomBackgrounds.Add("DesolationPoint6");
                                break;
                            case "RiverValleyRegion":
                                allowedCustomBackgrounds.Add("RiverValley2");
                                allowedCustomBackgrounds.Add("RiverValley3");
                                allowedCustomBackgrounds.Add("RiverValley4");
                                allowedCustomBackgrounds.Add("RiverValley5");
                                allowedCustomBackgrounds.Add("RiverValley6");
                                allowedCustomBackgrounds.Add("RiverValley7");
                                break;
                            case "TracksRegion":
                                allowedCustomBackgrounds.Add("TracksRegion1");
                                allowedCustomBackgrounds.Add("TracksRegion2");
                                allowedCustomBackgrounds.Add("TracksRegion3");
                                allowedCustomBackgrounds.Add("TracksRegion4");
                                allowedCustomBackgrounds.Add("TracksRegion5");
                                break;
                            case "TransitionMLtoCH":
                                allowedCustomBackgrounds.Add("Ravine1");
                                allowedCustomBackgrounds.Add("Ravine2");
                                allowedCustomBackgrounds.Add("Ravine3");
                                allowedCustomBackgrounds.Add("Ravine4");
                                allowedCustomBackgrounds.Add("Ravine5");
                                break;
                            case "AshCanyonRegion":
                                allowedCustomBackgrounds.Add("AshCanyonRegion1");
                                allowedCustomBackgrounds.Add("AshCanyonRegion2");
                                allowedCustomBackgrounds.Add("AshCanyonRegion3");
                                allowedCustomBackgrounds.Add("AshCanyonRegion4");
                                allowedCustomBackgrounds.Add("AshCanyonRegion5");
                                allowedCustomBackgrounds.Add("AshCanyonRegion6");
                                allowedCustomBackgrounds.Add("AshCanyonRegion7");
                                break;
                            case "CanneryRegion":
                                allowedCustomBackgrounds.Add("BleakInlet1");
                                allowedCustomBackgrounds.Add("BleakInlet2");
                                allowedCustomBackgrounds.Add("BleakInlet3");
                                allowedCustomBackgrounds.Add("BleakInlet4");
                                allowedCustomBackgrounds.Add("BleakInlet5");
                                allowedCustomBackgrounds.Add("BleakInlet6");
                                allowedCustomBackgrounds.Add("BleakInlet7");
                                allowedCustomBackgrounds.Add("BleakInlet8");
                                allowedCustomBackgrounds.Add("BleakInlet9");
                                allowedCustomBackgrounds.Add("BleakInlet10");
                                allowedCustomBackgrounds.Add("BleakInlet11");
                                allowedCustomBackgrounds.Add("BleakInlet12");
                                break;
                            case "BlackrockRegion":
                                allowedCustomBackgrounds.Add("Blackrock1");
                                allowedCustomBackgrounds.Add("Blackrock2");
                                allowedCustomBackgrounds.Add("Blackrock3");
                                allowedCustomBackgrounds.Add("Blackrock4");
                                allowedCustomBackgrounds.Add("Blackrock5");
                                allowedCustomBackgrounds.Add("Blackrock6");

                                allowedCustomBackgrounds.Add("KeepersPass1"); //temporary
                                allowedCustomBackgrounds.Add("KeepersPass2");
                                allowedCustomBackgrounds.Add("KeepersPass3");
                                allowedCustomBackgrounds.Add("KeepersPass4");
                                break;
                            default:
                                break;
                        }
                    }
                else
                {
                    return;
                }
                
                //override the loading screen

                Random rando = new Random();
                int index = rando.Next(0, allowedCustomBackgrounds.Count);

                if (!Settings.settings.backgrounds)
                {
                    index = 999;
                }
                
                __result = Implementation.BackgroundsAssetBundle.LoadAsset<Texture2D>(allowedCustomBackgrounds.ElementAt(index));
            }

        }

        public static string GetRegion()
        {

            string scene = GameManager.m_ActiveScene;
            string region = "";

          
            if (RegionManager.SceneIsRegion(scene))
            {
                region = scene;
                tempRegion = scene;
        
            }
            else
            {

                MelonLoader.MelonLogger.Msg("SCENE NAME IF NOT REGION: {0}", scene);

                region = InterfaceManager.GetLocIDForScene(scene);

                List<GameRegion> regions = GetAllRegions();

                if (!regions.Any(r => region.Contains(r.ToString()))) region = tempRegion;
            }

            /*MelonLoader.MelonLogger.Msg("SCENE NAME: {0}", scene);
            MelonLoader.MelonLogger.Msg("REGION NAME: {0}", region); */

            return region;
        }

        public static List<GameRegion> GetAllRegions()
        {
            List<GameRegion> list = new List<GameRegion>();
            string[] names = Enum.GetNames(typeof(GameRegion));
            for (int i = 0; i < names.Length; i++)
            {
                if (i != 6 && i != 7)
                {
                    list.Add((GameRegion)i);
                }
            }
            return list;

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

            if (Settings.settings.cHints)
            {
                allowedHints.Add("HINT_Wolves");
                allowedTitles.Add("FUAR_WolvesTitle");
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
                case "TransitionMLtoCH":
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
            LocalizationUtilities.LocalizationManager.LoadJSONLocalization(results);

            MelonLoader.MelonLogger.Msg("Localizations Added: {0}", LocalizationUtilities.LocalizationManager.LoadJSONLocalization(results));


        }

    }

}
