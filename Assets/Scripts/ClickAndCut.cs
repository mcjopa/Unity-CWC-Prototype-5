using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(TrailRenderer))]
public class ClickAndCut : MonoBehaviour
{
    private GameManager gameManager;
    private bool isDragging = false;
    private BoxCollider boxCollider;
    private TrailRenderer trail;
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        boxCollider = GetComponent<BoxCollider>();
        trail = GetComponent<TrailRenderer>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameActive || gameManager.isGamePaused) return;

        transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        LmbDragging();
        HandleDrag();
    }

    void LmbDragging()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            Debug.Log("LMB is pressed!");
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Debug.Log("LMB released!");
        }
    }

    void HandleDrag()
    {
        if (isDragging)
        {
            boxCollider.enabled = true;
            trail.enabled = true;
        }
        else
        {
            boxCollider.enabled = false;
            trail.enabled = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>())
        {
            collision.gameObject.GetComponent<Target>().Destroy();
        }      
    }
}
