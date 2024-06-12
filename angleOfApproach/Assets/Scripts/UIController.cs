using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI touchDownText;
    [SerializeField] TextMeshProUGUI outOfBoundsText;
    [SerializeField] Image background;
    [SerializeField] Button retryButton;

    void Start()
    {
        touchDownText.gameObject.SetActive(false);
        outOfBoundsText.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);   
    }

    void TouchDownMenu()
    {
        touchDownText.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true); 
    }

    void OutOfBoundsMenu()
    {
        outOfBoundsText.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true); 
    }

    public void Retry()
    {
        SceneManager.LoadScene("StartScreen");
    }

    //Events Subscribe & Unsubscribe
    void OnEnable()
    {
        TriggerSys.touchDown += TouchDownMenu;
        TriggerSys.outOfBounds += OutOfBoundsMenu;
    }

    void OnDisable()
    {
        TriggerSys.touchDown -= TouchDownMenu;
        TriggerSys.outOfBounds -= OutOfBoundsMenu;
    }
}
