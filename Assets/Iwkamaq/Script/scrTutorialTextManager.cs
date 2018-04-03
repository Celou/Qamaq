using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrTutorialTextManager : MonoBehaviour {

    public static scrTutorialTextManager TutorialTextManager;

    [Header("Basical Parameters")]
    public bool gameIsStopped;
    public bool textOnScreen;
    public int currentDialogue;
    //public int currentDialogueNumberOfLine;
    public int numberOfDialogue;
    public Text dialogueText;

    [Header("First Dialogue")]
    public string[] firstDialogue;
    public int firstDialogueCurrentLine;

    [Header("Second Dialogue")]
    public string[] secondDialogue;
    public int secondDialogueCurrentLine;

    [Header("TutoDialogue")]
    public string[] tutoDialogue;
    public int tutoDialogueCurrentLine;

    public bool zUsed;
    public bool qUsed;
    public bool dUsed;
    public bool sUsed;
    public bool sprintUsed;
    public bool movementTutoDone;

    public bool eUsed;
    public bool punchUsed;
    public bool attackTutoDone;

    public bool enemyEncounter;

    public bool competenceManagerFirstTutoDone;

    public bool competenceManagerSecondTutoDone;

    public bool competenceManagerThirdTutoDone;

    public bool gorillaEncounter;

    public bool gorillaAttacked;

    public bool tutoEnded;



    // Use this for initialization
    void Start () {

        TutorialTextManager = this;
        currentDialogue = 0;

    }

    // Update is called once per frame
    void Update()
    {

        /*if (!scrExperienceManager.ExperienceManager.inCompetenceMenu)
            textOnScreen = true;
        else
            textOnScreen = false;*/

        //NextLine();

        if (textOnScreen)
            transform.localPosition = new Vector3(0, 0, 0);
        else
            transform.localPosition = new Vector3(0, -1080, 0);

        /*if (!textOnScreen && Input.GetKeyDown(KeyCode.E))
            textOnScreen = true;*/

        if(textOnScreen)
        {

            if (currentDialogue == 0)
                TutoDialogue();

            if (currentDialogue == 1)
                FirstText();

            if (currentDialogue == 2)
                SecondText();

        }
    }

    void FirstText()
    {


        if (firstDialogueCurrentLine == firstDialogue.Length-1 && Input.GetKeyDown(KeyCode.Return))
        {

            currentDialogue = 0;
            firstDialogueCurrentLine = 0;
            textOnScreen = false;

        }

        if (firstDialogueCurrentLine < firstDialogue.Length-1 && Input.GetKeyDown(KeyCode.Return))
        {

            firstDialogueCurrentLine++;

        }

        dialogueText.text = firstDialogue[firstDialogueCurrentLine];

    }

    void SecondText()
    {

        if (firstDialogueCurrentLine == firstDialogue.Length - 1 && Input.GetKeyDown(KeyCode.Return))
        {

            currentDialogue = 0;
            firstDialogueCurrentLine = 0;
            textOnScreen = false;

        }

        if (firstDialogueCurrentLine < firstDialogue.Length - 1 && Input.GetKeyDown(KeyCode.Return))
        {

            firstDialogueCurrentLine++;

        }

        dialogueText.text = firstDialogue[firstDialogueCurrentLine];

    }

    void TutoDialogue()
    {

        if (Input.GetKeyDown(KeyCode.Z))
            zUsed = true;

        if (Input.GetKeyDown(KeyCode.Q))
            qUsed = true;

        if (Input.GetKeyDown(KeyCode.S))
            sUsed = true;

        if (Input.GetKeyDown(KeyCode.D))
            dUsed = true;

        if (Input.GetKeyDown(KeyCode.LeftShift))
            sprintUsed = true;

        if (sprintUsed && zUsed && qUsed && sUsed && dUsed && movementTutoDone == false)
        {

            tutoDialogueCurrentLine++;
            movementTutoDone = true;

        }

        if (Input.GetKeyDown(KeyCode.E))
            eUsed = true;

        if (Input.GetMouseButtonDown(0))
            punchUsed = true;

        if (punchUsed && eUsed && attackTutoDone == false)
        {

            tutoDialogueCurrentLine++;
            attackTutoDone = true;

        }

        if (tutoDialogueCurrentLine == 3)
        {

            movementTutoDone = true;
            attackTutoDone = true;

        }

        if(scrExperienceManager.ExperienceManager.currentExperience > 0 && competenceManagerFirstTutoDone == false)
        {

            tutoDialogueCurrentLine++;
            competenceManagerFirstTutoDone = true;

        }

        if(tutoDialogueCurrentLine == 4 && Input.GetKeyDown(KeyCode.Tab) && competenceManagerSecondTutoDone == false)
        {

            tutoDialogueCurrentLine++;
            competenceManagerSecondTutoDone = true;

        }

        if ((scrExperienceManager.ExperienceManager.baseBoostUnlocked || scrExperienceManager.ExperienceManager.protectionSphereUnlocked || scrExperienceManager.ExperienceManager.yamtaLightningUnclocked || scrExperienceManager.ExperienceManager.totemUnclocked) && competenceManagerThirdTutoDone == false)
        {

            tutoDialogueCurrentLine++;
            competenceManagerThirdTutoDone = true;

        }



        dialogueText.text = tutoDialogue[tutoDialogueCurrentLine];

    }

}
