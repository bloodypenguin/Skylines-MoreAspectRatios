using ICities;
using MoreAspectRatios.Redirection;
using Object = UnityEngine.Object;

namespace MoreAspectRatios
{
    public class MoreAspectRatios : IUserMod
    {
       private static bool _bootstrapped;

        public string Name
        {
            get
            {
                if (_bootstrapped)
                {
                    return "More Aspect Ratios";
                }
                Bootstrap();
                _bootstrapped = true;
                return "More Aspect Ratios";
            }
        }

        public string Description => "Adds support for non-standard aspect ratios to graphics options";

        private static void Bootstrap()
        {
            Redirector<OptionsGraphicsPanelDetour>.Deploy();
            var panel = Object.FindObjectOfType<OptionsGraphicsPanel>();
            if (panel == null)
            {
                return;
            }
            OptionsGraphicsPanelDetour.InitAspectRatios(panel);
            OptionsGraphicsPanelDetour.InitResolutions(panel);
            OptionsGraphicsPanelDetour.InitDisplayModes(panel);
        }
    }
}
