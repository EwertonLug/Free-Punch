using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreePunch.AI
{
    public class NPCManager : MonoBehaviour
    {
        public event Action OnNpcDied;

        [SerializeField] private NPCBase _npcBasePrefab;
        private List<NPCBase> _activeNpcs = new List<NPCBase>();
        private List<NPCBase> _discartedNpcs = new List<NPCBase>();

        public void Initialize(int numberOfNpcs)
        {
            _discartedNpcs = new List<NPCBase>();
            Generate(numberOfNpcs);
        }

        public NPCBase CreateNpc()
        {
            NPCBase npc = Instantiate(_npcBasePrefab);
            npc.DisabeAllPhysics();
            return npc;
        }

        public void OnUpdate()
        {
            _activeNpcs.ForEach((npc)=> {
                if (npc != null && !npc.IsDied)
                {
                    npc.Wander();
                }
            });
        }

        private void HandleNpcTakeDamage(NPCBase npc)
        {
            if (npc.Life <= 0)
            {
                npc.Die();
                HandleNpcDied(npc);
            }
        }

        private void HandleNpcDied(NPCBase npc)
        {
            StartCoroutine(EnableToCollect(npc));
        }

        private IEnumerator EnableToCollect(NPCBase npc)
        {
            yield return new WaitForSeconds(2f);
            _activeNpcs.Remove(npc);
            Destroy(npc.gameObject);
            OnNpcDied?.Invoke();
        }

        private void OnDestroy()
        {
            _activeNpcs.ForEach((npc) =>
            {

                if (npc != null)
                {
                    npc.OnTakeDamage -= HandleNpcDied;
                }
            });
        }
        public void OnStartNewLevel(int levelNpcAmount)
        {
            Generate(levelNpcAmount);
        }

        private void Generate(int npcAmount)
        {
            DestroyDiscardedNpcs();

            _activeNpcs.ForEach((npc) => {
                if (npc != null && npc.IsDied)
                {
                    Destroy(npc.gameObject);
                }
            });

            for (int i = 0; i < npcAmount; i++)
            {
                Vector3 randomSpawnPosition = new Vector3(UnityEngine.Random.Range(-10, 11), 5, UnityEngine.Random.Range(-10, 11));
                NPCBase npc = Instantiate(_npcBasePrefab, randomSpawnPosition, Quaternion.identity);
                npc.Initialize();
                npc.OnTakeDamage += HandleNpcTakeDamage;
                _activeNpcs.Add(npc);
            }

        }

        private void DestroyDiscardedNpcs()
        {
            _discartedNpcs.ForEach((npc) => Destroy(npc.gameObject));
            _discartedNpcs.Clear();
        }

        public void CacheDistardedNpcs(List<NPCBase> discardedNpcs)
        {
            _discartedNpcs.AddRange(discardedNpcs);
        }
    }
}
