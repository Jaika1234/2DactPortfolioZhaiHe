using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    [SerializeField] private Image dashImage;
    [SerializeField] private Image parryImage;
    [SerializeField] private Image swordImage;
    [SerializeField] private Image flaskImage;

    [SerializeField] private TextMeshProUGUI healthText;

    protected Player player;

    private SkillManager skills;

    private void Start()
    {
        player = PlayerManager.instance.player;
        skills = SkillManager.instance;

        playerStats = FindObjectOfType<PlayerStats>();
        {
            if (playerStats != null)
                upDateHealthUI();
        }
   
        playerStats.onHealthChanged += upDateHealthUI;

        healthText = slider.GetComponentInChildren<TextMeshProUGUI>();
        healthText.text = $"{playerStats.currentHealth} / {playerStats.GetMaxHealthValue()}";

    }
    private void Update()
    {
        player = PlayerManager.instance.player;
        playerStats = FindObjectOfType<PlayerStats>();
        {
            if (playerStats != null)
                upDateHealthUI();
        }

        playerStats.onHealthChanged += upDateHealthUI;


        if (Input.GetKeyDown(KeyCode.LeftShift))
            SetCooldownOf(dashImage);

        if(Input.GetKeyDown(KeyCode.Mouse1)||Input.GetButtonDown("Fire2"))
            SetCooldownOf(swordImage);

        if (Input.GetButtonDown("UsePotion") && Inventory.instance.GetEquipment(EquipmentType.Flask) != null)
        {
            Debug.Log("Potion is cool down"+ Inventory.instance.flaskCooldown);
            SetCooldownOf(flaskImage);
        }
        if (Input.GetButtonDown("UsePotion") && Inventory.instance.GetEquipment(EquipmentType.Flask) == null)
        {
            player.fx.CreatePopUpText("You do not have a potion equipped.", Color.yellow);
        }



        CheckCooldownof(dashImage, skills.dash.cooldown);
        CheckCooldownof(swordImage, skills.sword.cooldown);
        CheckCooldownof(flaskImage, Inventory.instance.flaskCooldown);
    }

        
    private void upDateHealthUI()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealth;
        healthText.text = $"{Mathf.Max(playerStats.currentHealth, 0)} / {playerStats.GetMaxHealthValue()}";

    }
    private void SetCooldownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }

    private void CheckCooldownof(Image _image, float _cooldown)
    {
        if (_image.fillAmount > 0)
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
    }

}
