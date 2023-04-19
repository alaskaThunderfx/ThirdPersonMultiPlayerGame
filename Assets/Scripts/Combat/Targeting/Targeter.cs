using System.Collections.Generic;
using UnityEngine;

namespace Combat.Targeting
{
    public class Targeter : MonoBehaviour
    {
        // List storing objects that meet the criteria of being Target type
        public List<Target> targets = new();

        private void OnTriggerEnter(Collider other)
        {
            // Check to see if the object that has entered the Targeter collider contains the Target script and if it 
            // does, returns a Target variable named target and adds it to the targets List, otherwise, it just returns.
            if (!other.TryGetComponent<Target>(out var target)) return;
            targets.Add(target);
        }

        private void OnTriggerExit(Collider other)
        {
            // Check to see if the object that has entered the Targeter collider contains the Target script and if it 
            // does, returns a variable named target that is then removed from the targets List. Otherwise, it just
            // returns.
            if (!other.TryGetComponent<Target>(out var target)) return;
            targets.Remove(target);
        }
    }
}