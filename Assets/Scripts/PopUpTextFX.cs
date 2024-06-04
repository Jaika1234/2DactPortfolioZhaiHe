using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpTextFX : MonoBehaviour
{
    private TextMeshPro myText; // Reference to the TextMeshPro component

    [SerializeField] private float speed;// Ascension speed
    [SerializeField] private float desapearanceSpeed; // Text disappearance speed
    [SerializeField] private float colorDeaspearanceSpeed; // Color disappearance speed

    [SerializeField] private float lifeTime;// Total time the text is displayed

    private float textTimer;
    void Start()
    {
        myText = GetComponent<TextMeshPro>();
        textTimer = lifeTime;
    }
    void Update()
    {
        // pop-up text effect
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), speed * Time.deltaTime);
        textTimer -= Time.deltaTime;

        if (textTimer < 0)
        {
            float alpha = myText.color.a - colorDeaspearanceSpeed * Time.deltaTime;
            myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, alpha);
            // Text disappearance
            if (myText.color.a < 50)
                speed = desapearanceSpeed;
            //transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 1), desapearanceSpeed * Time.deltaTime);
            if (myText.color.a<0)
                Destroy(gameObject);
        }
            // Text ascension phase
    }

}
