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
        EntityHealth.OnAnyEntityHit += UpdateHealthbar;
        _healthBar.value = 100;
    }

    public void UpdateHealthbar(object source, HitData hitdata)
    {
        if (hitdata.DamageReceiver.transform.root.CompareTag("Crystal"))
        {
            int health = hitdata.DamageReceiver.Health - hitdata.DamageDealt;
            _healthBar.value = Mathf.Max(Mathf.Min(((float)health / hitdata.DamageReceiver.MaxHealth) * 100, 100), 0);
        }
    }
}
