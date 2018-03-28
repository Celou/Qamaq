using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class scrVendettaSphereButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseOn;
    public Text text;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (mouseOn && Input.GetAxis("Slot One Input") != 0 && scrExperienceManager.ExperienceManager.vendettaSphereUnlocked)
        {
            if (!scrCompetenceManager.CompetenceManager.vendettaSphereEquiped)
                scrCompetenceManager.CompetenceManager.slotOneName = scrCompetenceManager.CompetenceManager.vendettaSphereName;

            if (scrCompetenceManager.CompetenceManager.vendettaSphereEquiped)
            {

                if (scrCompetenceManager.CompetenceManager.slotTwoName == scrCompetenceManager.CompetenceManager.vendettaSphereName)
                {

                    scrCompetenceManager.CompetenceManager.stockageName = scrCompetenceManager.CompetenceManager.slotOneName;
                    scrCompetenceManager.CompetenceManager.slotOneName = scrCompetenceManager.CompetenceManager.slotTwoName;
                    scrCompetenceManager.CompetenceManager.slotTwoName = scrCompetenceManager.CompetenceManager.stockageName;

                    scrCompetenceManager.CompetenceManager.StockageImage.sprite = scrCompetenceManager.CompetenceManager.slotOneImage.sprite;
                    scrCompetenceManager.CompetenceManager.slotOneImage.sprite = scrCompetenceManager.CompetenceManager.slotTwoImage.sprite;
                    scrCompetenceManager.CompetenceManager.slotTwoImage.sprite = scrCompetenceManager.CompetenceManager.StockageImage.sprite;

                }

                if (scrCompetenceManager.CompetenceManager.slotThreeName == scrCompetenceManager.CompetenceManager.vendettaSphereName)
                {

                    scrCompetenceManager.CompetenceManager.stockageName = scrCompetenceManager.CompetenceManager.slotOneName;
                    scrCompetenceManager.CompetenceManager.slotOneName = scrCompetenceManager.CompetenceManager.slotThreeName;
                    scrCompetenceManager.CompetenceManager.slotThreeName = scrCompetenceManager.CompetenceManager.stockageName;

                    scrCompetenceManager.CompetenceManager.StockageImage.sprite = scrCompetenceManager.CompetenceManager.slotOneImage.sprite;
                    scrCompetenceManager.CompetenceManager.slotOneImage.sprite = scrCompetenceManager.CompetenceManager.slotThreeImage.sprite;
                    scrCompetenceManager.CompetenceManager.slotThreeImage.sprite = scrCompetenceManager.CompetenceManager.StockageImage.sprite;

                }

            }

        }

        if (mouseOn && Input.GetAxis("Slot Two Input") != 0 && scrExperienceManager.ExperienceManager.vendettaSphereUnlocked)
        {
            if (!scrCompetenceManager.CompetenceManager.vendettaSphereEquiped)
                scrCompetenceManager.CompetenceManager.slotTwoName = scrCompetenceManager.CompetenceManager.vendettaSphereName;

            if (scrCompetenceManager.CompetenceManager.vendettaSphereEquiped)
            {

                if (scrCompetenceManager.CompetenceManager.slotOneName == scrCompetenceManager.CompetenceManager.vendettaSphereName)
                {

                    scrCompetenceManager.CompetenceManager.stockageName = scrCompetenceManager.CompetenceManager.slotTwoName;
                    scrCompetenceManager.CompetenceManager.slotTwoName = scrCompetenceManager.CompetenceManager.slotOneName;
                    scrCompetenceManager.CompetenceManager.slotOneName = scrCompetenceManager.CompetenceManager.stockageName;

                    scrCompetenceManager.CompetenceManager.StockageImage.sprite = scrCompetenceManager.CompetenceManager.slotTwoImage.sprite;
                    scrCompetenceManager.CompetenceManager.slotTwoImage.sprite = scrCompetenceManager.CompetenceManager.slotOneImage.sprite;
                    scrCompetenceManager.CompetenceManager.slotOneImage.sprite = scrCompetenceManager.CompetenceManager.StockageImage.sprite;

                }

                if (scrCompetenceManager.CompetenceManager.slotThreeName == scrCompetenceManager.CompetenceManager.vendettaSphereName)
                {

                    scrCompetenceManager.CompetenceManager.stockageName = scrCompetenceManager.CompetenceManager.slotTwoName;
                    scrCompetenceManager.CompetenceManager.slotTwoName = scrCompetenceManager.CompetenceManager.slotThreeName;
                    scrCompetenceManager.CompetenceManager.slotThreeName = scrCompetenceManager.CompetenceManager.stockageName;

                    scrCompetenceManager.CompetenceManager.StockageImage.sprite = scrCompetenceManager.CompetenceManager.slotTwoImage.sprite;
                    scrCompetenceManager.CompetenceManager.slotTwoImage.sprite = scrCompetenceManager.CompetenceManager.slotThreeImage.sprite;
                    scrCompetenceManager.CompetenceManager.slotThreeImage.sprite = scrCompetenceManager.CompetenceManager.StockageImage.sprite;

                }

            }

        }

        if (mouseOn && Input.GetAxis("Slot Three Input") != 0 && scrExperienceManager.ExperienceManager.vendettaSphereUnlocked)
        {
            if (!scrCompetenceManager.CompetenceManager.vendettaSphereEquiped)
                scrCompetenceManager.CompetenceManager.slotThreeName = scrCompetenceManager.CompetenceManager.vendettaSphereName;

            if (scrCompetenceManager.CompetenceManager.vendettaSphereEquiped)
            {

                if (scrCompetenceManager.CompetenceManager.slotOneName == scrCompetenceManager.CompetenceManager.vendettaSphereName)
                {

                    scrCompetenceManager.CompetenceManager.stockageName = scrCompetenceManager.CompetenceManager.slotThreeName;
                    scrCompetenceManager.CompetenceManager.slotThreeName = scrCompetenceManager.CompetenceManager.slotOneName;
                    scrCompetenceManager.CompetenceManager.slotOneName = scrCompetenceManager.CompetenceManager.stockageName;

                    scrCompetenceManager.CompetenceManager.StockageImage.sprite = scrCompetenceManager.CompetenceManager.slotThreeImage.sprite;
                    scrCompetenceManager.CompetenceManager.slotThreeImage.sprite = scrCompetenceManager.CompetenceManager.slotOneImage.sprite;
                    scrCompetenceManager.CompetenceManager.slotOneImage.sprite = scrCompetenceManager.CompetenceManager.StockageImage.sprite;

                }

                if (scrCompetenceManager.CompetenceManager.slotTwoName == scrCompetenceManager.CompetenceManager.vendettaSphereName)
                {

                    scrCompetenceManager.CompetenceManager.stockageName = scrCompetenceManager.CompetenceManager.slotThreeName;
                    scrCompetenceManager.CompetenceManager.slotThreeName = scrCompetenceManager.CompetenceManager.slotTwoName;
                    scrCompetenceManager.CompetenceManager.slotTwoName = scrCompetenceManager.CompetenceManager.stockageName;

                    scrCompetenceManager.CompetenceManager.StockageImage.sprite = scrCompetenceManager.CompetenceManager.slotThreeImage.sprite;
                    scrCompetenceManager.CompetenceManager.slotThreeImage.sprite = scrCompetenceManager.CompetenceManager.slotTwoImage.sprite;
                    scrCompetenceManager.CompetenceManager.slotTwoImage.sprite = scrCompetenceManager.CompetenceManager.StockageImage.sprite;

                }

            }

        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        mouseOn = true;

        if (scrExperienceManager.ExperienceManager.vendettaSphereUnlocked)
            text.text = "& To Use";
        else
            text.text = "Cost : 20 points";

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        mouseOn = false;

        text.text = "Payback Shield";

    }
}

