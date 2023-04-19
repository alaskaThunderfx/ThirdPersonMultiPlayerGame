using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Combat.Targeting
{
    public class Targeter : MonoBehaviour
    {
        // Serialized fields
        // Targeting group associated with the targeting state camera
        [SerializeField] private CinemachineTargetGroup cineTargetGroup;

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
            // Subscribes the RemoveTarget method to the OnDestroyedEvent
            target.OnDestroyedEvent += RemoveTarget;
        }


        private void OnTriggerExit(Collider other)
        {
            // Check to see if the object that has entered the Targeter collider contains the Target script and if it 
            // does, returns a variable named target that is then removed from the targets List. Otherwise, it just
            // returns.
            if (!other.TryGetComponent<Target>(out var target)) return;
            RemoveTarget(target);
        }

        // Method meant to select the proper target when entering the Targeting state
        public bool SelectTarget()
        {
            // Checks that anything exists in the targets list, if nothing does it returns
            if (Targets.Count == 0) return false;

            // Sets the current target to the first target in the Targets list
            CurrentTarget = Targets[0];
            // Uses Cinemachines method that will add a targeted object to the Targeting Group for the Targeting camera
            cineTargetGroup.AddMember(CurrentTarget.transform, 1, 2);

            return true;
        }

        // Method to cancel the Targeting state
        public void Cancel()
        {
            // makes sure CurrentTarget has a value
            if (!CurrentTarget) return;

            // Removes targeted object from the target group when the state is ended
            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            // Removes value from CurrentTarget variable
            CurrentTarget = null;
        }

        // Method called when the target is Destroyed, meant to remove the target from the Cinemachine's Target Group
        private void RemoveTarget(Target target)
        {
            // Checks if the CurrentTarget is equal to the target being being passed in
            if (CurrentTarget == target)
            {
                // if it is it Removes the target from CineTargetGroup
                cineTargetGroup.RemoveMember(CurrentTarget.transform);
                // Sets CurrentTarget to null
                CurrentTarget = null;
            }

            // Unsubscribes the RemoveTarget method from the OnDestroyedEvent
            target.OnDestroyedEvent -= RemoveTarget;
            // Removes target from the Targets list
            Targets.Remove(target);
        }
    }
}