using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDamagesManager : MonoBehaviour {

	public static scrDamagesManager DamagesManager;

	[Header("Player")]
	public GameObject player;

	[Header("PlayerPunch")]
	public float playerBasePunchKnockBack;
	public float playerBasePunchStunDuration;
	public float playerFinalPunchKnockBack;
	public float playerFinalPunchStunDuration;

	[Header("PlayerYamtaLightning")]
	public float yamtaLightningDamages;
	public float yamtaLightningKnockBack;
	public float yamtaLightningStunDuration;

	[Header("PlayerHeavenlyLightning")]
	public float heavenlyLightningDamages;
	public float heavenlyLightningKnockBack;
	public float heavenlyLightningStunDuration;

	[Header("PlayerVendettaSphere")]
	public float vendettaSphereDamages;
	public float vendettaSphereKnockBack;
	public float vendettaSphereStunDuration;

	[Header("Monkey")]
	public float monkeyDamages;
	public float monkeyKnockbackForce;
	public float monkeyStunDuration;

	[Header("Crawler")]
	public float crawlerDamages;
	public float crawlerKnockbackForce;
	public float crawlerStunDuration;

	void Start()
	{
		DamagesManager = this;
	}

	public float GetEnemyDamages(string name)
	{
		if (name == "monkey")
			return monkeyDamages;

		if (name == "crawler")
			return crawlerDamages;

		#region dummies
		if (name == "enemy1")
			return 10;

		if (name == "enemy2")
			return 20;

		if (name == "enemy3")
			return 30;
		#endregion

		else
			return 0;

	}


	public float GetEnemyKnockBackForce(string name)
	{
		if (name == "monkey")
			return monkeyKnockbackForce;

		if (name == "crawler")
			return crawlerKnockbackForce;

		if (name == "enemy1")
			return 5;

		if (name == "enemy2")
			return 10;

		if (name == "enemy3")
			return 15;

		else
			return 0;

	}

	public float GetEnemyStunDuration(string name)
	{

		if (name == "monkey")
			return monkeyStunDuration;

		if (name == "crawler")
			return crawlerStunDuration;

		return 0;

	}

	/*public Vector3 GetEnemyKnockbackDirection(string name)
    {

        if (name == "monkey")
            return monkeyKnockback;

        else
            return 0;

    }*/

	public float GetPlayerDamages(string name)
	{
		if (name == "playerPunch")
		{
			if (player.GetComponent<scrPlayer>().currentPunchCombo == 0)
				return player.GetComponent<scrPlayer>().currentPlayerDamage * player.GetComponent<scrPlayer>().finalPunchDamagesMultiplicator;
			else
				return player.GetComponent<scrPlayer>().currentPlayerDamage;
		}

		if(name == "playerLightning")
		{
			return player.GetComponent<scrPlayer>().currentPlayerDamage + yamtaLightningDamages;
		}

		if (name == "heavenlyLightning")
		{
			return player.GetComponent<scrPlayer>().currentPlayerDamage;
		}

		if (name == "vendettaSphere")
		{
			return scrCompetenceManager.CompetenceManager.vendattaShereDamageStockage;
		}

		return 0;

	}

	public float GetPlayerKnockBackForce(string name)
	{

		if (name == "playerPunch")
		{
			if (player.GetComponent<scrPlayer>().currentPunchCombo == 0)
				return playerFinalPunchKnockBack;
			else
				return playerBasePunchKnockBack;
		}

		if (name == "playerLightning")
		{
			return yamtaLightningKnockBack;
		}

		if (name == "heavenlyLightning")
		{
			return heavenlyLightningKnockBack;
		}

		if (name == "vendettaSphere")
		{
			return vendettaSphereKnockBack;
		}

		return 0;

	}

	public float GetPlayerStunDuration(string name)
	{

		if (name == "playerPunch")
		{                       
			if (player.GetComponent<scrPlayer>().currentPunchCombo == 0)
				return playerFinalPunchStunDuration;
			else
				return playerBasePunchStunDuration;
		}

		if (name == "playerLightning")
		{
			return yamtaLightningStunDuration;
		}

		if (name == "heavenlyLightning")
		{
			return heavenlyLightningStunDuration;
		}

		if (name == "vendettaSphere")
		{
			return vendettaSphereStunDuration;
		}

		return 0;

	}

}
