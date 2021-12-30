using FrooxEngine;
using FrooxEngine.LogiX;
using BaseX;
using Mehroz;
using System;

namespace MatrixMod
{
    [HiddenNode] // Hide overload from node browser
    [NodeName("Reduced Echelon Form")]
    [NodeOverload("GaussJordanElimination")]
    [Category(new string[] { "LogiX/Math/Matrix" })]

    public sealed class GaussJordanElimination_double3x3 : LogixNode
    {
        public readonly Input<double3x3> LinearEquationMatrix;
        public readonly Input<double3> LinearSolutionMatrix;
        public readonly Output<double3> SolutionMatrix;

        // Code goes here!
        protected override void OnEvaluate()
        {
            Matrix m1 = new Matrix(LinearEquationMatrix.EvaluateRaw().To2DArray());
            Matrix m2 = new Matrix(3, 1);
            m2[0, 0] = new Fraction(LinearSolutionMatrix.EvaluateRaw().x);
            m2[1, 0] = new Fraction(LinearSolutionMatrix.EvaluateRaw().y);
            m2[2, 0] = new Fraction(LinearSolutionMatrix.EvaluateRaw().z);
            Matrix m3 = Matrix.Concatenate(m1, m2);
            m3 = m3.ReducedEchelonForm();
            SolutionMatrix.Value = new double3(m3[0, 2].ToDouble(), m3[1, 2].ToDouble(), m3[2, 2].ToDouble());
        }

        protected override Type FindOverload(NodeTypes connectingTypes)
        {
            if (this.LinearEquationMatrix.IsConnected || this.LinearSolutionMatrix.IsConnected) // If any input is connected skip
            {
                return null;
            }
            Type type;
            if (connectingTypes.inputs.TryGetValue("LinearEquationMatrix", out type)) // Get type of wire trying to connect to input LinearEquationMatrix
            {
                if (type == typeof(BaseX.double2x2)) // This would perferably be done in some generic way
                {
                    return typeof(MatrixMod.GaussJordanElimination);  // Return 2x2
                }
            }
            if (connectingTypes.inputs.TryGetValue("LinearSolutionMatrix", out type)) // Get type of wire trying to connect to input LinearSolutionMatrix
            {
                if (type == typeof(BaseX.double2))
                {
                    return typeof(MatrixMod.GaussJordanElimination); // Return 2x2
                }
            }
            return null;
        }
    }
}

