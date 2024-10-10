    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    public class NeuralModel
    {
    private double[] rewards = { +3, -0.1, -3 };
    private int actionNeuro = 4; // Número de posibles acciones
    private int envPlace = 24; // Número de características del entorno
    private int rewNeuro = 3; // Número de posibles recompensas
    private double[,] weightLayerInp;
    private double[,] weightLayerOut;
    private double bias = 0.5;

    public NeuralModel()
    {
        weightLayerInp = new double[envPlace, actionNeuro];
        weightLayerOut = new double[actionNeuro, rewNeuro];
    }

    public void loadTrained(double[,] weightInp, double[,] weigthOut)
    {
        this.weightLayerInp = weightInp;
        this.weightLayerOut = weigthOut;
    }

    public void initNeuroNetwork()
    {
        System.Random rad = new System.Random();
        for (int i = 0; i < envPlace; i++)
        {
            for (int j = 0; j < actionNeuro; j++)
            {
                weightLayerInp[i, j] = Math.Round(rad.NextDouble(), 3);
            }
        }
        for (int i = 0; i < actionNeuro; i++)
        {
            for (int j = 0; j < rewNeuro; j++)
            {
                weightLayerOut[i, j] = Math.Round(rad.NextDouble(), 3);
            }
        }
    }

    public void trainStep(int[] vectorInput, int action, int rewardExpect, int reward)
    {  
        double q_value=0;
       try{ //Debug.Log("reward es "+rewardExpect);
         q_value = q_ecuation(rewards[rewardExpect], reward);
       }catch(Exception e){
         
        Debug.Log("error con el "+rewardExpect);

       }
        for (int i = 0; i < envPlace; i++)
        {
            if (vectorInput.Contains(i))
            {
                weightLayerInp[i, action] += q_value;
            }
        }

        // Actualizar pesos en la capa de acción a recompensa
        for (int j = 0; j < rewNeuro; j++)
        {
            weightLayerOut[action, j] += q_value;
        }
    }

    public (int action, int reward, int[] vectorInput) selectAction(int[] vectorInput)
    {
        int action = choose_action(vectorInput);
        int reward = choose_reward_espected(action);

        return (action, reward, vectorInput);
    }

    public int choose_action(int[] vectorInput)
    {
        int actionChoose = -1;
        double heavier = double.NegativeInfinity;

        for (int j = 0; j < actionNeuro; j++)
        {
            double sum = 0;
            for (int i = 0; i < envPlace; i++)
            {
                if (vectorInput.Contains(i))
                {
                    sum += weightLayerInp[i, j];
                }
            }

            if (sum > heavier)
            {
                heavier = sum;
                actionChoose = j;
            }
            else if (sum == heavier)
            {
                System.Random rand = new System.Random();
                if (rand.Next(2) == 1) actionChoose = j;
            }
        }

        if (actionChoose == -1) actionChoose = 0; // Acción por defecto

        return actionChoose;
    }

    public int choose_reward_espected(int actionChoose)
    {
        int rewardChoose = -1;
        double heaviest = 0;
        for (int j = 0; j < rewNeuro; j++)
        {
            double sum = 0;
            for (int i = 0; i < actionNeuro; i++)
            {
                if (actionChoose == i)
                {
                    sum += weightLayerOut[i, j];
                }
            }
            if (sum > heaviest)
            {
                heaviest = sum;
                rewardChoose = j;
            }
            else if (sum == heaviest && rewardChoose != -1)
            {
                System.Random rand = new System.Random();
                int randomNum = rand.Next(2);
                if (randomNum == 1) rewardChoose = j;
            }
        }
        //Debug.Log("última recompensa: " + rewardChoose);
        return rewardChoose;
    }
    

    public double activation(double sum)
    {
        return Math.Tanh(sum + bias) >= 1 ? 1 : Math.Tanh(sum + bias);
    }

    private double q_ecuation(double rewExpect, int reward)
    {
        double gamma = 0.5; // γ factor de descuento
        double alpha = 0.1; // α tasa de aprendizaje

        double qvalue = (double)rewExpect + alpha * ((double)reward + gamma * 3 - (double)rewExpect);
        return rewExpect < 0 && reward < 0 ? -(qvalue - rewExpect) : qvalue - rewExpect;
    }

    public void printLayer()
    {
        for (int i = 0; i < envPlace; i++)
        {
            string msj = "";
            for (int j = 0; j < actionNeuro; j++)
            {
                msj += weightLayerInp[i, j] + " ";
            }
            Debug.Log(msj);
        }
    }

    // Conversiones de matriz
    public string ConvertMatrixToString(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        string result = "";

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result += matrix[i, j];
                if (j < cols - 1) result += "-";
            }
            if (i < rows - 1) result += "/";
        }

        return result;
    }

    public int[,] ConvertStringToMatrix(string matrixString)
    {
        string[] rows = matrixString.Split('/');
        int rowCount = rows.Length;
        int colCount = rows[0].Split('-').Length;
        int[,] matrix = new int[rowCount, colCount];

        for (int i = 0; i < rowCount; i++)
        {
            string[] cols = rows[i].Split('-');
            for (int j = 0; j < colCount; j++)
            {
                matrix[i, j] = int.Parse(cols[j]);
            }
        }

        return matrix;
    }
}