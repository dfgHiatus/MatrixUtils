using FrooxEngine;
using FrooxEngine.LogiX;
using BaseX;
using Mehroz;

namespace MatrixMod
{
    [NodeName("Reduced Echelon Form")]
    [NodeOverload("Reduced-Echelon-Form")]
    [Category(new string[] { "LogiX/Math/Matrix" })]

    public sealed class GaussJordanElimination: LogixNode
    {
        public readonly Input<double2x2> LinearEquationMatrix;
        public readonly Input<double2> LinearSolutionMatrix;
        public readonly Output<double2> SolutionMatrix;

        // Code goes here!
        protected override void OnEvaluate()
        {
            Matrix m1 = new Matrix(LinearEquationMatrix.EvaluateRaw().To2DArray());
            Matrix m2 = new Matrix(2, 1);
            m2[0, 0] = new Fraction(LinearSolutionMatrix.EvaluateRaw().x);
            m2[1, 0] = new Fraction(LinearSolutionMatrix.EvaluateRaw().y);
            Matrix m3 = Matrix.Concatenate(m1, m2);
            m3 = m3.ReducedEchelonForm();
            SolutionMatrix.Value = new double2(m3[0, 2].ToDouble(), m3[1, 2].ToDouble());
        }
    }
}

