using System.Collections.Generic;
using UnityEngine;

namespace Combat.Targeting
{
    public class Targeter : MonoBehaviour
    {
        // List storing objects that meet the criteria of being Target type
        private readonly List<Target> Targets = new();

        // Stores the current target
        public Target CurrentTarget { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            // Check to see if the object that has entered the Targeter collider contains the Target script and if it 
            // does, returns a Target variable named target and adds it to the targets List, otherwise, it just returns.
            if (!other.TryGetComponent<Target>(out var target)) return;
            Targets.Add(target);
        }

        private void OnTriggerExit(Collider other)
        {
            // Check to see if the object that has entered the Targeter collider contains the Target script and if it 
            // does, returns a variable named target that is then removed from the targets List. Otherwise, it just
            // returns.
            if (!other.TryGetComponent<Target>(out var target)) return;
            Targets.Remove(target);
        }

        // Method meant to select the proper target when entering the Targeting state
        public bool SelectTarget()
        {
            // Checks that anything exists in the targets list, if nothing does it returns
            if (Targets.Count == 0) return false;

            // Sets the current target to the first target in the Targets list
            CurrentTarget = Targets[0];

            return true;
        }

        // Method to cancel the Targeting state
        public void Cancel()
        {
            // Removes value from CurrentTarget variable
            CurrentTarget = null;
        }
    }
}