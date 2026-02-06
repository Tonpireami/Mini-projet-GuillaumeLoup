using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    public Transform handAttachPoint;

    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable weapon;

    private Rigidbody weaponRb;
    private Animator anim;

    void Awake()
    {
        if (weapon == null || handAttachPoint == null)
        {
            enabled = false;
            return;
        }

        weaponRb = weapon.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        if (anim == null)
        {
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            anim.SetTrigger("Equip");
        }
    }

    public void DisableWeaponGrab()
    {
        if (weapon == null) return;

        weapon.enabled = false;
    }

    public void AttachWeapon()
    {
        if (weapon == null) return;

        if (weapon.isSelected)
        {
            var interactor = weapon.firstInteractorSelecting;
            if (interactor != null && weapon.interactionManager != null)
            {
                weapon.interactionManager.SelectExit(interactor, weapon);
            }
        }

        weapon.enabled = false;

        if (weaponRb != null)
        {
            weaponRb.isKinematic = true;
            weaponRb.useGravity = false;
            weaponRb.linearVelocity = Vector3.zero;
            weaponRb.angularVelocity = Vector3.zero;
        }

        weapon.transform.SetParent(handAttachPoint);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }

    public void EnableWeaponGrab()
    {
        if (weapon == null) return;

        weapon.enabled = true;

        if (weaponRb != null)
        {
            weaponRb.isKinematic = true;
            weaponRb.useGravity = false;
        }
    }

    public void DetachWeapon()
    {
        if (weapon == null) return;

        weapon.transform.SetParent(null);

        if (weaponRb != null)
        {
            weaponRb.isKinematic = false;
            weaponRb.useGravity = true;
        }
    }
}
