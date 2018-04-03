using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrRespawn : MonoBehaviour {

    public void Respawn()
    {
        transform.position = scrCheckPointManager.CheckpointManager.activeCheckPoint.transform.position - new Vector3(0, 0, scrCheckPointManager.CheckpointManager.activeCheckPoint.transform.localScale.y / 2 + 0.5f);
        GetComponent<scrPlayerLife>().currentLife = GetComponent<scrPlayerLife>().currentMaxLife;
        GetComponent<scrSlowMo>().slowMoOn = false;
        Time.timeScale = 1;
        //PlayerAnimator.Play("idleFace");        
    }

}
