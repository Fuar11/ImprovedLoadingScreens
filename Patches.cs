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

        private static List<Tuple<string, string>> hintsAndTitles = new List<Tuple<string, string>>();

        private static string tempRegion = "";

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
                    int index = rando.Next(0, hintsAndTitles.Count);

                    //override parameters

                    textLocId = hintsAndTitles[index].Item2;
                    titleLocId = hintsAndTitles[index].Item1;
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
                        allowedBackgrounds.Add("CrashMountainRegion2");
                        allowedBackgrounds.Add("CrashMountainRegion3");
                        allowedBackgrounds.Add("CrashMountainRegion4");
                        allowedBackgrounds.Add("CrashMountainRegion5");
                        allowedBackgrounds.Add("CrashMountainRegion6");
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
                    case "MiningRegion":
                        allowedBackgrounds.Add("MiningRegion1");
                        allowedBackgrounds.Add("MiningRegion2");
                        allowedBackgrounds.Add("MiningRegion3");
                        allowedBackgrounds.Add("MiningRegion4");
                        allowedBackgrounds.Add("MiningRegion5");
                        allowedBackgrounds.Add("MiningRegion6");
                        allowedBackgrounds.Add("MiningRegion7");
                        allowedBackgrounds.Add("MiningRegion8");
                        allowedBackgrounds.Add("MiningRegion9");
                        allowedBackgrounds.Add("MiningRegion10");
                        allowedBackgrounds.Add("MiningRegion11");
                        allowedBackgrounds.Add("MiningRegion12");
                        allowedBackgrounds.Add("MiningRegion13");
                        allowedBackgrounds.Add("MiningRegion14");
                        allowedBackgrounds.Add("MiningRegion15");
                        allowedBackgrounds.Add("MiningRegion16");
                        allowedBackgrounds.Add("MiningRegion17");
                        allowedBackgrounds.Add("MiningRegion18");
                        allowedBackgrounds.Add("MiningRegion19");
                        allowedBackgrounds.Add("MiningRegion20");
                        allowedBackgrounds.Add("MiningRegion21");
                        allowedBackgrounds.Add("MiningRegion22");
                        allowedBackgrounds.Add("MiningRegion23");
                        allowedBackgrounds.Add("MiningRegion24");
                        break;
                    case "MountainPassRegion":
                        allowedBackgrounds.Add("SunderedPassRegion1");
                        allowedBackgrounds.Add("SunderedPassRegion2");
                        allowedBackgrounds.Add("SunderedPassRegion3");
                        allowedBackgrounds.Add("SunderedPassRegion4");
                        allowedBackgrounds.Add("SunderedPassRegion5");
                        allowedBackgrounds.Add("SunderedPassRegion6");
                        allowedBackgrounds.Add("SunderedPassRegion7");
                        allowedBackgrounds.Add("SunderedPassRegion8");
                        allowedBackgrounds.Add("SunderedPassRegion9");
                        allowedBackgrounds.Add("SunderedPassRegion10");
                        allowedBackgrounds.Add("SunderedPassRegion11");
                        allowedBackgrounds.Add("SunderedPassRegion12");
                        allowedBackgrounds.Add("SunderedPassRegion13");
                        allowedBackgrounds.Add("SunderedPassRegion14");
                        allowedBackgrounds.Add("SunderedPassRegion15");
                        allowedBackgrounds.Add("SunderedPassRegion16");
                        allowedBackgrounds.Add("SunderedPassRegion17");
                        allowedBackgrounds.Add("SunderedPassRegion18");
                        allowedBackgrounds.Add("SunderedPassRegion19");
                        allowedBackgrounds.Add("SunderedPassRegion20");
                        allowedBackgrounds.Add("SunderedPassRegion21");
                        allowedBackgrounds.Add("SunderedPassRegion22");
                        allowedBackgrounds.Add("SunderedPassRegion23");
                        allowedBackgrounds.Add("SunderedPassRegion24");
                        allowedBackgrounds.Add("SunderedPassRegion25");
                        allowedBackgrounds.Add("SunderedPassRegion26");
                        allowedBackgrounds.Add("SunderedPassRegion27");
                        allowedBackgrounds.Add("SunderedPassRegion28");
                        allowedBackgrounds.Add("SunderedPassRegion29");
                        allowedBackgrounds.Add("SunderedPassRegion30");
                        break;
                    case "ModPrecariousCauseway":
                        allowedBackgrounds.Add("precariouscauseway1");
                        allowedBackgrounds.Add("precariouscauseway2");
                        allowedBackgrounds.Add("precariouscauseway3");
                        allowedBackgrounds.Add("precariouscauseway4");
                        allowedBackgrounds.Add("precariouscauseway5");
                        allowedBackgrounds.Add("precariouscauseway6");
                        allowedBackgrounds.Add("precariouscauseway7");
                        break;
                    case "ModRockyThoroughfare":
                        allowedBackgrounds.Add("rockythoroughfare1");
                        allowedBackgrounds.Add("rockythoroughfare2");
                        allowedBackgrounds.Add("rockythoroughfare3");
                        allowedBackgrounds.Add("rockythoroughfare4");
                        allowedBackgrounds.Add("rockythoroughfare5");
                        allowedBackgrounds.Add("rockythoroughfare6");
                        allowedBackgrounds.Add("rockythoroughfare7");
                        allowedBackgrounds.Add("rockythoroughfare8");
                        break;
                    case "ModShatteredMarsh":
                        allowedBackgrounds.Add("shatteredmarsh1");
                        allowedBackgrounds.Add("shatteredmarsh2");
                        allowedBackgrounds.Add("shatteredmarsh3");
                        allowedBackgrounds.Add("shatteredmarsh4");
                        allowedBackgrounds.Add("shatteredmarsh5");
                        allowedBackgrounds.Add("shatteredmarsh6");
                        allowedBackgrounds.Add("shatteredmarsh7");
                        allowedBackgrounds.Add("shatteredmarsh8");
                        allowedBackgrounds.Add("shatteredmarsh9");
                        allowedBackgrounds.Add("shatteredmarsh10");
                        allowedBackgrounds.Add("shatteredmarsh11");
                        allowedBackgrounds.Add("shatteredmarsh12");
                        allowedBackgrounds.Add("shatteredmarsh13");
                        break;
                    case "ModForsakenShore":
                        allowedBackgrounds.Add("forsakenshore1");
                        allowedBackgrounds.Add("forsakenshore2");
                        allowedBackgrounds.Add("forsakenshore3");
                        allowedBackgrounds.Add("forsakenshore4");
                        allowedBackgrounds.Add("forsakenshore5");
                        allowedBackgrounds.Add("forsakenshore6");
                        allowedBackgrounds.Add("forsakenshore7");
                        allowedBackgrounds.Add("forsakenshore8");
                        allowedBackgrounds.Add("forsakenshore9");
                        allowedBackgrounds.Add("forsakenshore10");
                        allowedBackgrounds.Add("forsakenshore11");
                        allowedBackgrounds.Add("forsakenshore12");
                        allowedBackgrounds.Add("forsakenshore13");
                        break;
                    default:
                        //add region non-specific backgrounds
                        break;
                }

                //override the loading screen

                Random rando = new Random();
                int index = rando.Next(0, allowedBackgrounds.Count);

                __instance.m_BackgroundTexture.mainTexture = (Settings.settings.backgrounds && allowedBackgrounds.Count > 0) ? Implementation.BackgroundsAssetBundle.LoadAsset<Texture2D>(allowedBackgrounds.ElementAt(index)) : null;
            }

        }

        public static string GetRegion()
        {

            string scene = GameManager.m_ActiveScene;
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
            names.Add("MountainPassRegion");

            //TLDev
            names.Add("ModPrecariousCauseway");
            names.Add("ModShatteredMarsh");
            names.Add("ModForsakenShore");
            names.Add("ModRockyThoroughfare");

            return names;
        }

        public static void FillGeneralLists()
        {

            //refactor

            hintsAndTitles.Clear();

            if (Settings.settings.bHints)
            {
                hintsAndTitles.Add(Tuple.Create("HINT_WolvesPatrol_title", "HINT_RawMeatScent"));
                hintsAndTitles.Add(Tuple.Create("HINT_CookingWarmupBuff_title", "HINT_CookingWarmupBuff"));
                hintsAndTitles.Add(Tuple.Create("GAMEPLAY_RadialTools", "HINT_ToolsRepair"));
                hintsAndTitles.Add(Tuple.Create("GAMEPLAY_Stat_Frostbite", "HINT_Frostbite"));
                hintsAndTitles.Add(Tuple.Create("GAMEPLAY_Hypothermia", "HINT_Hypothermia"));
                hintsAndTitles.Add(Tuple.Create("GAMEPLAY_CustomModeAdjustFreezingNearbyFire", "HINT_Freezing"));
                hintsAndTitles.Add(Tuple.Create("HINT_Temperature_title", "HINT_Temperature"));
                hintsAndTitles.Add(Tuple.Create("HINT_Wind_title", "HINT_Wind"));
                hintsAndTitles.Add(Tuple.Create("HINT_WoodHarvest_title", "HINT_WoodHarvest"));
                hintsAndTitles.Add(Tuple.Create("HINT_IceFishing_title", "HINT_IceFishing"));
                hintsAndTitles.Add(Tuple.Create("HINT_WetFrozenClothing_title", "HINT_WetFrozenClothing"));
                hintsAndTitles.Add(Tuple.Create("HINT_MaintainClothing_title", "HINT_MaintainClothing"));
                hintsAndTitles.Add(Tuple.Create("HINT_ExposureFrostbite_title", "HINT_ExposureFrostbite"));
                hintsAndTitles.Add(Tuple.Create("HINT_ClothingProtection_title", "HINT_ClothingProtection"));
            }

            hintsAndTitles.Add(Tuple.Create("GAMEPLAY_Fuel", "HINT_FireColdBurnTimes"));
            hintsAndTitles.Add(Tuple.Create("HINT_Moose_title", "HINT_Moose"));
            hintsAndTitles.Add(Tuple.Create("HINT_Quartering_title", "HINT_Quartering"));
            hintsAndTitles.Add(Tuple.Create("HINT_DistressPistol_title", "HINT_DistressPistol"));
            hintsAndTitles.Add(Tuple.Create("GAMEPLAY_RadialSnowShelter", "HINT_CraftingSnowShelter"));
            hintsAndTitles.Add(Tuple.Create("HINT_WoodHarvest_title", "HINT_Hatchets"));
            hintsAndTitles.Add(Tuple.Create("GAMEPLAY_HuntingKnife", "HINT_HuntingKnife"));
            hintsAndTitles.Add(Tuple.Create("GAMEPLAY_CanOpener", "HINT_CanOpener"));
            hintsAndTitles.Add(Tuple.Create("GAMEPLAY_Prybar", "HINT_Prybar"));
            hintsAndTitles.Add(Tuple.Create("GAMEPLAY_StormLantern", "HINT_Lantern"));
            hintsAndTitles.Add(Tuple.Create("HINT_Blizzards_title", "HINT_Blizzards"));
            hintsAndTitles.Add(Tuple.Create("HINT_WoodHarvest_title", "HINT_StormFuel"));
            hintsAndTitles.Add(Tuple.Create("HINT_Landmarks_title", "HINT_Landmarks"));
            hintsAndTitles.Add(Tuple.Create("HINT_Shivering_title", "HINT_Shivering"));
            hintsAndTitles.Add(Tuple.Create("HINT_Revolver_title", "HINT_Revolver"));
            hintsAndTitles.Add(Tuple.Create("HINT_BirchTea_title", "HINT_BirchTea"));
            hintsAndTitles.Add(Tuple.Create("FUAR_CrowsTitle", "FUAR_CrowsHint"));
            hintsAndTitles.Add(Tuple.Create("FUAR_BearCrowsTitle", "FUAR_BearCrowsHint"));
            hintsAndTitles.Add(Tuple.Create("FUAR_AuroraTitle", "FUAR_AuroraHint"));
            hintsAndTitles.Add(Tuple.Create("FUAR_DawnTitle", "FUAR_DawnHint"));
            hintsAndTitles.Add(Tuple.Create("FUAR_CartographyTitle", "FUAR_CartographyHint"));
            hintsAndTitles.Add(Tuple.Create("FUAR_RopeTitle", "FUAR_RopeHint"));
            hintsAndTitles.Add(Tuple.Create("FUAR_CacheTitle", "FUAR_CacheHint"));
            hintsAndTitles.Add(Tuple.Create("FUAR_TravoisTitle", "FUAR_TravoisHint1"));
            hintsAndTitles.Add(Tuple.Create("FUAR_TravoisTitle", "FUAR_TravoisHint2"));
            hintsAndTitles.Add(Tuple.Create("FUAR_InsulatedFlaskTitle", "FUAR_InsulatedFlaskHint1"));
            hintsAndTitles.Add(Tuple.Create("FUAR_CarcassHarvestTitle", "FUAR_CarcassHarvestHint1"));
            hintsAndTitles.Add(Tuple.Create("FUAR_PtarmiganTitle", "FUAR_PtarmiganHint1"));

        }
        public static void AddRegionHints(String region)
        {

            //add region hints to the general list

            switch (region)
            {
                case "LakeRegion":
                    AddRegionToHintsAndTitles("GAMEPLAY_MysteryLake", "HINT_LakeRegion");
                    AddRegionToHintsAndTitles("GAMEPLAY_CarterHydroDam", "HINT_CarterDam");

                    AddRegionToHintsAndTitles("GAMEPLAY_MysteryLake", "FUAR_LakeRegionLore1");
                    AddRegionToHintsAndTitles("GAMEPLAY_CarterHydroDam", "FUAR_CarterDamLore1");

                    break;
                case "RuralRegion":
                    AddRegionToHintsAndTitles("SCENENAME_RuralRegion", "SUBTITLE_SMFatE32430");
                    AddRegionToHintsAndTitles("SCENENAME_RuralRegion", "HINT_RuralRegion");

                    AddRegionToHintsAndTitles("SCENENAME_RuralRegion", "FUAR_RuralRegionHint1");
                    AddRegionToHintsAndTitles("FUAR_ThompsonCrossing", "FUAR_RuralRegionLore1");
                    AddRegionToHintsAndTitles("GAMEPLAY_RuralRadioTower", "FUAR_RuralRegionLore2");

                    break;
                case "MountainTownRegion":
                    AddRegionToHintsAndTitles("SCENENAME_MountainTownRegion", "HINT_MountainTownRegion");
                    AddRegionToHintsAndTitles("STORY_jnl_LeaveMilton_Title", "HINT_MountainTownRegion");
                    AddRegionToHintsAndTitles("HINT_MiltonOrigins_title", "HINT_MiltonOrigins");

                    AddRegionToHintsAndTitles("SCENENAME_MountainTownRegion", "FUAR_MountainTownRegionHint1");
                    AddRegionToHintsAndTitles("HINT_MiltonOrigins_title", "FUAR_MountainTownRegionLore1");
                    AddRegionToHintsAndTitles("GAMEPLAY_mtTownCentre", "FUAR_MountainTownRegionLore2");

                    break;
                case "CoastalRegion":
                    AddRegionToHintsAndTitles("SCENENAME_CoastalHighway", "GAMEPLAY_CoastalHighwayDescriptionUnlocked");
                    AddRegionToHintsAndTitles("SCENENAME_WorldMap", "HINT_LoreMining");

                    AddRegionToHintsAndTitles("SCENENAME_CoastalHighway", "FUAR_CoastalRegionHint1");
                    AddRegionToHintsAndTitles("SCENENAME_CoastalHighway", "FUAR_CoastalRegionLore1");

                    break;
                case "CrashMountainRegion":
                    AddRegionToHintsAndTitles("SCENENAME_CrashMountainRegion", "HINT_CrashMountainRegion");
                    AddRegionToHintsAndTitles("SCENENAME_CrashMountainRegion", "HINT_RegionDifference");

                    AddRegionToHintsAndTitles("SCENENAME_CrashMountainRegion", "FUAR_CrashMountainRegionHint1");
                    AddRegionToHintsAndTitles("SCENENAME_CrashMountainRegion", "FUAR_CrashMountainRegionLore1");
                    AddRegionToHintsAndTitles("SCENENAME_CrashMountainRegion", "FUAR_CrashMountainRegionLore2");

                    break;
                case "MarshRegion":
                    AddRegionToHintsAndTitles("SCENENAME_Marsh", "HINT_MarshRegion");
                    AddRegionToHintsAndTitles("SCENENAME_Marsh", "HINT_RegionDifference");

                    AddRegionToHintsAndTitles("SCENENAME_Marsh", "FUAR_MarshRegionHint1");
                    AddRegionToHintsAndTitles("SCENENAME_Marsh", "FUAR_MarshRegionLore1");

                    break;
                case "WhalingStationRegion":
                    AddRegionToHintsAndTitles("SCENENAME_WhalingStation", "HINT_WhalingStationRegion");
                    AddRegionToHintsAndTitles("SCENENAME_WorldMap", "HINT_LoreMining");

                    AddRegionToHintsAndTitles("SCENENAME_WhalingStation", "FUAR_WhalingStationRegionHint1");
                    AddRegionToHintsAndTitles("SCENENAME_WhalingStation", "FUAR_WhalingStationRegionLore1");
                    AddRegionToHintsAndTitles("SCENENAME_WhalingStation", "FUAR_WhalingStationRegionLore2");

                    break;
                case "RiverValleyRegion":
                    AddRegionToHintsAndTitles("SCENENAME_RiverValleyRegion", "HINT_RiverValleyRegion");
                    AddRegionToHintsAndTitles("SCENENAME_RiverValleyRegion", "HINT_RegionDifference");

                    AddRegionToHintsAndTitles("SCENENAME_RiverValleyRegion", "FUAR_RiverValleyRegionHint1");
                    AddRegionToHintsAndTitles("SCENENAME_RiverValleyRegion", "FUAR_RiverValleyRegionHint2");
                    AddRegionToHintsAndTitles("FUAR_RiverValleyRegionHint3Title", "FUAR_RiverValleyRegionHint3");

                    break;
                case "TracksRegion":
                    AddRegionToHintsAndTitles("SCENENAME_Railway", "HINT_TracksRegion");
                    AddRegionToHintsAndTitles("SCENENAME_Railway", "HINT_LoreEarthquakes");

                    AddRegionToHintsAndTitles("SCENENAME_Railway", "FUAR_TracksRegionHint1");
                    AddRegionToHintsAndTitles("SCENENAME_Railway", "FUAR_TracksRegionLore1");

                    break;
                case "RavineTransitionRegion":
                    AddRegionToHintsAndTitles("GAMEPLAY_MysteryLake", "HINT_LakeRegion");
                    break;
                case "AshCanyonRegion":
                    AddRegionToHintsAndTitles("SCENENAME_AshCanyon", "HINT_RegionDifference");

                    AddRegionToHintsAndTitles("SCENENAME_AshCanyon", "FUAR_AshCanyonRegionHint1");

                    break;
                case "CanneryRegion":

                    AddRegionToHintsAndTitles("SCENENAME_CanneryRegion", "FUAR_BleakInletRegionHint1");
                    AddRegionToHintsAndTitles("SCENENAME_CanneryRegion", "FUAR_BleakInletRegionLore1");

                    break;
                case "BlackrockRegion":
                    AddRegionToHintsAndTitles("HINT_BlackrockMountain_title", "HINT_BlackrockMountain");
                    AddRegionToHintsAndTitles("HINT_BlackrockBrokenRoads_title", "HINT_BlackrockBrokenRoads");

                    AddRegionToHintsAndTitles("STORY_jnl_BlackRock_Title", "HINT_BlackrockLore1");

                    break;
                case "LongRailTransitionZone":
                    AddRegionToHintsAndTitles("FUAR_FarRangeBranchLineTitle", "FUAR_FarRangeBranchLineHint1");
                    AddRegionToHintsAndTitles("FUAR_FarRangeBranchLineTitle", "FUAR_FarRangeBranchLineHint2");
                    AddRegionToHintsAndTitles("FUAR_FarTerritoryTitle", "FUAR_FarTerritoryLore1");
                    AddRegionToHintsAndTitles("FUAR_FarRangeTitle", "FUAR_FarRangeLore1");
                    break;
                case "HubRegion":
                    AddRegionToHintsAndTitles("FUAR_FarTerritoryTitle", "FUAR_FarTerritoryLore1");
                    AddRegionToHintsAndTitles("FUAR_FarRangeTitle", "FUAR_FarRangeLore1");
                    AddRegionToHintsAndTitles("FUAR_TransferPassTitle", "FUAR_TransferPassHint");
                    break;
                case "AirfieldRegion":
                    AddRegionToHintsAndTitles("FUAR_FarTerritoryTitle", "FUAR_FarTerritoryLore1");
                    AddRegionToHintsAndTitles("FUAR_FarRangeTitle", "FUAR_FarRangeLore1");
                    AddRegionToHintsAndTitles("FUAR_AirfieldTitle", "FUAR_AirfieldHint1");
                    AddRegionToHintsAndTitles("FUAR_AirfieldTitle", "FUAR_AirfieldHint2");
                    AddRegionToHintsAndTitles("FUAR_AirfieldLoreTitle", "FUAR_AirfieldLore1");
                    AddRegionToHintsAndTitles("FUAR_GlimmerFogTitle", "FUAR_GlimmerFogHint1");
                    AddRegionToHintsAndTitles("FUAR_GlimmerFogTitle", "FUAR_GlimmerFogHint2");
                    AddRegionToHintsAndTitles("FUAR_InsomniaTitle", "FUAR_InsomniaHint");
                    break;
                case "MiningRegion":
                    AddRegionToHintsAndTitles("FUAR_FarTerritoryTitle", "FUAR_FarTerritoryLore1");
                    AddRegionToHintsAndTitles("FUAR_FarRangeTitle", "FUAR_FarRangeLore1");
                    AddRegionToHintsAndTitles("FUAR_GlimmerFogTitle", "FUAR_GlimmerFogHint1");
                    AddRegionToHintsAndTitles("FUAR_GlimmerFogTitle", "FUAR_GlimmerFogHint2");
                    AddRegionToHintsAndTitles("FUAR_InsomniaTitle", "FUAR_InsomniaHint");
                    AddRegionToHintsAndTitles("FUAR_ZCTitle", "FUAR_ZCHint1");
                    AddRegionToHintsAndTitles("FUAR_ZCTitle", "FUAR_ZCHint2");
                    AddRegionToHintsAndTitles("FUAR_LoreTitle1", "FUAR_ZCLoreHint1");
                    AddRegionToHintsAndTitles("FUAR_LoreTitle1", "FUAR_ZCHint2");
                    AddRegionToHintsAndTitles("FUAR_ZCTitle", "FUAR_PoisonWolfHint");
                    AddRegionToHintsAndTitles("FUAR_LoreTitle1", "FUAR_PoisonWolfHint");
                    AddRegionToHintsAndTitles("FUAR_RespiratorTitle", "FUAR_RespiratorHint1");
                    AddRegionToHintsAndTitles("FUAR_LoreTitle1", "FUAR_RespiratorHint1");
                    AddRegionToHintsAndTitles("FUAR_RespiratorTitle", "FUAR_RespiratorHint2");
                    break;
                case "MountainPassRegion":
                    AddRegionToHintsAndTitles("FUAR_FarTerritoryTitle", "FUAR_FarTerritoryLore1");
                    AddRegionToHintsAndTitles("FUAR_FarRangeTitle", "FUAR_FarRangeLore1");
                    AddRegionToHintsAndTitles("FUAR_GlimmerFogTitle", "FUAR_GlimmerFogHint1");
                    AddRegionToHintsAndTitles("FUAR_GlimmerFogTitle", "FUAR_GlimmerFogHint2");
                    AddRegionToHintsAndTitles("FUAR_SPTitle", "FUAR_SPHint1");
                    AddRegionToHintsAndTitles("FUAR_SPTitle", "FUAR_SPHint2");
                    AddRegionToHintsAndTitles("FUAR_SPTitle", "FUAR_SPHint3");
                    break;
                //TLDev
                case "ModPrecariousCauseway":
                    AddRegionToHintsAndTitles("FUAR_TLDevTitle", "FUAR_TLDevLore1");
                    AddRegionToHintsAndTitles("FUAR_PCTitle", "FUAR_PCHint1");
                    AddRegionToHintsAndTitles("FUAR_PCTitle", "FUAR_PCHint2");
                    break;
                case "ModRockyThoroughfare":
                    AddRegionToHintsAndTitles("FUAR_TLDevTitle", "FUAR_TLDevLore1");
                    AddRegionToHintsAndTitles("FUAR_RTTitle", "FUAR_RTHint1");
                    break;
                case "ModShatteredMarsh":
                    AddRegionToHintsAndTitles("FUAR_TLDevTitle", "FUAR_TLDevLore1");
                    AddRegionToHintsAndTitles("FUAR_SMTitle", "FUAR_SMHint1");
                    AddRegionToHintsAndTitles("FUAR_SMTitle", "FUAR_SMHint2");
                    break;
                case "ModForsakenShore":
                    AddRegionToHintsAndTitles("FUAR_TLDevTitle", "FUAR_TLDevLore1");
                    AddRegionToHintsAndTitles("FUAR_FSTitle", "FUAR_FSHint1");
                    AddRegionToHintsAndTitles("FUAR_FSTitle", "FUAR_FSHint2");
                    AddRegionToHintsAndTitles("FUAR_FSTitle", "FUAR_FSHint3");
                    break;
                default:
                    AddRegionToHintsAndTitles("SCENENAME_WorldMap", "HINT_RegionDifference");
                    break;
            }
        }

        static void AddRegionToHintsAndTitles(string title, string hint)
        {
            hintsAndTitles.Add(Tuple.Create(title, hint));
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
