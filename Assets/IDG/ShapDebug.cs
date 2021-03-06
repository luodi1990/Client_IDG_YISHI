using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IDG
{
    public static class ShapDebug
    {
        public static void Draw(ShapBase shap,Color color,float time=0.1f){
            for (int i = 0; i < shap.PointsCount-1; i++)
            {
               UnityEngine.Debug.DrawLine((shap.position+ shap.GetPoint(i)).ToVector3(),(shap.position +shap.GetPoint(i+1)).ToVector3(),color,time);
            }
            UnityEngine.Debug.DrawLine((shap.position+ shap.GetPoint(shap.PointsCount-1)).ToVector3(),(shap.position +shap.GetPoint(0)).ToVector3(),color,time);
        }
    }
}