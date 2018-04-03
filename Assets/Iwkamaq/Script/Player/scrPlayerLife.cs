using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrPlayerLife : MonoBehaviour {

    [Header("Life Values")]
    public float startLife;
    public float startMaxLife;
    public float currentMaxLife;
    public float currentLife;

    [Header("Life Graphics")]
    public GameObject lifeBarObject;
    public Image lifeBarFill;
    public float lifeBarShakeDuration;


    // Use this for initialization
    void Start()
    {
        currentLife = startLife;
    }

    // Update is called once per frame
    void Update()
    {
        MaxLifeUpdate();

        LifeBarUpdate();

        if (currentLife <= 0)
        {
            StartCoroutine(Death());
        }

        //cette partie doit être rendue plus propre, quand tout ce qui touche à l'exp/inteface toussa sera rendu plus propre
        if (!GetComponent<scrPlayerPaused>().playerPaused && lifeBarObject.GetComponent<Screenshake>().shakeDuration <= 0)
            lifeBarObject.transform.localPosition = new Vector3(-392, -310, 10);

        if (GetComponent<scrPlayerPaused>().playerPaused)
            lifeBarObject.transform.localPosition = new Vector3(-392, -310 - 1080, 10);
    }

    void MaxLifeUpdate()
    {
        currentMaxLife = startMaxLife;

        if (currentLife > currentMaxLife)
            currentLife = currentMaxLife;
    }

    void LifeBarUpdate()
    {
        lifeBarFill.fillAmount = currentLife / currentMaxLife;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1);

        GetComponent<scrRespawn>().Respawn();
    }

    public void TakeDamages(float takenDamages)
    {
        currentLife -= takenDamages;
    }

}
