using FreePunch.Player;
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
        [SerializeField] private List<NPCBase> _activeNpcs;

        public void Initialize()
        {
            _activeNpcs.ForEach((npc) =>
            {

                if (npc != null)
                {
                    npc.Initialize();
                    npc.OnTakeDamage += HandleNpcTakeDamage;
                }
            });
        }

        public NPCBase CreateNpc()
        {
            NPCBase npc = Instantiate(_npcBasePrefab);
            npc.DisabeAllPhysics();
            return npc;
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
    }
}
