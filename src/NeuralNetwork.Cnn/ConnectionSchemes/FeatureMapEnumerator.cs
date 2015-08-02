using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using DigitR.Core.NeuralNetwork.Primitives;

namespace DigitR.NeuralNetwork.Cnn.ConnectionSchemes
{
    internal class FeatureMapEnumerator : IEnumerator<IReadOnlyList<INeuron<double>>>
    {
        private readonly int source2DSize;
        private readonly int kernelSize;
        private readonly int step;

        private readonly IReadOnlyCollection<INeuron<double>> sourceNeurons;
        private readonly INeuron<double>[,] sourceNeurons2D;

        private int currentKernelCenterI;
        private int currentKernelCenterJ;

        public FeatureMapEnumerator(
            int step,
            int kernelSize,
            int source2DSize,
            IReadOnlyCollection<INeuron<double>> sourceNeurons)
        {
            if (sourceNeurons == null)
            {
                throw new ArgumentNullException(nameof(sourceNeurons));
            }
            this.source2DSize = source2DSize;
            this.sourceNeurons = sourceNeurons;
            this.kernelSize = kernelSize;
            this.step = step;

            sourceNeurons2D = new INeuron<double>[source2DSize, source2DSize];

            TransformToSource2D();

            currentKernelCenterI = kernelSize / 2;
            currentKernelCenterJ = currentKernelCenterI;
        }

        public IReadOnlyList<INeuron<double>> Current
        {
            get;
            private set;
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        private void TransformToSource2D()
        {
            int i = 0;
            int j = 0;
            int batchCount = 0;

            foreach (INeuron<double> neuron in sourceNeurons)
            {
                if (batchCount == source2DSize)
                {
                    ++i;
                    batchCount = 0;
                    j = 0;
                }
                sourceNeurons2D[i, j] = neuron;
                ++j;
                ++batchCount;
            }
        }

        private IList<INeuron<double>> GetCurrentKernel()
        {
            IList<INeuron<double>> neurons = new List<INeuron<double>>();

            for (int i = -step; i <= step; i++)
            {
                for (int j = -step; j <= step; j++)
                {
                    neurons.Add(sourceNeurons2D[currentKernelCenterI + i, currentKernelCenterJ + j]);
                }
            }

            return neurons;
        }

        public bool MoveNext()
        {
            bool notIsLast = currentKernelCenterI < sourceNeurons2D.GetLength(0) - 1;

            if (notIsLast)
            {
                Current = new ReadOnlyCollection<INeuron<double>>(GetCurrentKernel());
                currentKernelCenterJ += step;

                if (currentKernelCenterJ >= sourceNeurons2D.GetLength(1) - 1)
                {
                    currentKernelCenterJ = kernelSize / 2;
                    currentKernelCenterI += step;
                }
            }

            return notIsLast;
        }

        public void Reset()
        {
            currentKernelCenterJ = kernelSize / 2;
            currentKernelCenterI = currentKernelCenterJ;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
