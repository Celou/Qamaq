using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class scrPlayer : MonoBehaviour
{
    [Header("Heal")]
    public bool healUnlocked;
    public float cooldownHeal;
    float currentCooldownHeal;
    public int healAmount;

    [Header("Camera")]
    public Camera mainCamera;
    public float cameraZoomSpeed;
    Vector3 mousePos;

    [Header("Zone à effet")]
    public GameObject ZoneEffect;

    [Header("HoldingObject")]
    public bool holdingObject;
    public GameObject HoldedObject;
    public float holdingObjectDelay;
    public GameObject LittleRock;

    [Header("PlayerDamage")]
    public float basicPlayerDamage;
    public float currentPlayerDamage;

    [Header("Red Mask Damages Feedback")]
    public GameObject redMask;

    #region sounds
    [Header("Sounds")]
    public AudioSource attackSound1;
    public AudioSource attackSound2;
    public AudioSource attackSound3;
    public AudioSource finalAttackSound;
    public AudioSource attackHitSound;
    public AudioSource finalAttackHitSound;

    public AudioSource FootStepSound1;
    public AudioSource FootStepSound2;
    public AudioSource FootStepSound3;
    public AudioSource FootStepSound4;
    public AudioSource FootStepSound5;

    public AudioSource hurtSound1;
    public AudioSource hurtSound2;
    public AudioSource hurtSound3;

    public AudioSource globeMouvementSound1;
    public AudioSource globeMouvementSound2;
    public AudioSource globeMouvementSound3;

    public AudioSource yamtaLightningCreationSound;

    public AudioSource protectionSphereCreationSound;
    public AudioSource protectionSphereMaintenanceSound;
    public AudioSource protectionSphereEndSound;
    public AudioSource protectionSphereBreakSound;

    public AudioSource totemInvocationSound;

    public AudioSource boostActivationSound;
    public AudioSource boostMaintenanceSound;
    public AudioSource boostEndSound;
    #endregion

    #region PostProcess
    [Header("PostProcessFX")]

    private float Saturation = 1;
    public PostProcessingProfile PPstack;
    public PostProcessingProfile PPstackB;
    public BloomModel.Settings bloomSettings;
    public ColorGradingModel.Settings colorSettings;
    public ColorGradingModel.Settings colorSettingsB;
    public Camera CamB;
	float Satu = 1;
	float RotAmp = 0 ;
	float RotFreq = 0;
	float ShutAngle = 0;
	float FrameBlending = 0;
    #endregion


    void Start()
    {
        PPstackB.colorGrading.enabled = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        DamageUpdate();

        DamageDesaturationUpdate();
    }

    public void CheckIfPlayerAbleToAct()
    {

    }

    void DamageUpdate()
    {

        currentPlayerDamage = (basicPlayerDamage) * scrCompetenceManager.CompetenceManager.currentDamageBoostAmount;

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



            Satu = (GetComponent<scrPlayerLife>().currentLife / 40)+0.3f;

			if (Satu <= 0.5f) 
			{
				Satu = 0.5f;
			}

            colorSettings.basic.saturation = Satu;



			mainCamera.gameObject.GetComponent<Klak.Motion.BrownianMotion> ().rotationFrequency = RotFreq;
			mainCamera.gameObject.GetComponent<Klak.Motion.BrownianMotion> ().rotationAmplitude = RotAmp;
			mainCamera.gameObject.GetComponent<Kino.Motion> ().shutterAngle = ShutAngle;
			mainCamera.gameObject.GetComponent<Kino.Motion> ().frameBlending = FrameBlending;

			RotFreq = (1 / GetComponent<scrPlayerLife>().currentLife) + 0.05f;

			RotAmp = (1/ GetComponent<scrPlayerLife>().currentLife) + 0.02f;

			ShutAngle = 1500 * (1/ GetComponent<scrPlayerLife>().currentLife);
			FrameBlending =(1 - (1 / GetComponent<scrPlayerLife>().currentLife))+0.1f;

			if (ShutAngle >= 180) 
			{
				ShutAngle = 180 ;
			}

			if (ShutAngle <= 60) 
			{
				ShutAngle = 0 ;
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
				FrameBlending = 0.5f ;
			}

			if (FrameBlending <= 0.2f) 
			{
				FrameBlending = 0 ;
			}

        }
        else
        {
			mainCamera.gameObject.GetComponent<Klak.Motion.BrownianMotion> ().rotationFrequency = 0;
			mainCamera.gameObject.GetComponent<Klak.Motion.BrownianMotion> ().rotationAmplitude = 0;
			mainCamera.gameObject.GetComponent<Kino.Motion> ().shutterAngle = 0;
			mainCamera.gameObject.GetComponent<Kino.Motion> ().frameBlending = 0;
            Satu = 1.3f;
            PPstack.colorGrading.enabled = false;
            PPstackB.colorGrading.enabled = true;
		
        }

    }

}

