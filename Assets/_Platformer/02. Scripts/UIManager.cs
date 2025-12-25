using Platformer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public SceneType sceneType;

    private IMovement movement;
    private AdventurerAttack attack;

    // [SerializeField] private TMP_Dropdown selectedInput;
    //
    // [SerializeField] private GameObject optionUI;
    // [SerializeField] private GameObject inventoryUI;
    //
    // [SerializeField] private Button optionButton;
    // [SerializeField] private Button optionExitButton;
    // [SerializeField] private Button inventoryButton;
    
    // void Start()
    // {
    //     if (sceneType == SceneType.Town)
    //     {
    //         movement = FindFirstObjectByType<TownMovement>() as IMovement;
    //     }
    //     else if (sceneType == SceneType.HuntingGround)
    //     {
    //         movement = FindFirstObjectByType<AdventurerMovement>() as IMovement;
    //         attack = FindFirstObjectByType<AdventurerAttack>();
    //
    //         attack.inputType = movement.inputType;
    //     }
    //     
    //     selectedInput.value = (int)movement.inputType;
    //
    //     selectedInput.onValueChanged.AddListener(SetInputType);
    //
    //     optionButton.onClick.AddListener(OptionUIOn);
    //     optionExitButton.onClick.AddListener(OptionUIOff);
    //     
    //     if (inventoryButton != null)
    //         inventoryButton.onClick.AddListener(InventoryUIOn);
    // }
    //
    // private void OptionUIOn()
    // {
    //     Time.timeScale = 0f;
    //     optionUI.SetActive(true);
    // }
    //
    // private void OptionUIOff()
    // {
    //     Time.timeScale = 1f;
    //     optionUI.SetActive(false);
    // }
    //
    // private void InventoryUIOn()
    // {
    //     if (inventoryUI.activeSelf)
    //     {
    //         Time.timeScale = 1f;
    //         inventoryUI.SetActive(false);
    //     }
    //     else
    //     {
    //         Time.timeScale = 0f;
    //         inventoryUI.SetActive(true);
    //     }
    // }

    // private void ActiveOptionUI()
    // {
    //     bool isActive = optionUI.activeSelf;
    //     
    //     optionUI.SetActive(!isActive);
    // }

    private void SetInputType(int index)
    {
        movement.inputType = (InputType)index;

        if (sceneType == SceneType.HuntingGround)
            attack.inputType = (InputType)index;
    }
}