using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class Thruster : MonoBehaviour, IShipInjectable
    {
        [SerializeField] private List<ShipInputAction> targetActions;
        [SerializeField] private MeshRenderer activeRenderer;
        [SerializeField] private MeshRenderer idleRenderer;
        
        private Ship _ship;

        public void InjectShip(Ship ship)
        {
            _ship = ship;
        }

        private void Update()
        {
            bool isRequestedByAction = targetActions.Any(ShipInput.GetAction);
            
            SetActiveRenderer(isRequestedByAction);

            if (!isRequestedByAction) 
                return;
            
            Thrust();
        }

        private void Thrust()
        {
            Transform t = transform;
            Vector3 force = t.forward * (_ship.Power * Time.deltaTime);
            Vector3 position = t.position;
            _ship.Rigidbody.AddForceAtPosition(force, position);
        }

        private void SetActiveRenderer(bool isActive)
        {
            activeRenderer.enabled = isActive;
            idleRenderer.enabled = !isActive;
        }
    }
}
