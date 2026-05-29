using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{

    [Header("Abilities and Actions")]
    [SerializeField] private PlayerAbilities playerAbilities;
    [SerializeField] private PlayerEffects playerDrop;

    [Header("UI actions")]
    [SerializeField] private GameObject keyPrompt;
    [SerializeField] private GameObject actionWindow;
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private TextMeshProUGUI abilityTextOne;
    [SerializeField] private TextMeshProUGUI abilityTextTwo;


    private string actionName;
    private string shortName;
    private int abilityId;
    private int abilityOne;
    private int abilityTwo;
    private bool keyBinded = false;
    private delegate void Ability(int ability);


    void Update()
    {
        IntializeBinds();
    }

    private void IntializeBinds()
    {

        if (Input.GetKeyUp(KeyCode.O) && Time.timeScale != 0f)
        {
          
            if (abilityOne == 0)
            {
                abilityOne = abilityId;
                abilityTextOne.text = shortName;
                if (abilityTextOne.text == abilityTextTwo.text)
                {
                    abilityTextTwo.text = "non";
                }

            }

            if (abilityOne == abilityTwo)
            {
                abilityOne = 0;
                abilityTextOne.text = "non";

            }

            if (keyPrompt.activeInHierarchy)
            {
                keyBinded = true;
                keyPrompt.SetActive(false);
                actionWindow.SetActive(true);
            }
            Ability bindedAbility = getAbility;
            bindedAbility(abilityOne);
        }

        if (Input.GetKeyUp(KeyCode.P) && Time.timeScale != 0f)
        {
         
            if (abilityTwo == 0)
            {
                abilityTwo = abilityId;
                abilityTextTwo.text = shortName;

                if (abilityTextTwo.text == abilityTextOne.text)
                {
                    abilityTextOne.text = "non";
                }

            }

            if (abilityTwo == abilityOne) 
            {
                abilityTwo = 0;
                abilityTextTwo.text = "non";

            }
            if (keyPrompt.activeInHierarchy)
            {
                keyBinded = true;
                keyPrompt.SetActive(false);
                actionWindow.SetActive(true);
            }
            Ability bindedAbility = getAbility;
            bindedAbility(abilityTwo);

        }
    }

    public void selectFreeze()
    {
        playerAbilities.ResetAbility();

        actionName = "Freeze";
        shortName = "Frz";
        abilityId = 1;

        if (abilityOne == abilityId)
        {
            ResetBinds();
        }

        if (abilityTwo == abilityId)
        {
            ResetBinds();
        }
        getAbility(abilityId);
        return;
    }

    public void selectSight()
    {
        playerAbilities.ResetAbility();

        actionName = "Third Eye";
        shortName = "Eye";
        abilityId = 2;

        if (abilityOne == abilityId)
        {
            ResetBinds();
        }

        if (abilityTwo == abilityId)
        {
            ResetBinds();
        }
        getAbility(abilityId);
        return;
    }

    public void selectDrop()
    {
        playerAbilities.ResetAbility();

        actionName = "Drop";
        shortName = "Drp";
        abilityId = 3;

        if (abilityOne == abilityId)
        {
            ResetBinds();
        }

        if (abilityTwo == abilityId)
        {
            ResetBinds();
        }
        getAbility(abilityId);
        return;
    }
    private void ResetBinds()
    {
        abilityOne = 0;
        abilityTwo = 0;
    }

    public void ResetAbilities()
    {
        if(abilityOne != 0 && abilityTwo != 0)
        {
            abilityTextOne.text = "non";
            abilityTextTwo.text = "non";
            ResetBinds();
        }
    }
    private void getAbility(int ability)
    {
        actionText.text = actionName;

        if (ability == 1 && keyBinded && actionWindow.activeInHierarchy) 
        {
            playerAbilities.Freeze();
        }

        if (ability == 2 && keyBinded && actionWindow.activeInHierarchy) 
        {
            playerAbilities.Sight();
        }

        if (ability == 3 && keyBinded && actionWindow.activeInHierarchy)
        {
            playerDrop.DropSoul();
        }
        return;
    }

}
