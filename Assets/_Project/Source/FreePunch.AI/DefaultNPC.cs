using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace FreePunch.AI
{
    public class DefaultNPC : NPCBase
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        [SerializeField] private Transform _ragdollRoot;
        [SerializeField] private NavMeshAgent _navMeshAgent;

        [SerializeField] private float _wanderRadius = 10;
        private float _wanderRate=3f;
        private float _currentWanderRate;

        private List<Rigidbody> _rigidbodiesOfRadoll;
        private List<Collider> _capsuleCollidersOfRadoll;
        private Vector3 wanderTarget = Vector3.zero;
        private bool _isDied;
        public override bool IsDied => _isDied;

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
            _navMeshAgent.enabled = false;
            _isDied = true;
            EnableRagdoll();
        }

        public override void Wander()
        {
            _currentWanderRate += Time.deltaTime;
            if (_currentWanderRate > _wanderRate)
            {
                Vector3 randomDirection = Random.insideUnitSphere * _wanderRadius;
                randomDirection += transform.position;
                if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _wanderRadius, 1))
                {
                    wanderTarget = hit.position;
                }
                _currentWanderRate = 0;
            }

            Seek(wanderTarget);
        }

        private void Seek(Vector3 location)
        {
            _navMeshAgent.SetDestination(location);
        }

        public override void DisabeAllPhysics()
        {
            _rigidbody.isKinematic = true;
            _animator.enabled = false;
            _capsuleCollider.enabled = false;
            _navMeshAgent.enabled = false;
            _rigidbodiesOfRadoll = _ragdollRoot.GetComponentsInChildren<Rigidbody>().ToList();
            _capsuleCollidersOfRadoll = _ragdollRoot.GetComponentsInChildren<Collider>().ToList();
            DisabeRagdoll();
        }
        public override  void EnableRagdoll()
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
