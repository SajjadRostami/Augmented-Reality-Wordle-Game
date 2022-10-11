using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBaseCube : MonoBehaviour
{
    public Renderer m_renderer;
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_renderer.material.color = Color.red;


        m_renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

