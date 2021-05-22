using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject startCanvas;
    [SerializeField] private GameObject restartCanvas;
    [SerializeField] private GameObject finishCanvas;
    
    // Start is called before the first frame update
    private void Start()
    {
        startCanvas.SetActive(true);
        restartCanvas.SetActive(false);
        finishCanvas.SetActive(false);
    }

    public void Finish()
    {
        finishCanvas.SetActive(true);
    }

    public void Restart()
    {
        restartCanvas.SetActive(true);
    }
}
