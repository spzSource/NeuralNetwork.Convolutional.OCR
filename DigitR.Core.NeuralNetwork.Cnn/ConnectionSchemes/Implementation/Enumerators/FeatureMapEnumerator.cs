using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DigitR.Core.NeuralNetwork.Cnn.Primitives;

namespace DigitR.Core.NeuralNetwork.Cnn.ConnectionSchemes.Implementation.Enumerators
{
    internal class FeatureMapEnumerator : IEnumerator<IReadOnlyList<CnnNeuron>>
    {
        private readonly int source2DSize;
        private readonly int kernelSize;
        private readonly int step;

        private readonly IReadOnlyCollection<CnnNeuron> sourceNeurons;
        private readonly CnnNeuron[,] sourceNeurons2D;

        private int currentKernelCenterI;
        private int currentKernelCenterJ;

        public FeatureMapEnumerator(
            int step,
            int kernelSize,
            int source2DSize,
            IReadOnlyCollection<CnnNeuron> sourceNeurons)
        {
            if (sourceNeurons == null)
            {
                throw new ArgumentNullException("sourceNeurons");
            }
            this.source2DSize = source2DSize;
            this.sourceNeurons = sourceNeurons;
            this.kernelSize = kernelSize;
            this.step = step;

            sourceNeurons2D = new CnnNeuron[source2DSize, source2DSize];

            TransformToSource2D();

            currentKernelCenterI = kernelSize / 2;
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

        private void TransformToSource2D()
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

            #region Old code

            //neurons.Add(sourceNeurons2D[currentKernelCenterI, currentKernelCenterJ]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI - 1, currentKernelCenterJ]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI - 2, currentKernelCenterJ]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI + 1, currentKernelCenterJ]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI + 2, currentKernelCenterJ]);

            //neurons.Add(sourceNeurons2D[currentKernelCenterI - 1, currentKernelCenterJ + 1]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI - 2, currentKernelCenterJ + 1]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI, currentKernelCenterJ + 1]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI + 1, currentKernelCenterJ + 1]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI + 2, currentKernelCenterJ + 1]);

            //neurons.Add(sourceNeurons2D[currentKernelCenterI - 1, currentKernelCenterJ + 2]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI - 2, currentKernelCenterJ + 2]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI, currentKernelCenterJ + 2]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI + 1, currentKernelCenterJ + 2]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI + 2, currentKernelCenterJ + 2]);

            //neurons.Add(sourceNeurons2D[currentKernelCenterI - 1, currentKernelCenterJ - 1]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI - 2, currentKernelCenterJ - 1]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI, currentKernelCenterJ - 1]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI + 1, currentKernelCenterJ - 1]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI + 2, currentKernelCenterJ - 1]);

            //neurons.Add(sourceNeurons2D[currentKernelCenterI - 1, currentKernelCenterJ - 2]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI - 2, currentKernelCenterJ - 2]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI, currentKernelCenterJ - 2]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI + 1, currentKernelCenterJ - 2]);
            //neurons.Add(sourceNeurons2D[currentKernelCenterI + 2, currentKernelCenterJ - 2]);

            #endregion

            for (int i = -step; i <= step; i++)
            {
                for (int j = -step; i <= step; i++)
                {
                    neurons.Add(sourceNeurons2D[currentKernelCenterI + i, currentKernelCenterJ + j]);
                }
            }

            return neurons;
        }

        public bool MoveNext()
        {
            Current = new ReadOnlyCollection<CnnNeuron>(GetCurrentKernel());
            currentKernelCenterJ += step;
            if (currentKernelCenterJ <= sourceNeurons2D.GetLength(1))
            {
                currentKernelCenterJ = kernelSize / 2;
                currentKernelCenterI += step;
            }
            return currentKernelCenterI <= sourceNeurons2D.GetLength(0);
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
