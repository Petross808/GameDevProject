using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class InGameHUDLogic : MonoBehaviour
{
    private UIDocument _document;
    private ProgressBar _healthBar;
    private VisualElement _xpBar;
    private Label _timer;

    void Awake()
    {
        _document = GetComponent<UIDocument>();
        _healthBar = _document.rootVisualElement.Q("CrystalHealth") as ProgressBar;
        _xpBar = _document.rootVisualElement.Q("XpBar") as VisualElement;
        _timer = _document.rootVisualElement.Q("Timer") as Label;

        EntityHealth.OnAfterAnyEntityHit += UpdateHealthbarOnHit;
        EntityHealth.OnAfterAnyEntityHeal += UpdateHealthbarOnHeal;
        GameState.OnGameSecondPassed += UpdateTimer;
        EntityLeveling.OnAfterAnyEntityGainXP += UpdateXPBar;

        _healthBar.value = 100;
        _xpBar.style.width = Length.Percent(0);
    }

    private void UpdateXPBar(object sender, EventArgs e)
    {
        if(sender is EntityLeveling el &&
            el.gameObject.CompareTag("Player"))
        {
            _xpBar.style.width = Length.Percent((float)el.Experience * 100 / el.XpToLvlUp);
        }
    }

    private void UpdateTimer(object sender, int e)
    {
        _timer.text = String.Format("{0:D2}:{1:D2}", (e / 60), (e % 60));
    }

    public void UpdateHealthbarOnHit(object source, HitData hitdata)
    {
        if (hitdata.DamageReceiver.transform.root.CompareTag("Crystal"))
        {
            int health = hitdata.DamageReceiver.Health;
            _healthBar.value = Mathf.Max(Mathf.Min(((float)health / hitdata.DamageReceiver.MaxHealth) * 100, 100), 0);
        }
    }
    public void UpdateHealthbarOnHeal(object source, int amount)
    {
        if(source is EntityHealth eh &&
            eh.gameObject.CompareTag("Crystal"))
        {
            int health = eh.Health;
            _healthBar.value = Mathf.Max(Mathf.Min(((float)health / eh.MaxHealth) * 100, 100), 0);
        }
    }

    private void OnDestroy()
    {
        EntityHealth.OnAfterAnyEntityHit -= UpdateHealthbarOnHit;
        EntityHealth.OnAfterAnyEntityHeal -= UpdateHealthbarOnHeal;
        GameState.OnGameSecondPassed -= UpdateTimer;
        EntityLeveling.OnAfterAnyEntityGainXP -= UpdateXPBar;
    }
}
