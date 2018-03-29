using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts {
    public interface IProxyColliderListener {
        void OnProxyCollisionEnter(Collision col);
        void OnProxyCollisionStay(Collision col);
        void OnProxyCollisionExit(Collision col);
    }
}
