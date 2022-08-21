using System;
using System.Collections.Generic;
using System.Linq;
using Assembly.ShipParts;
using UnityEngine;

namespace Assembly
{
	[CreateAssetMenu(menuName = "ShipPartPrefabHandler", fileName = "ShipParts")]
	public class ShipPartPrefabHandler : ScriptableObject
	{
		public List<ShipPart> shipParts;
		
		private static ShipPartPrefabHandler _instance;

		private Dictionary<Type, ShipPart> _partTypeToPrefab;

		public static ShipPartPrefabHandler Instance
		{
			get
			{
				if (_instance is not null)
					return _instance;

				_instance = Resources.Load<ShipPartPrefabHandler>("ShipParts");
				// ReSharper disable once Unity.NoNullCoalescing
				if (_instance)
				{
					_instance.PrepareParts();
					return _instance;
				}

				const string exceptionMessage = "Scriptable object \"ShipParts\" not found in Resources";
				throw new Exception(exceptionMessage);
			}
		}

		private void PrepareParts()
		{
			_partTypeToPrefab = shipParts.ToDictionary(k => k.GetType(), v => v);
		}

		public GameObject GetPrefab<T>() where T : ShipPart
		{
			return _partTypeToPrefab[typeof(T)].gameObject;
		}
	}
}