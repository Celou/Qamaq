using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class scrDamageSaturation : MonoBehaviour {

    [Header("PostProcessFX")]
    private float Saturation = 1;
    public PostProcessingProfile PPstack;
    public PostProcessingProfile PPstackB;
    public BloomModel.Settings bloomSettings;
    public ColorGradingModel.Settings colorSettings;
    public ColorGradingModel.Settings colorSettingsB;
    public Camera CamB;
    float Satu = 1;
    float RotAmp = 0;
    float RotFreq = 0;
    float ShutAngle = 0;
    float FrameBlending = 0;

    [Header("Camera")]
    public Camera mainCamera;

    private void Start()
    {
        PPstackB.colorGrading.enabled = false;
    }

    private void Update()
    {
        DamageDesaturationUpdate();
    }

    void DamageDesaturationUpdate()

    {
        if (GetComponent<scrPlayerLife>().currentLife <= (GetComponent<scrPlayerLife>().currentMaxLife) / 2.5f)
        {
            PPstackB.colorGrading.enabled = false;

            PPstack = mainCamera.GetComponent<PostProcessingBehaviour>().profile;
            PPstackB = CamB.GetComponent<PostProcessingBehaviour>().profile;

            PPstack.colorGrading.settings = colorSettings;


            PPstack.colorGrading.enabled = true;



            Satu = (GetComponent<scrPlayerLife>().currentLife / 40) + 0.3f;

            if (Satu <= 0.5f)
            {
                Satu = 0.5f;
            }

            colorSettings.basic.saturation = Satu;



            mainCamera.gameObject.GetComponent<Klak.Motion.BrownianMotion>().rotationFrequency = RotFreq;
            mainCamera.gameObject.GetComponent<Klak.Motion.BrownianMotion>().rotationAmplitude = RotAmp;
            mainCamera.gameObject.GetComponent<Kino.Motion>().shutterAngle = ShutAngle;
            mainCamera.gameObject.GetComponent<Kino.Motion>().frameBlending = FrameBlending;

            RotFreq = (1 / GetComponent<scrPlayerLife>().currentLife) + 0.05f;

            RotAmp = (1 / GetComponent<scrPlayerLife>().currentLife) + 0.02f;

            ShutAngle = 1500 * (1 / GetComponent<scrPlayerLife>().currentLife);
            FrameBlending = (1 - (1 / GetComponent<scrPlayerLife>().currentLife)) + 0.1f;

            if (ShutAngle >= 180)
            {
                ShutAngle = 180;
            }

            if (ShutAngle <= 60)
            {
                ShutAngle = 0;
            }


            if (RotFreq >= 0.15f)
            {
                RotFreq = 0.15f;
            }

            if (RotFreq >= 0.15f)
            {
                RotFreq = 0.15f;
            }

            if (RotFreq <= 0.08f)
            {
                RotFreq = 0;
            }

            if (RotAmp >= 0.11f)
            {
                RotAmp = 0.11f;
            }

            if (RotAmp <= 0.08f)
            {
                RotAmp = 0;
            }

            if (FrameBlending >= 0.5f)
            {
                FrameBlending = 0.5f;
            }

            if (FrameBlending <= 0.2f)
            {
                FrameBlending = 0;
            }

        }
        else
        {
            mainCamera.gameObject.GetComponent<Klak.Motion.BrownianMotion>().rotationFrequency = 0;
            mainCamera.gameObject.GetComponent<Klak.Motion.BrownianMotion>().rotationAmplitude = 0;
            mainCamera.gameObject.GetComponent<Kino.Motion>().shutterAngle = 0;
            mainCamera.gameObject.GetComponent<Kino.Motion>().frameBlending = 0;
            Satu = 1.3f;
            PPstack.colorGrading.enabled = false;
            PPstackB.colorGrading.enabled = true;

        }

    }
}
