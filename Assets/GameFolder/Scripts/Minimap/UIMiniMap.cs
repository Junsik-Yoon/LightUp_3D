using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMiniMap : MonoBehaviour
{
    [SerializeField]
    private Camera minimapCamera;
    [SerializeField]
    private float zoomMin = 1;
    [SerializeField]
    private float zoomMax = 25;
    [SerializeField]
    private float zoomOneStep = 1;
    [SerializeField]
    private Text textMapName;
    public GameObject bigMapCanvas;
    private void Awake()
    {
        textMapName.text = SceneManager.GetActiveScene().name;
    }
    public void ZoomIn()
    {
        minimapCamera.orthographicSize = Mathf.Max(minimapCamera.orthographicSize-zoomOneStep,zoomMin);
    }
    public void ZoomOut()
    {
        minimapCamera.orthographicSize = Mathf.Min(minimapCamera.orthographicSize+zoomOneStep,zoomMax);
    }
    public void OnBigMapButton()
    {
        bigMapCanvas.SetActive(true);
    }
    public void CloseBigMap()
    {
        bigMapCanvas.SetActive(false);
    }
}
