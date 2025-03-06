using TMPro;
using UnityEngine;

public class PressAnyKeyToStart : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private bool isGameStarted = false;
    [SerializeField] private GameManager gameManager;


    private void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted && Input.anyKeyDown)
        {
            gameManager.StartGame();
            text.gameObject.SetActive(false);
            Destroy(this);
        }
    }
}
