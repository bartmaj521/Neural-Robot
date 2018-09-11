using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class NeuralNetwork
{

    public readonly int[] Layers;

    public float[][] Neurons;

    public float[][][] Weights;

    [JsonConstructor]
    public NeuralNetwork(int[] layers, float[][] neurons, float[][][] weights)
    {
        Layers = layers;
        Neurons = neurons;
        Weights = weights;
    }

    public NeuralNetwork(int[] layers)
    {
        Layers = new int[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            Layers[i] = layers[i];
        }

        InitNeurons();
        InitWeights();
    }

    public NeuralNetwork(NeuralNetwork networkToCopy)
    {
        Layers = new int[networkToCopy.Layers.Length];
        for (int i = 0; i < networkToCopy.Layers.Length; i++)
        {
            Layers[i] = networkToCopy.Layers[i];
        }

        InitNeurons();
        InitWeights();
        CopyWeights(networkToCopy.Weights);

    }

    private void CopyWeights(float[][][] weightsToCopy)
    {
        for (int i = 0; i < weightsToCopy.Length; i++)
        {
            for (int j = 0; j < weightsToCopy[i].Length; j++)
            {
                for (int k = 0; k < weightsToCopy[i][j].Length; k++)
                {
                    Weights[i][j][k] = weightsToCopy[i][j][k];
                }
            }
        }
    }

    private void InitNeurons()
    {
        var neuronsList = new List<float[]>();

        for (int i = 0; i < Layers.Length; i++)
        {
            neuronsList.Add(new float[Layers[i]]);
        }

        Neurons = neuronsList.ToArray();
    }

    private void InitWeights()
    {
        var weightsList = new List<float[][]>();

        for (int i = 1; i < Layers.Length; i++)
        {
            var layerWeightsList = new List<float[]>();

            int neuronsInPreviousLayer = Layers[i - 1];

            for (int j = 0; j < Neurons[i].Length; j++)
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

        Weights = weightsList.ToArray();
    }

    public float[] FeedForward(float[] inputs)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            Neurons[0][i] = inputs[i];
        }

        for (int i = 1; i < Layers.Length; i++)
        {
            for (int j = 0; j < Neurons[i].Length; j++)
            {
                float value = 0.25f;

                for (int k = 0; k < Neurons[i - 1].Length; k++)
                {
                    value += Weights[i - 1][j][k] * Neurons[i - 1][k];
                }

                Neurons[i][j] = (float)Math.Tanh(value);
            }
        }

        return Neurons[Neurons.Length - 1];
    }

    public void Mutate()
    {
        for (int i = 0; i < Weights.Length; i++)
        {
            for (int j = 0; j < Weights[i].Length; j++)
            {
                for (int k = 0; k < Weights[i][j].Length; k++)
                {
                    float weight = Weights[i][j][k];

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

                    Weights[i][j][k] = weight;
                }
            }
        }
    }
}
