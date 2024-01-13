using FreePunch.AI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FreePunch.Player
{
    public class PlayerBackStack : MonoBehaviour
    {
        [SerializeField] private float _distanceBeetween;
        [SerializeField] private float _inertiaSpeed;

        private Stack<NPCBase> _npcsStack = new Stack<NPCBase>();
        private List<Transform> _slots;

        private int _avaliableSlotIndex = 0;
        private GameObject _pivot;
        public void Setup(int maxSlots)
        {
            CreatePivot();
            CreateSlots(maxSlots);
        }

        private void CreateSlots(int maxSlots)
        {
            _slots = new List<Transform>();
            _npcsStack = new Stack<NPCBase>(maxSlots);

            for (int i = 0; i < maxSlots; i++)
            {
                GameObject slot = new($"Slot=>{i}");
                GameObject child = new($"Child=>{i}");
                child.transform.SetParent(slot.transform, false);
                child.transform.localPosition = new Vector3(-0.7f, 0, 0);
                child.transform.localEulerAngles = new Vector3(90, slot.transform.eulerAngles.y, -90f);
                slot.transform.position = _pivot.transform.position;

                if(i == 0)
                {
                    AdjustPosition(_pivot.transform, slot.transform, _distanceBeetween);
                    AdjustRotation(_pivot.transform, slot.transform);
                }
                _slots.Add(slot.transform);
            }
        }

        public int Size()
        {
            return _npcsStack.Count;
        }

        private void CreatePivot()
        {
            _pivot = new($"Pivot");
            _pivot.transform.SetParent(transform);
            _pivot.transform.localPosition = Vector3.zero;
        }

        public void AddNpc(NPCBase npc)
        {
            Transform avaliableSlot = _slots[_avaliableSlotIndex];
            npc.transform.SetParent(avaliableSlot,false);
            npc.transform.position = avaliableSlot.GetChild(0).transform.position;
            npc.transform.rotation = avaliableSlot.GetChild(0).transform.rotation;

            _npcsStack.Push(npc);
            _avaliableSlotIndex++;
        }

        public void RemoveAllNpcs()
        {
            foreach (NPCBase npc in _npcsStack)
            {
                npc.transform.SetParent(null);
                npc.EnableRagdoll();
            }
            _npcsStack.Clear();
            _avaliableSlotIndex = 0;
        }

        public void OnUpdate()
        {
            if (_slots.Count <= 1)
            {
                return;
            }

            for (int i = 0; i < _slots.Count; i++)
            {
                Transform target = _pivot.transform;
                Transform slot = _slots[i];

                if (i > 0)
                {
                    target = _slots[i - 1];
                }

                AdjustPosition(target, slot, _distanceBeetween);
                AdjustRotation(target, slot);

            }

        }

        private void AdjustRotation(Transform target, Transform slot)
        {
            Vector3 lookPos = target.position - slot.position;
            slot.rotation = Quaternion.LookRotation(lookPos, transform.forward);
            slot.rotation = Quaternion.LookRotation(slot.up, slot.forward);
        }

        private void AdjustPosition(Transform target, Transform slot, float distanceBeetween)
        {
            Vector3 newPos = Vector3.Lerp(slot.position, target.position, _inertiaSpeed * Time.deltaTime);
            slot.position = new Vector3(newPos.x, target.position.y + distanceBeetween, newPos.z);
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Gizmos.color = Color.red;

            if (_slots == null)
            {
                return;
            }

            for (int i = 0; i < _slots.Count; i++)
            {

                Gizmos.DrawSphere(_slots[i].position, .4f);
            }

#endif
        }
    }
}
