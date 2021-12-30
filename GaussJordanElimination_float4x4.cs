using FrooxEngine;
using FrooxEngine.LogiX;
using BaseX;
using Mehroz;
using System;

namespace MatrixMod
{
    [HiddenNode] // Hide overload from node browser
    [NodeName("Reduced Echelon Form")]
    [NodeOverload("Reduced-Echelon-Form")]
    [Category(new string[] { "LogiX/Math/Matrix" })]

    public sealed class GaussJordanElimination_double4x4 : LogixNode
    {
        public readonly Input<double4x4> LinearEquationMatrix;
        public readonly Input<double4> LinearSolutionMatrix;
        public readonly Output<double4> SolutionMatrix;

        protected override void OnEvaluate()
        {
            Matrix m1 = new Matrix(LinearEquationMatrix.EvaluateRaw().To2DArray());
            Matrix m2 = new Matrix(4, 1);
            m2[0, 0] = new Fraction(LinearSolutionMatrix.EvaluateRaw().x);
            m2[1, 0] = new Fraction(LinearSolutionMatrix.EvaluateRaw().y);
            m2[2, 0] = new Fraction(LinearSolutionMatrix.EvaluateRaw().z);
            m2[3, 0] = new Fraction(LinearSolutionMatrix.EvaluateRaw().w);
            Matrix m3 = Matrix.Concatenate(m1, m2);
            m3 = m3.ReducedEchelonForm();
            // m3.Rows should be 2
            SolutionMatrix.Value = new double4(m3[0, m3.Rows].ToDouble(), m3[1, m3.Rows].ToDouble(), m3[2, m3.Rows].ToDouble(), m3[3, m3.Rows].ToDouble());
        }

        protected override Type FindOverload(NodeTypes connectingTypes)
        {
            if (this.LinearEquationMatrix.IsConnected || this.LinearSolutionMatrix.IsConnected)
            {
                return null;
            }
            Type type;

            // Get type of wire trying to connect to input LinearEquationMatrix
            if (connectingTypes.inputs.TryGetValue("LinearEquationMatrix", out type)) 
            {
                switch (type.Name)
                {
                    case nameof(float2x2):
                        return typeof(MatrixMod.GaussJordanElimination_float2x2);
                    case nameof(float3x3):
                        return typeof(MatrixMod.GaussJordanElimination_float3x3);
                    case nameof(float4x4):
                        return typeof(MatrixMod.GaussJordanElimination_float4x4);
                    case nameof(double2x2):
                        return typeof(MatrixMod.GaussJordanElimination);
                    case nameof(double3x3):
                        return typeof(MatrixMod.GaussJordanElimination_double3x3);
                    case nameof(double4x4):
                        return typeof(MatrixMod.GaussJordanElimination_double4x4);
                }
            }
            if (connectingTypes.inputs.TryGetValue("LinearSolutionMatrix", out type))
            {
                switch(type.Name)
                {
                    case nameof(float2):
                        return typeof(MatrixMod.GaussJordanElimination_float2x2);
                    case nameof(float3):
                        return typeof(MatrixMod.GaussJordanElimination_float3x3);
                    case nameof(float4):
                        return typeof(MatrixMod.GaussJordanElimination_float4x4);
                    case nameof(double2):
                        return typeof(MatrixMod.GaussJordanElimination);
                    case nameof(double3):
                        return typeof(MatrixMod.GaussJordanElimination_double3x3);
                    case nameof(double4):
                        return typeof(MatrixMod.GaussJordanElimination_double4x4);
                }
            }
            return null;
        }
    }
}

