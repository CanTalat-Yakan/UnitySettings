using UnityEngine;

namespace UnityEssentials
{
    public class SetAspectRatio : SettingsMenuBase
    {
        [Info]
        [SerializeField]
        private string _info =
            "This component sets the aspect ratio of the camera render texture based on the settings profile.\n" +
            "It listens for changes in the aspect ratio setting and applies the selected aspect ratio to the camera render texture handler.";

        [field: Space]
        [field: ReadOnly]
        [field: SerializeField] 
        public Vector2 AspectRatio { get; private set; }

        private const string AspectRatioReference = "aspect_ratio";

        public override void InitializeSetter(UIMenuProfile profile, out string reference) =>
            AspectRatio = GetAspectRatio.Options[profile.Get<int>(reference = AspectRatioReference)]
                .ExtractVector2FromString(':');

        public CameraRenderTextureHandler RenderTextureHandler => _renderTextureHandler ??= CameraProvider.Active?.GetComponent<CameraRenderTextureHandler>();
        private CameraRenderTextureHandler _renderTextureHandler;

        public void Update()
        {
            if (RenderTextureHandler == null)
                return;

            var aspectRatioNumerator = Mathf.Max(0, AspectRatio.x);
            var aspectRatioDenominator = Mathf.Max(0, AspectRatio.y);

            RenderTextureHandler.Settings.AspectRatioNumerator = aspectRatioNumerator;
            RenderTextureHandler.Settings.AspectRatioDenominator = aspectRatioDenominator;
        }
    }
}