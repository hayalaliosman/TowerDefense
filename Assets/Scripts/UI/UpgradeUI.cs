using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Button healthUpgradeButton;
    public Button damageUpgradeButton;
    public Button attackSpeedUpgradeButton;
    public Button criticalChanceUpgradeButton;
    public Button rangeUpgradeButton;

    private void Start()
    {
        healthUpgradeButton.onClick.AddListener(() => UpgradeSystem.Instance.UpgradeBaseHealth());
        damageUpgradeButton.onClick.AddListener(() => UpgradeSystem.Instance.UpgradeDamage());
        attackSpeedUpgradeButton.onClick.AddListener(() => UpgradeSystem.Instance.UpgradeAttackSpeed());
        // criticalChanceUpgradeButton.onClick.AddListener(() => UpgradeSystem.Instance.UpgradeCriticalChance());
        rangeUpgradeButton.onClick.AddListener(() => UpgradeSystem.Instance.UpgradeRange());
    }
}