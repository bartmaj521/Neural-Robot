using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork
{

    private readonly int[] _layers;

    private float[][] _neurons;

    private float[][][] _weights;

    public NeuralNetwork(int[] layers)
    {
        _layers = new int[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            _layers[i] = layers[i];
        }

        InitNeurons();
        InitWeights();
    }

    public NeuralNetwork(NeuralNetwork networkToCopy)
    {
        _layers = new int[networkToCopy._layers.Length];
        for (int i = 0; i < networkToCopy._layers.Length; i++)
        {
            _layers[i] = networkToCopy._layers[i];
        }

        InitNeurons();
        InitWeights();
        CopyWeights(networkToCopy._weights);

    }

    private void CopyWeights(float[][][] weightsToCopy)
    {
        for (int i = 0; i < weightsToCopy.Length; i++)
        {
            for (int j = 0; j < weightsToCopy[i].Length; j++)
            {
                for (int k = 0; k < weightsToCopy[i][j].Length; k++)
                {
                    _weights[i][j][k] = weightsToCopy[i][j][k];
                }
            }
        }
    }

    private void InitNeurons()
    {
        var neuronsList = new List<float[]>();

        for (int i = 0; i < _layers.Length; i++)
        {
            neuronsList.Add(new float[_layers[i]]);
        }

        _neurons = neuronsList.ToArray();
    }

    private void InitWeights()
    {
        var weightsList = new List<float[][]>();

        for (int i = 1; i < _layers.Length; i++)
        {
            var layerWeightsList = new List<float[]>();

            int neuronsInPreviousLayer = _layers[i - 1];

            for (int j = 0; j < _neurons[i].Length; j++)
            {
                float[] neuronWeights = new float[neuronsInPreviousLayer];

                for (int k = 0; k < neuronsInPreviousLayer; k++)
                {
                    neuronWeights[k] = UnityEngine.Random.Range(-0.5f, 0.5f);
                }

                layerWeightsList.Add(neuronWeights);
            }
            weightsList.Add(layerWeightsList.ToArray());
        }

        _weights = weightsList.ToArray();
    }

    public float[] FeedForward(float[] inputs)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            _neurons[0][i] = inputs[i];
        }

        for (int i = 1; i < _layers.Length; i++)
        {
            for (int j = 0; j < _neurons[i].Length; j++)
            {
                float value = 0.25f;

                for (int k = 0; k < _neurons[i - 1].Length; k++)
                {
                    value += _weights[i - 1][j][k] * _neurons[i - 1][k];
                }

                _neurons[i][j] = (float)Math.Tanh(value);
            }
        }

        return _neurons[_neurons.Length - 1];
    }

    public void Mutate()
    {
        for (int i = 0; i < _weights.Length; i++)
        {
            for (int j = 0; j < _weights[i].Length; j++)
            {
                for (int k = 0; k < _weights[i][j].Length; k++)
                {
                    float weight = _weights[i][j][k];

                    var mutationSelector = new System.Random().Next() % 4;

                    switch (mutationSelector)
                    {
                        case 0:
                            weight *= -1;
                            break;
                        case 1:
                            weight *= UnityEngine.Random.Range(1f, 1.1f);
                            break;
                        case 2:
                            weight *= UnityEngine.Random.Range(0.9f, 1f);
                            break;
                        case 3:
                            weight = UnityEngine.Random.Range(-0.5f, 0.5f);
                            break;
                    }

                    _weights[i][j][k] = weight;
                }
            }
        }
    }
}
