using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Car", menuName = "Picker3D/CD_Car", order = 0)]
    public class CD_Car : ScriptableObject
    {
        public CarData Data;
    }
}