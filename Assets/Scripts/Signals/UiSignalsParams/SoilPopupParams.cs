using System.Collections.Generic;
using Service;
using UnityEngine;

namespace DefaultNamespace.Signals.UiSignalsParams
{
    public class SoilPopupParams
    {
        public List<Vector3Int> Positions;

        public SoilPopupParams(List<Vector3Int> positions)
        {
            Positions = positions;
        }
    }
}