using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrExperienceManager : MonoBehaviour
{

    [Header("Basic Properties")]
    public static scrExperienceManager ExperienceManager;
    public float currentExperience;
    //float currentFloatExperience;
    public bool inCompetenceMenu;
    float openingDelayTime;
    public GameObject Player;
    public Canvas menuCanvas;
    public Text menuText;

    [Header("Costs")]
    public float firstLevelCompCost;
    public float secondLevelCompCost;

    [Header("Locks")]
    public bool yamtaLightningUnclocked;
    public bool baseBoostUnlocked;
    public bool protectionSphereUnlocked;
    public bool totemUnclocked;
    public bool vendettaSphereUnlocked;
    public bool heavenlyLightningUnlocked;


    [Header("Buttons")]
    public Button Lightning;
    public Button BaseBoost;
    public Button BaseShield;
    public Button TauntingSummoning;
    public Button VendettaSphere;
    public Button HeavenlyLightning;

    [Header("Buttons Color")]
    public float globalColorChangeTime;
    public float currentGlobalColorChangeTime;
    public Color lockedColor;
    public Color unlockableColor;
    bool colorSwitch;
    public Color currentColor;

    [Header("Sounds")]
    public AudioSource competenceMenuAmbianceSound;
    public AudioSource competenceMenuOpeningSound;
    public AudioSource competenceMenClosingSound;
    public AudioSource competenceBuyingSound;
    public AudioSource competenceBuyableMouseOnSound;
    public AudioSource competenceEquipementSound;
    public AudioSource competenceBuyRefuseSound;

    [Header("TreeFill")]
    public GameObject CompetenceTree;
    public GameObject CentralCircle;
    public Image CompetenceTreeFillDefChoiceFirstPart;
    public Image CompetenceTreeFillDefChoiceLeftCircle;
    public Image CompetenceTreeFillDefChoiceSecondPartLeft;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.RightControl))
        //    InterfaceShake(0.5f, 5);

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            currentExperience += 10;

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
            currentExperience -= 10;


        if (Input.GetKeyDown(KeyCode.Tab) && !inCompetenceMenu && openingDelayTime == 0)
        {

            inCompetenceMenu = true;
            transform.localPosition = new Vector3(0, 0, 40);
            openingDelayTime = 0.5f;
            CompetenceTree.transform.localPosition = new Vector3(0, 0, 50);
            competenceMenuOpeningSound.Play();

        }

        if (Input.GetKeyDown(KeyCode.Tab) && inCompetenceMenu && openingDelayTime == 0)
        {

            inCompetenceMenu = false;
            transform.localPosition = new Vector3(0, 1080, 40);
            openingDelayTime = 0.5f;
            CompetenceTree.transform.localPosition = new Vector3(0, 1080, 50);
            competenceMenClosingSound.Play();

        }

        if (openingDelayTime > 0)
            openingDelayTime -= Time.deltaTime;
        if (openingDelayTime < 0)
            openingDelayTime = 0;

        ExperienceManager = this;

        AttackBranch();
        ShieldBranch();
        BoostBranch();
        SummoningBranch();

        if (inCompetenceMenu)
        {
            CompetenceMenu();
            if (!competenceMenuAmbianceSound.isPlaying)
                competenceMenuAmbianceSound.Play();
        }
        else
        {
            if (competenceMenuAmbianceSound.isPlaying)
                competenceMenuAmbianceSound.Stop();
        }


        Fill();

        if (inCompetenceMenu)
            ButtonsColorUpdate();

    }

    public void UnlockLightning()
    {

        if (yamtaLightningUnclocked == false && currentExperience >= firstLevelCompCost)
        {

            yamtaLightningUnclocked = true;
            currentExperience -= firstLevelCompCost;
            Lightning.interactable = false;
            competenceBuyingSound.Play();

        }

    }


    public void UnlockBaseBoost()
    {

        if (baseBoostUnlocked == false && currentExperience >= firstLevelCompCost)
        {

            baseBoostUnlocked = true;
            currentExperience -= firstLevelCompCost;
            BaseBoost.interactable = false;
            competenceBuyingSound.Play();
        }

    }

    public void UnlockBaseShield()
    {

        if (protectionSphereUnlocked == false && currentExperience >= firstLevelCompCost)
        {

            protectionSphereUnlocked = true;
            currentExperience -= firstLevelCompCost;
            BaseShield.interactable = false;
            competenceBuyingSound.Play();
        }

    }

    public void UnlockTauntingSummon()
    {

        if (totemUnclocked == false && currentExperience >= firstLevelCompCost)
        {

            totemUnclocked = true;
            currentExperience -= firstLevelCompCost;
            TauntingSummoning.interactable = false;
            competenceBuyingSound.Play();
        }

    }

    public void UnlockVendettaSphere()
    {

        if (protectionSphereUnlocked == true && vendettaSphereUnlocked == false && currentExperience >= secondLevelCompCost)
        {

            vendettaSphereUnlocked = true;
            currentExperience -= secondLevelCompCost;
            VendettaSphere.interactable = false;
            competenceBuyingSound.Play();
        }

    }

    public void UnlockHeavenlyLightning()
    {

        if (yamtaLightningUnclocked == true && heavenlyLightningUnlocked == false && currentExperience >= secondLevelCompCost)
        {

            heavenlyLightningUnlocked = true;
            currentExperience -= secondLevelCompCost;
            HeavenlyLightning.interactable = false;
            competenceBuyingSound.Play();
        }

    }

    public void ButtonsColorUpdate()
    {

        currentColor.r = Mathf.Lerp(lockedColor.r, unlockableColor.r, currentGlobalColorChangeTime);
        currentColor.g = Mathf.Lerp(lockedColor.g, unlockableColor.g, currentGlobalColorChangeTime);
        currentColor.b = Mathf.Lerp(lockedColor.b, unlockableColor.b, currentGlobalColorChangeTime);
        currentColor.a = 255;

        if (currentGlobalColorChangeTime > 0.4f)
            colorSwitch = true;
        if (currentGlobalColorChangeTime < 0)
            colorSwitch = false;


        if (colorSwitch)
        {


            currentGlobalColorChangeTime -= 0.2f * Time.deltaTime;


        }
        else
        {

            currentGlobalColorChangeTime += 0.2f * Time.deltaTime;

        }



    }

    public void Fill()
    {
        //currentFloatExperience = currentExperience;

        if (!baseBoostUnlocked || !protectionSphereUnlocked || !totemUnclocked || !yamtaLightningUnclocked)
        {

            if (currentExperience <= firstLevelCompCost)
            {
                //ici, la valeur en flotant correspond à la taille que l'objet qui remplit l'arbre doit avoir pour atteindre la compétence suivante. 
                //Ainsi, quel que soit le coup du niveau de compétence, la progression se fera toujours progressivement jusqu'à atteindre la compétence suivante, 
                //à partir du moment où la quantité d'experience st suffisante pour la débloquer
                CentralCircle.transform.localScale = new Vector3(currentExperience * 0.68f / firstLevelCompCost, currentExperience * 0.68f / firstLevelCompCost, 1);
            }
            else
                CentralCircle.transform.localScale = new Vector3(0.68f, 0.68f, 1);
        }

        if (!protectionSphereUnlocked && currentExperience >= firstLevelCompCost)
        {

            CompetenceTreeFillDefChoiceFirstPart.fillAmount = (currentExperience - firstLevelCompCost) / secondLevelCompCost * 2.5f;

            if (currentExperience - firstLevelCompCost <= 0.5f * secondLevelCompCost)
                CompetenceTreeFillDefChoiceLeftCircle.fillAmount = (currentExperience - firstLevelCompCost - 0.4f * secondLevelCompCost) / secondLevelCompCost * 0.25f * 10;
            else
                CompetenceTreeFillDefChoiceLeftCircle.fillAmount = 0.25f;

            CompetenceTreeFillDefChoiceSecondPartLeft.fillAmount = (currentExperience - firstLevelCompCost - 0.5f * secondLevelCompCost) / secondLevelCompCost * 2;

        }
        if (protectionSphereUnlocked)
        {

            CompetenceTreeFillDefChoiceFirstPart.fillAmount = currentExperience - firstLevelCompCost / secondLevelCompCost * 2.5f;

            if (currentExperience <= 0.5f * secondLevelCompCost)
                CompetenceTreeFillDefChoiceLeftCircle.fillAmount = (currentExperience - 0.4f * secondLevelCompCost) / secondLevelCompCost * 0.25f * 10;

            CompetenceTreeFillDefChoiceSecondPartLeft.fillAmount = (currentExperience - 0.5f * secondLevelCompCost) / secondLevelCompCost * 2;

        }



    }

    public void InterfaceShake(float shakingDuration, float shakingAmount)
    {
        GetComponent<Screenshake>().shakeDuration = shakingDuration;
        GetComponent<Screenshake>().shakeAmount = shakingAmount;
        CompetenceTree.GetComponent<Screenshake>().shakeDuration = shakingDuration;
        CompetenceTree.GetComponent<Screenshake>().shakeAmount = shakingAmount;
    }

    void CompetenceMenu()
    {



    }

    void AttackBranch()
    {



    }

    void ShieldBranch()
    {



    }

    void BoostBranch()
    {



    }

    void SummoningBranch()
    {



    }
}
