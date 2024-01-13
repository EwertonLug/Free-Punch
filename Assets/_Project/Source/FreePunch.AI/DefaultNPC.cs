using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FreePunch.AI
{
    public class DefaultNPC : NPCBase
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        [SerializeField] private Transform _ragdollRoot;

        private List<Rigidbody> _rigidbodiesOfRadoll;
        private List<Collider> _capsuleCollidersOfRadoll;

        public override void Initialize()
        {
            _rigidbody.isKinematic = false;
            _animator.enabled = true;
            _capsuleCollider.enabled = true;
            _rigidbodiesOfRadoll = _ragdollRoot.GetComponentsInChildren<Rigidbody>().ToList();
            _capsuleCollidersOfRadoll = _ragdollRoot.GetComponentsInChildren<Collider>().ToList();
            DisabeRagdoll();
        }

        public override void Die()
        {
            _rigidbody.isKinematic = true;
            _animator.enabled = false;
            _capsuleCollider.enabled = false;

            EnableRagdoll();
        }

        public override void DisabeAllPhysics()
        {
            _rigidbody.isKinematic = true;
            _animator.enabled = false;
            _capsuleCollider.enabled = false;
            _rigidbodiesOfRadoll = _ragdollRoot.GetComponentsInChildren<Rigidbody>().ToList();
            _capsuleCollidersOfRadoll = _ragdollRoot.GetComponentsInChildren<Collider>().ToList();
            DisabeRagdoll();
        }

        public void EnableRagdoll()
        {
           
            _rigidbodiesOfRadoll.ForEach((rgb) => rgb.isKinematic = false);
            _capsuleCollidersOfRadoll.ForEach((col) => col.enabled = true);
        }

        public void DisabeRagdoll()
        {
            
            _rigidbodiesOfRadoll.ForEach((rgb) => rgb.isKinematic = true);
            _capsuleCollidersOfRadoll.ForEach((col) => col.enabled = false);
        }

        
    }
}
