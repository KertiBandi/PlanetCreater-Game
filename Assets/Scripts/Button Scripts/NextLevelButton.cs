using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnValidate()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(gameManager.StartNextScene);
    }


}