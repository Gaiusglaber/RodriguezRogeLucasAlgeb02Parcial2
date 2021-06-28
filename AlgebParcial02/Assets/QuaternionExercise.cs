using System.Collections.Generic;
using UnityEngine;

namespace EjerciciosAlgebra
{
    public class QuaternionExercise : MonoBehaviour
    {
        public enum Ejercicio
        {
            Uno,
            Dos,
            Tres,
        }
        public Ejercicio num;
        public float angle;

        private void Start()
        {
            VectorDebugger.EnableCoordinates();
            VectorDebugger.EnableEditorView();
            VectorDebugger.AddVector(new Vector3(10f, 0.0f, 0.0f), Color.red, "V1");
            List<Vector3> positions1 = new List<Vector3>();
            positions1.Add(new Vector3(10f, 0.0f, 0.0f));
            positions1.Add(new Vector3(10f, 10f, 0.0f));
            positions1.Add(new Vector3(20f, 10f, 0.0f));
            VectorDebugger.AddVectorsSecuence(positions1, false, Color.blue, "V2");
            List<Vector3> positions2 = new List<Vector3>();
            positions2.Add(new Vector3(10f, 0.0f, 0.0f));
            positions2.Add(new Vector3(10f, 10f, 0.0f));
            positions2.Add(new Vector3(20f, 10f, 0.0f));
            positions2.Add(new Vector3(20f, 20f, 0.0f));
            VectorDebugger.AddVectorsSecuence(positions2, false, Color.yellow, "V3");
        }

        private void FixedUpdate()
        {
            VectorDebugger.TurnOffVector("V1");
            VectorDebugger.DisableEditorView("V1");
            VectorDebugger.TurnOffVector("V2");
            VectorDebugger.DisableEditorView("V2");
            VectorDebugger.TurnOffVector("V3");
            VectorDebugger.DisableEditorView("V3");
            switch (num)
            {
                case Ejercicio.Uno:
                    VectorDebugger.TurnOnVector("V1");
                    VectorDebugger.EnableEditorView("V1");
                    List<Vector3> newPositions1 = new List<Vector3>();
                    for (int index = 0; index < VectorDebugger.GetVectorsPositions("V1").Count; ++index)
                        newPositions1.Add(Quaternioncito.Euler(new Vector3(0.0f, angle, 0.0f)) * VectorDebugger.GetVectorsPositions("V1")[index]);
                    VectorDebugger.UpdatePositionsSecuence("V1", newPositions1);
                    break;
                case Ejercicio.Dos:
                    VectorDebugger.TurnOnVector("V2");
                    VectorDebugger.EnableEditorView("V2");
                    List<Vector3> newPositions2 = new List<Vector3>();
                    for (int index = 0; index < VectorDebugger.GetVectorsPositions("V2").Count; ++index)
                        newPositions2.Add((Quaternioncito.Euler(new Vector3(0.0f, angle, 0.0f)) * VectorDebugger.GetVectorsPositions("V2")[index]));
                    VectorDebugger.UpdatePositionsSecuence("V2", newPositions2);
                    break;
                case Ejercicio.Tres:
                    VectorDebugger.TurnOnVector("V3");
                    VectorDebugger.EnableEditorView("V3");
                    List<Vector3> newPositions3 = new List<Vector3>();
                    newPositions3.Add(VectorDebugger.GetVectorsPositions("V3")[0]);
                    newPositions3.Add((Quaternioncito.Euler(new Vector3(angle, angle, 0.0f)) * VectorDebugger.GetVectorsPositions("V3")[1]));
                    newPositions3.Add(VectorDebugger.GetVectorsPositions("V3")[2]);
                    newPositions3.Add((Quaternioncito.Euler(new Vector3(-angle, -angle, 0.0f)) * VectorDebugger.GetVectorsPositions("V3")[3]));
                    newPositions3.Add(VectorDebugger.GetVectorsPositions("V3")[4]);
                    VectorDebugger.UpdatePositionsSecuence("V3", newPositions3);
                    break;
            }
        }
    }
}
