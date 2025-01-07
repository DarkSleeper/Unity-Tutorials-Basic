using UnityEngine;
using TMPro;

public class FrameRateCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI display;

    [SerializeField, Range(0.1f, 2f)]
    float sampleDuration = 1f;    

    public enum DisplayMode { FPS, MS }

    [SerializeField]
    DisplayMode displayMode = DisplayMode.FPS;

    int frames = 0;

    float duration = 0f;
    float bestDuration = float.MaxValue;
    float worstDuration = 0f;

    void Update() {
        var frameDuration = Time.unscaledDeltaTime;
        frames++;
        duration += frameDuration;

        if (frameDuration < bestDuration) bestDuration = frameDuration;
        if (frameDuration > worstDuration) worstDuration = frameDuration;

        if (duration > sampleDuration) {
            if (displayMode == DisplayMode.FPS) {
                display.SetText(
                    "FPS\n{0:1}\n{1:1}\n{2:1}",
                    1f / bestDuration,
                    frames / duration,
                    1f / worstDuration
                );
            }
            else {
                display.SetText(
                    "MS\n{0:1}\n{1:1}\n{2:1}",
                    1000f * bestDuration,
                    1000f * duration / frames,
                    1000f * worstDuration
                );
            }
            frames = 0;
            duration = 0f;
            bestDuration = float.MaxValue;
            worstDuration = 0f;
        }
    }
}
