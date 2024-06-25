using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TextMeshProUGUI inventoryMessageText;
    private float messageDisplayTime = 8f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        inventoryMessageText.gameObject.SetActive(false);
    }

    public void ShowInventoryMessage(string message)
    {
        StartCoroutine(DisplayMessage(message));
    }

    private IEnumerator DisplayMessage(string message)
    {
        inventoryMessageText.text = message;
        inventoryMessageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(messageDisplayTime);

        inventoryMessageText.gameObject.SetActive(false);
    }
}
