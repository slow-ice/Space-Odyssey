using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utility.Cursor {
    public class GetCursor : MonoBehaviour {

        private void OnEnable() {
            UnityEngine.Cursor.visible = true;
        }
    }
}
