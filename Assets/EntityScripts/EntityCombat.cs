using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    public Transform _baseAttackTemplate;
    public Transform _aim;
    private IAttack _primaryAttack;
    private IAttack _secondaryAttack;


    // Start is called before the first frame update
    void Start()
    {
        RegisterAttack(ref _primaryAttack, _baseAttackTemplate);
        RegisterAttack(ref _secondaryAttack, _baseAttackTemplate);
    }

    private void RegisterAttack(ref IAttack slotReference, Transform attackToRegister)
    {
        Transform attackTransform = Instantiate(attackToRegister);
        attackTransform.parent = transform;
        slotReference = attackTransform.GetComponent<IAttack>();
        attackTransform.gameObject.SetActive(false);
    }

    public void PrimaryAttack()
    {
        _primaryAttack.Attack(_aim);
    }
    public void SecondaryAttack()
    {
        _secondaryAttack.Attack(_aim);
    }

    void Update()
    {
        _primaryAttack.CooldownTick();
        _secondaryAttack.CooldownTick();
    }
}
