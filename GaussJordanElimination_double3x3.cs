using FrooxEngine;
using FrooxEngine.LogiX;
using BaseX;
using Mehroz;

namespace MatrixMod
{
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
    }
}

