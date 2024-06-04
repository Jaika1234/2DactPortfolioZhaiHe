using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity entity => GetComponentInParent<Entity>();
    private CharacterStats myStats =>GetComponentInParent<CharacterStats>();
    private RectTransform myTransform => GetComponent<RectTransform>();
    private Slider slider;
    private void Start()
    {
        slider = GetComponentInChildren<Slider>();

        upDateHealthUI();
    }

    private void upDateHealthUI()
    {
        slider.maxValue = myStats.GetMaxHealthValue();
        slider.value = myStats.currentHealth;
    }

    private void OnEnable()
    {
        entity.onFlipped += FlipUI;
        myStats.onHealthChanged += upDateHealthUI;
    }

    private void OnDisable()
    {
        if(entity!= null) entity.onFlipped -= FlipUI;
        if(myStats!=null) myStats.onHealthChanged -= upDateHealthUI;
    } 
    private void FlipUI() => myTransform.Rotate(0, 180, 0);



}
