using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Enumerators
{
    internal class FeatureMapEnumerator : IEnumerator<IReadOnlyList<CnnNeuron>>
    {
        private const int Step = 2;
        private const int KernelSize = 5;

        private readonly int source2DSize;
        private readonly int kernelSize;
        private readonly int step;
        private readonly IReadOnlyList<CnnNeuron> sourceNeurons;
        private readonly CnnNeuron[,] sourceNeurons2D;

        private int currentKernelCenterI = 0;
        private int currentKernelCenterJ = 0;

        public FeatureMapEnumerator(
            int source2DSize,
            IReadOnlyList<CnnNeuron> sourceNeurons)
        {
            if (sourceNeurons == null)
            {
                throw new ArgumentNullException("sourceNeurons");
            }
            this.source2DSize = source2DSize;
            this.sourceNeurons = sourceNeurons;
            sourceNeurons2D = new CnnNeuron[source2DSize, source2DSize];

            PrepareSource2D();

            currentKernelCenterI = KernelSize / 2;
            currentKernelCenterJ = currentKernelCenterI;
        }

        public IReadOnlyList<CnnNeuron> Current
        {
            get;
            private set;
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        private void PrepareSource2D()
        {
            int i = 0;
            int j = 0;
            int batchCount = 0;

            foreach (CnnNeuron neuron in sourceNeurons)
            {
                if (batchCount == source2DSize)
                {
                    sourceNeurons2D[i, j] = neuron;
                    ++i;
                    batchCount = 0;
                }
                ++j;
                ++batchCount;
            }
        }

        private IList<CnnNeuron> GetCurrentKernel()
        {
            IList<CnnNeuron> neurons = new List<CnnNeuron>();

            neurons.Add(sourceNeurons2D[currentKernelCenterI, currentKernelCenterJ]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI - 1, currentKernelCenterJ]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI - 2, currentKernelCenterJ]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI + 1, currentKernelCenterJ]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI + 2, currentKernelCenterJ]);

            neurons.Add(sourceNeurons2D[currentKernelCenterI - 1, currentKernelCenterJ + 1]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI - 2, currentKernelCenterJ + 1]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI, currentKernelCenterJ + 1]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI + 1, currentKernelCenterJ + 1]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI + 2, currentKernelCenterJ + 1]);

            neurons.Add(sourceNeurons2D[currentKernelCenterI - 1, currentKernelCenterJ + 2]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI - 2, currentKernelCenterJ + 2]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI, currentKernelCenterJ + 2]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI + 1, currentKernelCenterJ + 2]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI + 2, currentKernelCenterJ + 2]);

            neurons.Add(sourceNeurons2D[currentKernelCenterI - 1, currentKernelCenterJ - 1]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI - 2, currentKernelCenterJ - 1]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI, currentKernelCenterJ - 1]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI + 1, currentKernelCenterJ - 1]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI + 2, currentKernelCenterJ - 1]);

            neurons.Add(sourceNeurons2D[currentKernelCenterI - 1, currentKernelCenterJ - 2]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI - 2, currentKernelCenterJ - 2]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI, currentKernelCenterJ - 2]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI + 1, currentKernelCenterJ - 2]);
            neurons.Add(sourceNeurons2D[currentKernelCenterI + 2, currentKernelCenterJ - 2]);

            return neurons;
        }

        public bool MoveNext()
        {
            Current = new ReadOnlyCollection<CnnNeuron>(GetCurrentKernel());
            currentKernelCenterJ += Step;
            if (currentKernelCenterJ <= sourceNeurons2D.GetLength(1))
            {
                currentKernelCenterJ = KernelSize / 2;
                currentKernelCenterI += Step;
            }
            return currentKernelCenterI <= sourceNeurons2D.GetLength(0);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
