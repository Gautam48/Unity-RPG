using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] float weaponRange = 2f;
        public float WeaponRange { get { return weaponRange; } }
        [SerializeField] float weaponDamage = 10f;
        [SerializeField] bool isRgihtHanded = true;
        public float WeaponDamage { get { return weaponDamage; } }


        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (weaponPrefab != null)
            {
                if (isRgihtHanded)
                {
                    Instantiate(weaponPrefab, rightHand);
                }
                else
                {
                    Instantiate(weaponPrefab, leftHand);
                }
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }
    }
}
