using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class InGameHUDLogic : MonoBehaviour
{
    private UIDocument _document;
    private ProgressBar _healthBar;

    void Awake()
    {
        _document = GetComponent<UIDocument>();
        _healthBar = _document.rootVisualElement.Q("CrystalHealth") as ProgressBar;
        EntityHealth.OnAnyEntityHit += UpdateHealthbarOnHit;
        EntityHealth.OnAnyEntityHeal += UpdateHealthbarOnHeal;
        _healthBar.value = 100;
    }

    public void UpdateHealthbarOnHit(object source, HitData hitdata)
    {
        if (hitdata.DamageReceiver.transform.root.CompareTag("Crystal"))
        {
            int health = hitdata.DamageReceiver.Health - hitdata.DamageDealt;
            _healthBar.value = Mathf.Max(Mathf.Min(((float)health / hitdata.DamageReceiver.MaxHealth) * 100, 100), 0);
        }
    }
    public void UpdateHealthbarOnHeal(object source, int amount)
    {
        if(source is EntityHealth eh &&
            eh.gameObject.CompareTag("Crystal"))
        {
            int health = eh.Health + amount;
            _healthBar.value = Mathf.Max(Mathf.Min(((float)health / eh.MaxHealth) * 100, 100), 0);
        }
    }

    private void OnDestroy()
    {
        EntityHealth.OnAnyEntityHit -= UpdateHealthbarOnHit;
        EntityHealth.OnAnyEntityHeal -= UpdateHealthbarOnHeal;
    }
}
