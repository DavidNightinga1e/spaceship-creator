using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class Thruster : MonoBehaviour, IShipInjectable
    {
        [SerializeField] private List<ShipInputAction> targetActions;
        
        private Ship _ship;

        public void InjectShip(Ship ship)
        {
            _ship = ship;
        }

        private void Update()
        {
            if (targetActions.Any(ShipInput.GetAction)) 
                _ship.Rigidbody.AddForceAtPosition(transform.forward * _ship.Power, transform.position);
        }
    }
}
