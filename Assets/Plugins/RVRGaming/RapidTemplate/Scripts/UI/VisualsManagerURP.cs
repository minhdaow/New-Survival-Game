using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class VisualsManagerURP : MonoBehaviour
{
    public VolumeProfile urpProfile;
    public Toggle motionBlurToggle;
    public Toggle filmGrainToggle;
    public Toggle chromaticAberrationToggle;

    public Button resetButton;

    private MotionBlur motionBlur;
    private FilmGrain filmGrain;
    private ChromaticAberration chromaticAberration;

    void Start()
    {
        if (urpProfile != null)
        {
            urpProfile.TryGet(out motionBlur);
            urpProfile.TryGet(out filmGrain);
            urpProfile.TryGet(out chromaticAberration);
        }

        if (motionBlur != null)
            motionBlurToggle.isOn = motionBlur.active;
        if (filmGrain != null)
            filmGrainToggle.isOn = filmGrain.active;
        if (chromaticAberration != null)
            chromaticAberrationToggle.isOn = chromaticAberration.active;

        motionBlurToggle.onValueChanged.AddListener(SetMotionBlur);
        filmGrainToggle.onValueChanged.AddListener(SetFilmGrain);
        chromaticAberrationToggle.onValueChanged.AddListener(SetChromaticAberration);

        resetButton.onClick.AddListener(ResetValues);
    }

    void SetMotionBlur(bool isOn)
    {
        if (motionBlur != null)
        {
            motionBlur.active = isOn;
        }
    }

    void SetFilmGrain(bool isOn)
    {
        if (filmGrain != null)
        {
            filmGrain.active = isOn;
        }
    }

    void SetChromaticAberration(bool isOn)
    {
        if (chromaticAberration != null)
        {
            chromaticAberration.active = isOn;
        }
    }

    void ResetValues()
    {
        if (motionBlur != null)
        {
            motionBlur.active = true;
            motionBlurToggle.isOn = true;
        }
        if (filmGrain != null)
        {
            filmGrain.active = true;
            filmGrainToggle.isOn = true;
        }
        if (chromaticAberration != null)
        {
            chromaticAberration.active = true;
            chromaticAberrationToggle.isOn = true;
        }
    }
}
