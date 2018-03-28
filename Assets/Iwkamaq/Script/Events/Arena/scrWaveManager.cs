using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrWaveManager : MonoBehaviour {


    // Use this for initialization
    public int enemiesleft = 0;
    bool killedallenemies = false;
    public GameObject Wave;
    public bool IsWave3;
    public Transform[] SubWaves;
    public GameObject startBlock;
    public GameObject endBlock;
    public GameObject wave3CrawlerSpawner;

    public bool IsWave1;
    public bool IsWave2;
    public GameObject Spawner1;
    public GameObject Spawner2;
    public GameObject Spawner3;

    void Start () {
		enemiesleft = 36;

        if(IsWave1 || IsWave2)
            StartCoroutine(LaunchingArena());

        if (IsWave3)
            StartCoroutine(LaunchingLastWave());
	}
	
	// Update is called once per frame
	void Update () {
        enemiesleft = 0;

        if (!IsWave3) {
            for (int i = 0; i < SubWaves.Length; i++)
            {
                enemiesleft += SubWaves[i].childCount;
            }
        }
        else
        {
            enemiesleft = SubWaves.Length;
            for (int i = 0; i < SubWaves.Length; i++)
            {
                if (SubWaves[i] == null)
                    enemiesleft -= 1;
            }
        }

        if (enemiesleft == 0 && !IsWave3)
        {
            Wave.SetActive(true);
        }
        if (enemiesleft == 0 && IsWave3)
        {
            startBlock.GetComponent<scrArenaEntryCheck>().publicDestroyed = true;
            endBlock.GetComponent<scrArenaExitCheck>().publicDestroyed = true;
            Destroy(wave3CrawlerSpawner);
        }

    }

    IEnumerator LaunchingArena()
    {
        Debug.Log("salut");
        scrCamera.CameraManager.eventing = true;
        scrCamera.CameraManager.GoalPosition = scrCamera.CameraManager.transform.position;

        yield return new WaitForSeconds(1);

        Spawner1.SetActive(true);
        scrCamera.CameraManager.GoalPosition = Spawner1.transform.position;

        yield return new WaitForSeconds(1);

        Spawner2.SetActive(true);
        scrCamera.CameraManager.GoalPosition = Spawner2.transform.position;

        yield return new WaitForSeconds(1);

        Spawner3.SetActive(true);
        scrCamera.CameraManager.GoalPosition = Spawner3.transform.position;

        yield return new WaitForSeconds(1);

        scrCamera.CameraManager.GoalPosition = scrCompetenceManager.CompetenceManager.Player.transform.position;

        yield return new WaitForSeconds(1);

        scrCamera.CameraManager.eventing = false;
        scrCamera.CameraManager.posX = 0;
        scrCamera.CameraManager.posZ = 0;
    }

    IEnumerator LaunchingLastWave()
    {
        scrCamera.CameraManager.eventing = true;
        scrCamera.CameraManager.GoalPosition = wave3CrawlerSpawner.transform.position;

        yield return new WaitForSeconds(1);

        scrCamera.CameraManager.GoalPosition = scrCompetenceManager.CompetenceManager.Player.transform.position;

        yield return new WaitForSeconds(1);

        scrCamera.CameraManager.eventing = false;

        for (int i = 0; i < 6; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(3);

        Spawner1.SetActive(true);

        yield return new WaitForSeconds(3);

        Spawner2.SetActive(true);

        yield return new WaitForSeconds(3);

        Spawner3.SetActive(true);

    }
}
