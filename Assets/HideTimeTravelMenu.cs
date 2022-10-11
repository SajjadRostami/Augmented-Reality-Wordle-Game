using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HideTimeTravelMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("SectionSubtitleStepOne"))
        {
            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("SectionSubtitleStepTwo"))
        {
            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("SectionSubtitleStepThree"))
        {
            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("SectionSubtitleStepFour"))
        {
            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("SectionSubtitleStepFive"))
        {
            TextMeshPro renderer = go.GetComponent<TextMeshPro>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }

        //back panel
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("BackPlateOne"))
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("BackPlateTwo"))
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("BackPlateThree"))
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("BackPlateFour"))
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("BackPlateFive"))
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        //gems buttons

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("StepOneGem"))
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("StepTwoGem"))
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("StepThreeGem"))
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("StepFourGem"))
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("StepFiveGem"))
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
