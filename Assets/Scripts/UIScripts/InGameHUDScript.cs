using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class InGameHUDLogic : MonoBehaviour
{
    [SerializeField]
    private GameState _gameState;

    private UIDocument _document;
    private ProgressBar _healthBar; // Ship health bar
    private VisualElement _xpBar; // Player xp bar
    private Label _timer; // game time timer
    private VisualElement[] _cooldownIcons; // Skill icons with the cooldowns
    private VisualElement _keyHints;
     
    // Initialize variables and register events
    void Awake()
    {
        _document = GetComponent<UIDocument>();
        _healthBar = _document.rootVisualElement.Q<ProgressBar>("CrystalHealth");
        _xpBar = _document.rootVisualElement.Q<VisualElement>("XpBar");
        _timer = _document.rootVisualElement.Q<Label>("Timer");
        _cooldownIcons = new VisualElement[]
        {
            _document.rootVisualElement.Q<VisualElement>("FillPrimary"),
            _document.rootVisualElement.Q<VisualElement>("FillSecondary"),
            _document.rootVisualElement.Q<VisualElement>("FillUtility")
        };
        _keyHints = _document.rootVisualElement.Q<VisualElement>("KeyHintWrapper");

        EntityHealth.OnAfterAnyEntityHit += UpdateHealthbar;
        EntityHealth.OnAfterAnyEntityHeal += UpdateHealthbar;
        _gameState.OnGameSecondPassed += UpdateTimer;
        _gameState.OnGameSecondPassed += HideKeyHints;
        EntityLeveling.OnAfterAnyEntityGainXP += UpdateXPBar;
        EntityCombat.OnAnyEntityAttack += UpdateCooldowns;


    }

    private void HideKeyHints(object sender, int gameTime)
    {
        if(gameTime == 20)
        {
            _keyHints.visible = false;
        }
    }

    // Set ship healthbar to full and player xp bar to zero
    private void Start()
    {
        _healthBar.value = 100;
        _xpBar.style.width = Length.Percent(0);
    }

    // When the player uses one of their skills, update the cooldown icon for that skill
    private void UpdateCooldowns(object sender, AttackData e)
    {
        if (e.EntityCombat.gameObject.CompareTag("Player") &&
            e.Slot != EntityCombat.AttackSlot.AURA)
        {

            StartCoroutine(UpdateCooldownIcon((int) e.Slot, e.Attack.Cooldown));
        }
    }

    // Make the skill icon opaque and transition its fill width from min to max over the cooldown time
    private IEnumerator UpdateCooldownIcon(int slot, float cooldown)
    {
        _cooldownIcons[slot].style.opacity = 100;
        for (int i = 0; i <= 30; i++)
        {
            _cooldownIcons[slot].style.width = 18 + i;
            yield return new WaitForSeconds(cooldown/35);
        }
    }

    // After the player gains XP, update the XP bar
    private void UpdateXPBar(object sender, EventArgs e)
    {
        if(sender is EntityLeveling el &&
            el.gameObject.CompareTag("Player"))
        {
            _xpBar.style.width = Length.Percent((float)el.Experience * 100 / el.XpToLvlUp);
        }
    }

    // Update the timer every second
    private void UpdateTimer(object sender, int e)
    {
        _timer.text = String.Format("{0:D2}:{1:D2}", (e / 60), (e % 60));
    }

    // After the health of the Ship changes, update the health bar
    public void UpdateHealthbar(object source, int _)
    {
        if (source is EntityHealth eh &&
            eh.gameObject.CompareTag("Ship"))
        {
            int health = eh.Health;
            _healthBar.value = Mathf.Max(Mathf.Min(((float)health / eh.MaxHealth) * 100, 100), 0);
        }
    }

    private void OnDestroy()
    {
        EntityHealth.OnAfterAnyEntityHit -= UpdateHealthbar;
        EntityHealth.OnAfterAnyEntityHeal -= UpdateHealthbar;
        _gameState.OnGameSecondPassed -= UpdateTimer;
        EntityLeveling.OnAfterAnyEntityGainXP -= UpdateXPBar;
        EntityCombat.OnAnyEntityAttack -= UpdateCooldowns;
    }
}
