using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrCompetenceManager : MonoBehaviour {

	public static scrCompetenceManager CompetenceManager;

	[Header("Global Parameters")]
	//public float globalCooldown;
	public float currentGlobalCooldown;

	[Header("Player")]
	public GameObject Player;

	[Header("Slot 1")]
	public string slotOneName;
	public float slotOneCooldown;
	public float currentSlotOneCooldown;
	public Image slotOneImage;
	public Image slotOneFill;
	public KeyCode SlotOneInput;


	[Header("Slot 2")]
	public string slotTwoName;
	public float slotTwoCooldown;
	public float currentSlotTwoCooldown;
	public Image slotTwoImage;
	public Image slotTwoFill;
	public KeyCode SlotTwoInput;


	[Header("Slot 3")]
	public string slotThreeName;
	public float slotThreeCooldown;
	public float currentSlotThreeCooldown;
	public Image slotThreeImage;
	public Image slotThreeFill;
	public KeyCode SlotThreeInput;


	[Header("Empty")]
	public string stockageName;
	public Image EmptyImage;
	public Image StockageImage;

	[Header("Shaking")]
	public float equipementShakingDuration;
	public float equipementShakingAmount;

	[Header("ProtectionSphere")]
	public string protectionSphereName;
	public float protectionSphereDuration;
	public float currentProtectionSphereDuration;
	public float cooldownProtectionSphere;
	public float currentCooldownProtectionSphere;
	public bool protectionSphereOn;
	public bool protectionSphereEquiped;
	public Image ProtectionSphereImage;
	public float protectionSphereGlobalCooldown;
	public bool competenceKeyDown;
	public SphereCollider protectionSphereCollider;
	public float protectionSphereColliderMaxRadius;
	public float protectionSphereColliderExpandSpeed;
	public Image protectionSphereCooldownFill;

	[Header("YamtaLightning")]
	public string yamtaLightningName;
	public float yamtaLightningCooldown;
	public float currentYamtaLightningCooldown;
	public GameObject PlayerYamtaLightning;
	public int yamtaLightningDamage;
	public bool yamtaLightningEquiped;
	public Image YamtaLightningImage;
	public float yamtaLightningGlobalCooldown;
	public Image yamtaLightningCooldownFill;

	[Header("Totem")]
	public string totemName;
	public int currentTotemLife;
	public float totemDuration;
	public float currentTotemDuration;
	public bool totemSummoned;
	public Transform totemPosition;
	public bool taunted;
	public GameObject Totem;
	public float totemCooldown;
	public float currentTotemCooldown;
	public int maxTotemLife;
	public bool totemEquiped;
	public Image TotemImage;
	public float totemGlobalCooldown;
	public Image totemCooldownFill;

	[Header("Boost")]
	public string boostName;
	public float boostCooldown;
	public float currentBoostCooldown;
	public float boostDuration;
	public float currentBoostDuration;
	public float damageBoostAmount;
	public float currentDamageBoostAmount;
	public float speedBoostAmont;
	public float currentSpeedBoostAmount;
	public ParticleSystem Particles;
	public bool boostEquiped;
	public Image BoostImage;
	public float boostGlobalCooldown;
	public Image boostCooldownFill;

	[Header("VendettaSphere")]
	public string vendettaSphereName;
	public float vendettaSphereDuration;
	public float currentVendettaSphereDuration;
	public float cooldownVendettaSphere;
	public float currentCooldownVendettaSphere;
	public bool vendettaSphereOn;
	public bool vendettaSphereEquiped;
	public Image VendettaSphereImage;
	public float vendettaSphereGlobalCooldown;
	public GameObject vendettaSphereObject;
	public float vendettaSphereExpandDuration;
	public float vendettaSphereCurrentExpandDuration;
	public float vendettaSphereProtectionAmount;
	public float vendattaShereDamageStockage;
	public float vendettaSphereExpandSize;
	public Image vendettaSphereCooldownFill;

	[Header("HeavenlyLightning")]
	public string heavenlyLightningName;
	public float heavenlyLightningExpandDuration;
	public float currentHeavenlyLightningExpandDuration;
	public float heavenlyLightningExpandSize;
	public float heavenlyLightningCooldown;
	public float currentHeavenlyLightningCooldown;
	public bool heavenlyLightningEquiped;
	public GameObject heavenlyLightningObject;
	public Image heavenlyLightningImage;
	public float heavenlyLightningGlobalCooldown;
	public float heavenlyLightningDamage;
	public Image heavenlyLightningCooldownFill;

	// Use this for initialization
	void Start () {

		vendettaSphereObject.transform.position = Player.transform.position + new Vector3(0, 10, 0);
		heavenlyLightningObject.transform.position = Player.transform.position + new Vector3(0, 10, 0);


		CompetenceManager = this;

		protectionSphereGlobalCooldown = protectionSphereDuration;
		vendettaSphereGlobalCooldown = vendettaSphereDuration;

		currentDamageBoostAmount = 1;
		currentSpeedBoostAmount = 1;

        protectionSphereCollider.radius = 0;


	}

	// Update is called once per frame
	void Update () {

		GlobalUpdate();

		Slot1Manager();

		Slot2Manager();

		Slot3Manager();

		EquipedCompetencesChecker();

		/*if (!scrExperienceManager.ExperienceManager.inCompetenceMenu)
		{

			VendettaSphereExpandUpdate();
			HeavenlyLightningExpandUpdate();

		}*/

		ButtonsFill();

	}

	void GlobalUpdate()
	{

		if (currentGlobalCooldown > 0)
			currentGlobalCooldown -= Time.deltaTime;

		if (currentGlobalCooldown < 0)
		{

			currentGlobalCooldown = 0;

		}

	}

	//fonction qui gère la compétence présente dans le Slot1
	void Slot1Manager()
	{

		if (slotOneName == "empty")
		{

			slotOneImage.sprite = EmptyImage.sprite;
			slotOneFill.fillAmount = 1;

		}

		if (slotOneName == protectionSphereName)
		{

			if(protectionSphereEquiped != true)
			{

				protectionSphereEquiped = true;
				//currentCooldownProtectionSphere = cooldownProtectionSphere;
				slotOneImage.sprite = ProtectionSphereImage.sprite;

			}

			slotOneFill.fillAmount = currentCooldownProtectionSphere / cooldownProtectionSphere;

		}

		if(slotOneName == yamtaLightningName)
		{

			if(yamtaLightningEquiped != true)
			{

				yamtaLightningEquiped = true;
				//currentYamtaLightningCooldown = yamtaLightningCooldown;
				slotOneImage.sprite = YamtaLightningImage.sprite;

			}

			slotOneFill.fillAmount = currentYamtaLightningCooldown / yamtaLightningCooldown;

		}

		if (slotOneName == totemName)
		{

			if (totemEquiped != true)
			{

				totemEquiped = true;
				//currentTotemCooldown = totemCooldown;
				slotOneImage.sprite = TotemImage.sprite;

			}

			slotOneFill.fillAmount = currentTotemCooldown / totemCooldown;

		}

		if (slotOneName == boostName)
		{

			if (boostEquiped != true)
			{

				boostEquiped = true;
				//currentBoostCooldown = boostCooldown;
				slotOneImage.sprite = BoostImage.sprite;

			}

			slotOneFill.fillAmount = currentBoostCooldown / boostCooldown;

		}

		if (slotOneName == vendettaSphereName)
		{

			if (vendettaSphereEquiped != true)
			{

				vendettaSphereEquiped = true;
				//currentCooldownVendettaSphere = cooldownVendettaSphere;
				slotOneImage.sprite = VendettaSphereImage.sprite;

			}

			slotOneFill.fillAmount = currentCooldownVendettaSphere / cooldownVendettaSphere;

		}

		if (slotOneName == heavenlyLightningName)
		{

			if (heavenlyLightningEquiped != true)
			{

				heavenlyLightningEquiped = true;
				//currentHeavenlyLightningCooldown = heavenlyLightningCooldown;
				slotOneImage.sprite = heavenlyLightningImage.sprite;

			}

			slotOneFill.fillAmount = currentHeavenlyLightningCooldown / heavenlyLightningCooldown;

		}

	}

	//fonction qui gère la compétence présente dans le Slot2
	void Slot2Manager()
	{

		if (slotTwoName == "empty")
		{

			slotTwoImage.sprite = EmptyImage.sprite;
			slotTwoFill.fillAmount = 1;

		}

		if (slotTwoName == protectionSphereName)
		{

			if (protectionSphereEquiped != true)
			{

				protectionSphereEquiped = true;
				//currentCooldownProtectionSphere = cooldownProtectionSphere;
				slotTwoImage.sprite = ProtectionSphereImage.sprite;

			}

			slotTwoFill.fillAmount = currentCooldownProtectionSphere / cooldownProtectionSphere;

		}

		if (slotTwoName == yamtaLightningName)
		{

			if (yamtaLightningEquiped != true)
			{

				yamtaLightningEquiped = true;
				//currentYamtaLightningCooldown = yamtaLightningCooldown;
				slotTwoImage.sprite = YamtaLightningImage.sprite;

			}

			slotTwoFill.fillAmount = currentYamtaLightningCooldown / yamtaLightningCooldown;

		}

		if (slotTwoName == totemName)
		{

			if (totemEquiped != true)
			{

				totemEquiped = true;
				//currentTotemCooldown = totemCooldown;
				slotTwoImage.sprite = TotemImage.sprite;

			}

			slotTwoFill.fillAmount = currentTotemCooldown / totemCooldown;

		}

		if (slotTwoName == boostName)
		{

			if (boostEquiped != true)
			{

				boostEquiped = true;
				//currentBoostCooldown = boostCooldown;
				slotTwoImage.sprite = BoostImage.sprite;

			}

			slotTwoFill.fillAmount = currentBoostCooldown / boostCooldown;

		}

		if (slotTwoName == vendettaSphereName)
		{

			if (vendettaSphereEquiped != true)
			{

				vendettaSphereEquiped = true;
				//currentCooldownVendettaSphere = cooldownVendettaSphere;
				slotTwoImage.sprite = VendettaSphereImage.sprite;

			}

			slotTwoFill.fillAmount = currentCooldownVendettaSphere / cooldownVendettaSphere;

		}

		if (slotTwoName == heavenlyLightningName)
		{

			if (heavenlyLightningEquiped != true)
			{

				heavenlyLightningEquiped = true;
				//currentHeavenlyLightningCooldown = heavenlyLightningCooldown;
				slotTwoImage.sprite = heavenlyLightningImage.sprite;

			}

			slotTwoFill.fillAmount = currentHeavenlyLightningCooldown / heavenlyLightningCooldown;

		}


	}

	//fonction qui gère la compétence présente dans le Slot3
	void Slot3Manager()
	{

		if(slotThreeName == "empty")
		{

			slotThreeImage.sprite = EmptyImage.sprite;
			slotThreeFill.fillAmount = 1;

		}

		if (slotThreeName == protectionSphereName)
		{

			if (protectionSphereEquiped != true)
			{

				protectionSphereEquiped = true;
				//currentCooldownProtectionSphere = cooldownProtectionSphere;
				slotThreeImage.sprite = ProtectionSphereImage.sprite;

			}

			slotThreeFill.fillAmount = currentCooldownProtectionSphere / cooldownProtectionSphere;

		}

		if (slotThreeName == yamtaLightningName)
		{

			if (yamtaLightningEquiped != true)
			{

				yamtaLightningEquiped = true;
				//currentYamtaLightningCooldown = yamtaLightningCooldown;
				slotThreeImage.sprite = YamtaLightningImage.sprite;

			}

			slotThreeFill.fillAmount = currentYamtaLightningCooldown / yamtaLightningCooldown;

		}

		if (slotThreeName == totemName)
		{

			if (totemEquiped != true)
			{

				totemEquiped = true;
				//currentTotemCooldown = totemCooldown;
				slotThreeImage.sprite = TotemImage.sprite;

			}

			slotThreeFill.fillAmount = currentTotemCooldown / totemCooldown;

		}

		if (slotThreeName == boostName)
		{

			if (boostEquiped != true)
			{

				boostEquiped = true;
				//currentBoostCooldown = boostCooldown;
				slotThreeImage.sprite = BoostImage.sprite;

			}

			slotThreeFill.fillAmount = currentBoostCooldown / boostCooldown;

		}

		if (slotThreeName == vendettaSphereName)
		{

			if (vendettaSphereEquiped != true)
			{

				vendettaSphereEquiped = true;
				//currentCooldownVendettaSphere = cooldownVendettaSphere;
				slotThreeImage.sprite = VendettaSphereImage.sprite;

			}

			slotThreeFill.fillAmount = currentCooldownVendettaSphere / cooldownVendettaSphere;

		}

		if (slotThreeName == heavenlyLightningName)
		{

			if (heavenlyLightningEquiped != true)
			{

				heavenlyLightningEquiped = true;
				//currentHeavenlyLightningCooldown = heavenlyLightningCooldown;
				slotThreeImage.sprite = heavenlyLightningImage.sprite;

			}

			slotThreeFill.fillAmount = currentHeavenlyLightningCooldown / heavenlyLightningCooldown;

		}

	}

	//cette fonction gère le déséquipement des compétences. Elle permet notamment à certains effets de se jouer à l'instant ou le joueur la déséquipe (par exemple, supprimer le boost, désinvoquer le totem, activer la zone de Vendetta,...)
	void EquipedCompetencesChecker() {

		//permet de déséquiper les compétences. Vérifie les durées et les passe à -1 si la compétence associée est déséquipée
		if (protectionSphereEquiped && (slotOneName != protectionSphereName && slotTwoName != protectionSphereName && slotThreeName != protectionSphereName))
		{
			if (currentProtectionSphereDuration > 0)
				currentProtectionSphereDuration = -0.1f;
			protectionSphereEquiped = false;
		}


		if (yamtaLightningEquiped && (slotOneName != yamtaLightningName && slotTwoName != yamtaLightningName && slotThreeName != yamtaLightningName))
			yamtaLightningEquiped = false;

		if (totemEquiped && (slotOneName != totemName && slotTwoName != totemName && slotThreeName != totemName))
		{

			if (totemSummoned == true)
			{
				taunted = false;
				currentTotemLife = 0;
				totemPosition.position = transform.position + new Vector3(0, 20, 0);
				totemSummoned = false;
			}

			totemEquiped = false;

		}

		if (boostEquiped && (slotOneName != boostName && slotTwoName != boostName && slotThreeName != boostName))
		{
			if(currentBoostDuration>0)
				currentBoostDuration = -1;

			boostEquiped = false;
		}

		if(vendettaSphereEquiped && (slotOneName != vendettaSphereName && slotTwoName != vendettaSphereName && slotThreeName != vendettaSphereName)){

			if (currentVendettaSphereDuration > 0)
			{
				vendettaSphereOn = false;
				currentVendettaSphereDuration = 0;

				CompetenceManager.vendettaSphereCurrentExpandDuration = CompetenceManager.vendettaSphereExpandDuration;
				CompetenceManager.vendettaSphereObject.transform.position = Player.transform.position;
			}

			vendettaSphereEquiped = false;

		}

		if (heavenlyLightningEquiped && (slotOneName != heavenlyLightningName && slotTwoName != heavenlyLightningName && slotThreeName != heavenlyLightningName))
			heavenlyLightningEquiped = false;

	}



	void VendettaSphereExpandUpdate()
	{

		if (vendettaSphereCurrentExpandDuration > 0)
		{

			vendettaSphereCurrentExpandDuration -= Time.deltaTime;
			vendettaSphereObject.transform.localScale = new Vector3(Mathf.Lerp(0, vendettaSphereExpandSize, 1 - (vendettaSphereCurrentExpandDuration / vendettaSphereExpandDuration)), Mathf.Lerp(0, vendettaSphereExpandSize, 1 - (vendettaSphereCurrentExpandDuration / vendettaSphereExpandDuration)), Mathf.Lerp(0, vendettaSphereExpandSize, 1 - (vendettaSphereCurrentExpandDuration / vendettaSphereExpandDuration)));


		}

		//fin de l'attaque de zone
		if (vendettaSphereCurrentExpandDuration < 0)
		{

			vendettaSphereCurrentExpandDuration = 0;
			vendettaSphereObject.transform.localScale = new Vector3(0, 0, 0);
			vendettaSphereObject.transform.position = Player.transform.position + new Vector3(0, 10, 0);

		}

	}

	void HeavenlyLightningExpandUpdate()
	{

		if(currentHeavenlyLightningExpandDuration > 0)
		{

			currentHeavenlyLightningExpandDuration -= Time.deltaTime;
			heavenlyLightningObject.transform.localScale = new Vector3(Mathf.Lerp(0, heavenlyLightningExpandSize, 1 - (currentHeavenlyLightningExpandDuration / heavenlyLightningExpandDuration)), Mathf.Lerp(0, heavenlyLightningExpandSize, 1 - (currentHeavenlyLightningExpandDuration / heavenlyLightningExpandDuration)), Mathf.Lerp(0, heavenlyLightningExpandSize, 1 - (currentHeavenlyLightningExpandDuration / heavenlyLightningExpandDuration)));

		}

		if(currentHeavenlyLightningExpandDuration < 0)
		{

			currentHeavenlyLightningExpandDuration = 0;
			heavenlyLightningObject.transform.localScale = new Vector3(0, 0, 0);
			heavenlyLightningObject.transform.position = Player.transform.position + new Vector3(0, 10, 0);

		}

	}

	public void EquipInFirstSlot(string competenceName)
	{
		bool equiped = false;

		#region verification : équipé ou pas?
		if (competenceName == boostName && boostEquiped)
			equiped = true;

		if (competenceName == yamtaLightningName && yamtaLightningEquiped)
			equiped = true;

		if (competenceName == totemName && totemEquiped)
			equiped = true;

		if (competenceName == protectionSphereName && protectionSphereEquiped)
			equiped = true;

		if (competenceName == vendettaSphereName && vendettaSphereEquiped)
			equiped = true;

		if (competenceName == heavenlyLightningName && heavenlyLightningEquiped)
			equiped = true;
		#endregion

		#region si pas équipé        
		if (!equiped)
		{
			slotOneName = competenceName;
			scrExperienceManager.ExperienceManager.InterfaceShake(equipementShakingDuration, equipementShakingAmount);
			scrExperienceManager.ExperienceManager.competenceEquipementSound.Play();
		}
		#endregion

		if (equiped)
		{

			if (slotTwoName == competenceName)
			{

				stockageName = slotOneName;
				slotOneName = slotTwoName;
				slotTwoName = stockageName;

				StockageImage.sprite = slotOneImage.sprite;
				slotOneImage.sprite = slotTwoImage.sprite;
				slotTwoImage.sprite = StockageImage.sprite;

				scrExperienceManager.ExperienceManager.InterfaceShake(equipementShakingDuration, equipementShakingAmount);
				scrExperienceManager.ExperienceManager.competenceEquipementSound.Play();
			}

			if (slotThreeName == competenceName)
			{

				stockageName = slotOneName;
				slotOneName = slotThreeName;
				slotThreeName = stockageName;

				StockageImage.sprite = slotOneImage.sprite;
				slotOneImage.sprite = slotThreeImage.sprite;
				slotThreeImage.sprite = StockageImage.sprite;

				scrExperienceManager.ExperienceManager.InterfaceShake(equipementShakingDuration, equipementShakingAmount);
				scrExperienceManager.ExperienceManager.competenceEquipementSound.Play();
			}

		}
	}

	public void EquipInSecondSlot(string competenceName)
	{
		bool equiped = false;

		#region verification : équipé ou pas?
		if (competenceName == boostName && boostEquiped)
			equiped = true;

		if (competenceName == yamtaLightningName && yamtaLightningEquiped)
			equiped = true;

		if (competenceName == totemName && totemEquiped)
			equiped = true;

		if (competenceName == protectionSphereName && protectionSphereEquiped)
			equiped = true;

		if (competenceName == vendettaSphereName && vendettaSphereEquiped)
			equiped = true;

		if (competenceName == heavenlyLightningName && heavenlyLightningEquiped)
			equiped = true;
		#endregion

		#region si pas équipé        
		if (!equiped)
		{
			slotTwoName = competenceName;
			scrExperienceManager.ExperienceManager.InterfaceShake(equipementShakingDuration, equipementShakingAmount);
			scrExperienceManager.ExperienceManager.competenceEquipementSound.Play();
		}
		#endregion

		if (equiped)
		{

			if (slotOneName == competenceName)
			{

				stockageName = slotTwoName;
				slotTwoName = slotOneName;
				slotOneName = stockageName;

				StockageImage.sprite = slotTwoImage.sprite;
				slotTwoImage.sprite = slotOneImage.sprite;
				slotOneImage.sprite = StockageImage.sprite;

				scrExperienceManager.ExperienceManager.InterfaceShake(equipementShakingDuration, equipementShakingAmount);
				scrExperienceManager.ExperienceManager.competenceEquipementSound.Play();
			}

			if (slotThreeName == competenceName)
			{

				stockageName = slotTwoName;
				slotTwoName = slotThreeName;
				slotThreeName = stockageName;

				StockageImage.sprite = slotTwoImage.sprite;
				slotTwoImage.sprite = slotThreeImage.sprite;
				slotThreeImage.sprite = StockageImage.sprite;

				scrExperienceManager.ExperienceManager.InterfaceShake(equipementShakingDuration, equipementShakingAmount);
				scrExperienceManager.ExperienceManager.competenceEquipementSound.Play();
			}

		}
	}

	public void EquipInThirdSlot(string competenceName)
	{
		bool equiped = false;

		#region verification : équipé ou pas?
		if (competenceName == boostName && boostEquiped)
			equiped = true;

		if (competenceName == yamtaLightningName && yamtaLightningEquiped)
			equiped = true;

		if (competenceName == totemName && totemEquiped)
			equiped = true;

		if (competenceName == protectionSphereName && protectionSphereEquiped)
			equiped = true;

		if (competenceName == vendettaSphereName && vendettaSphereEquiped)
			equiped = true;

		if (competenceName == heavenlyLightningName && heavenlyLightningEquiped)
			equiped = true;
		#endregion

		#region si pas équipé        
		if (!equiped)
		{
			slotThreeName = competenceName;
			scrExperienceManager.ExperienceManager.InterfaceShake(equipementShakingDuration, equipementShakingAmount);
			scrExperienceManager.ExperienceManager.competenceEquipementSound.Play();
		}
		#endregion

		if (equiped)
		{

			if (slotOneName == competenceName)
			{

				stockageName = slotThreeName;
				slotThreeName = slotOneName;
				slotOneName = stockageName;

				StockageImage.sprite = slotThreeImage.sprite;
				slotThreeImage.sprite = slotOneImage.sprite;
				slotOneImage.sprite = StockageImage.sprite;

				scrExperienceManager.ExperienceManager.InterfaceShake(equipementShakingDuration, equipementShakingAmount);
				scrExperienceManager.ExperienceManager.competenceEquipementSound.Play();
			}

			if (slotTwoName == competenceName)
			{

				stockageName = slotThreeName;
				slotThreeName = slotTwoName;
				slotTwoName = stockageName;

				StockageImage.sprite = slotThreeImage.sprite;
				slotThreeImage.sprite = slotTwoImage.sprite;
				slotTwoImage.sprite = StockageImage.sprite;

				scrExperienceManager.ExperienceManager.InterfaceShake(equipementShakingDuration, equipementShakingAmount);
				scrExperienceManager.ExperienceManager.competenceEquipementSound.Play();
			}

		}
	}

	void ButtonsFill()
	{
		yamtaLightningCooldownFill.fillAmount = currentYamtaLightningCooldown/yamtaLightningCooldown;

		protectionSphereCooldownFill.fillAmount = currentCooldownProtectionSphere/ cooldownProtectionSphere;

		totemCooldownFill.fillAmount = currentTotemCooldown/totemCooldown;

		boostCooldownFill.fillAmount = currentBoostCooldown/boostCooldown;

		//vendettaSphereCooldownFill.fillAmount = currentCooldownVendettaSphere/cooldownVendettaSphere;

		//heavenlyLightningCooldownFill.fillAmount = currentHeavenlyLightningCooldown/heavenlyLightningCooldown;

	}
}
