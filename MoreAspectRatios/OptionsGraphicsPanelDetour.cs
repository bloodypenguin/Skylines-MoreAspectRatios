using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using ColossalFramework.UI;
using MoreAspectRatios.Redirection;
using UnityEngine;

namespace MoreAspectRatios
{
    [TargetType(typeof(OptionsGraphicsPanel))]
    public class OptionsGraphicsPanelDetour : OptionsGraphicsPanel
    {

        private static readonly float[] kAvailableAspectRatios = new float[]
        {
            1.25f,
            1.333333f,
            1.5f,
            1.777778f,
            1.6f,
            2f,
            2.333333f,
            2.4f,
            3.555556f,
            3.2f
        };

        private static readonly string[] kAvailableAspectRatioNames = new string[]
        {
            "Square (5:4)",
            "ASPECTRATIO_NORMAL",
            "Classic (3:2)",
            "ASPECTRATIO_WIDESCREEN",
            "ASPECTRATIO_WIDESCREEN2",
            "Widescreen (2:1)",
            "ASPECTRATIO_WIDESCREEN3",
            "Widescreen (12:5)",
            "Widescreen (32:9)",
            "SuperUltraWide (32:10)"
        };

        [RedirectMethod]
        public static void InitAspectRatios(OptionsGraphicsPanel panel)
        {
            var mSupportedAspectRatios =
                (List<float>)
                    typeof(OptionsGraphicsPanel).GetField("m_SupportedAspectRatios",
                        BindingFlags.NonPublic | BindingFlags.Instance)
                        .GetValue(panel);
            mSupportedAspectRatios.Clear();
            var resolutions = Screen.resolutions;
            var list = new List<string>();
            for (int index1 = 0; index1 < OptionsGraphicsPanelDetour.kAvailableAspectRatios.Length; ++index1)
            {
                for (int index2 = 0; index2 < resolutions.Length; ++index2)
                {
                    Resolution resolution = resolutions[index2];
                    if (MatchAspectRatio(panel, resolution.width, resolution.height,
                        OptionsGraphicsPanelDetour.kAvailableAspectRatios[index1]))
                    {
                        mSupportedAspectRatios.Add(OptionsGraphicsPanelDetour.kAvailableAspectRatios[index1]);
                        list.Add(OptionsGraphicsPanelDetour.kAvailableAspectRatioNames[index1]);
                        break;
                    }
                }
            }
            if (mSupportedAspectRatios.Count == 0)
            {
                mSupportedAspectRatios.Add(OptionsGraphicsPanelDetour.kAvailableAspectRatios[0]);
                list.Add(OptionsGraphicsPanelDetour.kAvailableAspectRatioNames[0]);
            }
            var mAspectRatioDropdown =
                (UIDropDown)
                    typeof(OptionsGraphicsPanel).GetField("m_AspectRatioDropdown",
                        BindingFlags.NonPublic | BindingFlags.Instance).GetValue(panel);
            mAspectRatioDropdown.localizedItems = list.ToArray();
            mAspectRatioDropdown.selectedIndex = FindCurrentAspectRatio(panel);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [RedirectReverse]
        public static void InitResolutions(OptionsGraphicsPanel panel)
        {
            UnityEngine.Debug.Log("InitResolutions");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [RedirectReverse]
        public static void InitDisplayModes(OptionsGraphicsPanel panel)
        {
            UnityEngine.Debug.Log("InitDisplayModes");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [RedirectReverse]
        private static bool MatchAspectRatio(OptionsGraphicsPanel panel, int width, int height, float aspect)
        {
            UnityEngine.Debug.Log("MatchAspectRatio");
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        [RedirectReverse]
        private static int FindCurrentAspectRatio(OptionsGraphicsPanel panel)
        {
            UnityEngine.Debug.Log("FindCurrentAspectRatio");
            return 0;
        }
    }
}