using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    // Player level and experience variables
    public int level;
    public float currentXp;
    public float requiredXp;

    // Timers for XP bar animation
    float lerfTimer;
    float delayTimer;

    [Header("UI")]
    public Image frontXpBar;
    public Image backXpBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;

    [Header("Multipliers")]
    [Range(1f, 300f)]
    public float additionMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;

    void Start()
    {
        InitializeUI();
    }

    void Update()
    {
        UpdateXpUI();
        if (Input.GetKeyDown(KeyCode.C))
            GainExperienceFlatRate(20);
        if (currentXp >= requiredXp)
            LevelUp();
    }

    // Initialize UI elements
    void InitializeUI()
    {
        requiredXp = CalculateRequiredXp();
        frontXpBar.fillAmount = currentXp / requiredXp;
        backXpBar.fillAmount = currentXp / requiredXp;
        levelText.text = "Level " + level;
        xpText.text = currentXp + "/" + requiredXp;
    }

    // Update the XP bar UI
    void UpdateXpUI()
    {
        float xpFraction = currentXp / requiredXp;
        float FXP = frontXpBar.fillAmount;

        if (FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if (delayTimer > 3)
            {
                lerfTimer += Time.deltaTime;
                float percentComplete = lerfTimer / 4;
                frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
            }
        }
        xpText.text = currentXp + "/" + requiredXp;
    }

    // Gain experience at a flat rate
    public void GainExperienceFlatRate(float xpGained)
    {
        currentXp += xpGained;
        ResetTimers();
    }

    // Gain experience scaled by level difference
    public void GainExperienceScalable(float xpGained, int passedLevel)
    {
        float multiplier = (passedLevel < level) ? 1 + (level - passedLevel) * 0.1f : 1f;
        currentXp += xpGained * multiplier;
        ResetTimers();
    }

    // Level up the player
    public void LevelUp()
    {
        level++;
        ResetXPBars();
        currentXp = Mathf.RoundToInt(currentXp - requiredXp);
        GetComponent<PlayerHealth>().IncreaseHealth(level);
        requiredXp = CalculateRequiredXp();
        UpdateLevelText();
    }

    // Calculate the required XP for the next level
    private int CalculateRequiredXp()
    {
        int solveForRequiredXp = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredXp / 4;
    }

    // Reset timers for XP bar animation
    private void ResetTimers()
    {
        lerfTimer = 0f;
        delayTimer = 0f;
    }

    // Reset XP bars to initial state
    private void ResetXPBars()
    {
        frontXpBar.fillAmount = 0f;
        backXpBar.fillAmount = 0f;
    }

    // Update level text in UI
    private void UpdateLevelText()
    {
        levelText.text = "Level " + level;
    }
}
