using System.Linq;
using UnityEngine;

namespace UnityEssentials
{
    [RequireComponent(typeof(UIMenuOptionsDataConfigurator))]
    public class GetDisplayResolution : SettingsMenuBase
    {
        [Info]
        [SerializeField]
        private string _info =
            "This component retrieves all available display resolutions from the system and populates the menu options.\n" +
            "It is intended for use with UIMenuOptionsDataConfigurator to allow users to select their preferred screen resolution.";

        public static string[] Options { get; private set; }

        public override void InitializeGetter()
        {
            Options = new string[Screen.resolutions.Length + 1];
            for (int i = 0; i < Screen.resolutions.Length; i++)
            {
                var resolution = Screen.resolutions[i];
                Options[i] = $"{resolution.width}x{resolution.height}";
            }
            Options[^1] = "Native";
            Options = Options.Reverse().ToArray();

            GetComponent<UIMenuOptionsDataConfigurator>().Options = Options;
        }

        public void OnEnable() => SetDisplaySelection.OnDisplaySelectionChanged += InitializeGetter;
        public void OnDisable() => SetDisplaySelection.OnDisplaySelectionChanged -= InitializeGetter;
    }
}