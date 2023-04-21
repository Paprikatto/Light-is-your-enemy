using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LightSwitch : MonoBehaviour
{
    [SerializeField] private bool active;
    [SerializeField] private Image inputImage;
    [SerializeField] private List<GameObject> gameObjects;

    private void Start()
    {
        inputImage.enabled = false;
    }
    private void Activate()
    {
        active = true;
        inputImage.enabled = true;
    }
    private void Deactivate()
    {
        active = false;
        inputImage.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Activate();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Deactivate();
        }
    }

    private void Update()
    {
        if(active && Input.GetKeyDown(KeyCode.E))
        {
            foreach(GameObject g in gameObjects)
            {
                g.SetActive(!g.activeSelf);
            }
        }
    }
}
