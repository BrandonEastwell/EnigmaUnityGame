using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public bool flashlightEnabled;
    public float currentEnergy;
    public GameObject flashlight;
    private int batteries;
    public AudioSource flashlightSound;
    void Start()
    {
        currentEnergy = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && batteries != 0 && currentEnergy < 75)
        {
            currentEnergy = 100;
            batteries -= 1;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            flashlightEnabled =! flashlightEnabled;
            flashlightSound.Play();

        }
        if (flashlightEnabled)
        {
            flashlight.SetActive(true);
            if (currentEnergy <= 0)
            {
                flashlight.SetActive(false);
            } 
            if (currentEnergy > 0)
            {
                currentEnergy -= 0.25f * Time.deltaTime;
            }
        } else
        {
            flashlight.SetActive(false);
        }
        
    }
    public void onBatteryPickUp()
    {
        batteries++;
    }
}
