using UnityEngine;
using UnityEngine.UI;

public class LevelEndUI : MonoBehaviour
{
    [SerializeField] private GameObject failPanel, successPanel;
    [SerializeField] private GameObject bottomPanel;
    [SerializeField] private Button failContinueButton, successContinueButton;
    
    // Start is called before the first frame update
    private void Start()
    {
        failContinueButton.onClick.AddListener(OnClick_FailContinueButton);
        successContinueButton.onClick.AddListener(OnClick_SuccessContinueButton);
    }

    private void OnClick_FailContinueButton()
    {
        failPanel.SetActive(false);
        bottomPanel.SetActive(true);
        LevelManager.Instance.onLevelRestarted.Invoke();
    }
    
    private void OnClick_SuccessContinueButton()
    {
        successPanel.SetActive(false);
        bottomPanel.SetActive(true);
        LevelManager.Instance.onLevelRestarted.Invoke();
    }

    public void ActivateFailPanel()
    {
        failPanel.SetActive(true);
    }

    public void ActivateSuccessPanel()
    {
        successPanel.SetActive(true);
    }
}
