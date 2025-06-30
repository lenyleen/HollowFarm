using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DefaultNamespace.ViewModels;
using Service;
using UnityEngine;

namespace DefaultNamespace.Handlers
{
    public interface ISoilMenuSelector
    {
        public void SelectSoilMenu(Dictionary<Vector3Int, SoilViewModel> soilVmData);
        
        public UniTask<SoilFilter> OpenFilterDialogMenu(Vector3 position);
    }
}