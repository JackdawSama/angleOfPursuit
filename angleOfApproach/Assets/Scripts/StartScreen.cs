using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] Button startButton;

    public void StartSim()
    {
        SceneManager.LoadScene("TouchdownScene");
    }
}
