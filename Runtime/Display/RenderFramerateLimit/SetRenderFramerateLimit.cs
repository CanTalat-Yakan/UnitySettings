using UnityEngine;

namespace UnityEssentials
{
    public class SetRenderFrameRateLimit : SettingsMenuBase
    {
        [Info]
        [SerializeField]
        private string _info =
            "SetRenderFrameRateLimit is responsible for managing the frame rate limit for rendering cameras. " +
            "It allows the user to set a specific frame rate limit through the settings menu, " +
            "ensuring that the game runs at a consistent frame rate as defined by the user.";

        [field: Space]
        [field: ReadOnly]
        [field: SerializeField] 
        public int RenderFrameRateLimit { get; private set; }

        private const string RenderFrameRateLimitReference = "render_framerate_limit";

        public override void InitializeSetter(UIMenuProfile profile, out string reference) =>
            RenderFrameRateLimit = profile.Get<int>(reference = RenderFrameRateLimitReference);

        public CameraFrameRateLimiter CameraFrameRateLimiter => _cameraFrameRateLimiter ??= CameraProvider.Active?.GetComponent<CameraFrameRateLimiter>();
        private CameraFrameRateLimiter _cameraFrameRateLimiter;

        public void Update()
        {
            if (RenderFrameRateLimit <= 0)
                RenderFrameRateLimit = (int)GetScreenFrameRate();

            CameraFrameRateLimiter?.SetTargetFrameRate(RenderFrameRateLimit);
        }

        private float GetScreenFrameRate() =>
            Screen.currentResolution.refreshRateRatio.numerator / Screen.currentResolution.refreshRateRatio.denominator;
    }
}