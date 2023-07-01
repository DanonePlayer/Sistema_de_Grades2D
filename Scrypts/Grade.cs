using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
using System;

public class Grade<TGridObject>
{
    public const int HEAT_MAP_MAX_VALUE = 100;
    public const int HEAT_MAP_MIN_VALUE = 0;

    public event EventHandler<OnGriValueChangedEventArgs> OnGridValueChanged;
    public class OnGriValueChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int largura;
    private int altura;
    private Vector3 originPosition;
    private float tamanho_celula;
    private TGridObject[,] matriz_Grade;

    public Grade(int largura, int altura, float tamanho_celula, Vector3 originPosition)
    {
        this.largura = largura;
        this.altura = altura;
        this.tamanho_celula = tamanho_celula;
        this.originPosition = originPosition;

        matriz_Grade = new TGridObject[largura, altura];

        bool ShowDebug = true;
        if(ShowDebug)
        {
            TextMesh[,] debugTextMatriz = new TextMesh[largura, altura];

            for (int x = 0; x < matriz_Grade.GetLength(0); x++)
            {
                for (int y = 0; y < matriz_Grade.GetLength(1); y++)
                {
                    debugTextMatriz[x, y] = UtilsClass.CreateWorldText(matriz_Grade[x, y].ToString(), null, ObterPosiçãoMundial(x, y) + new Vector3(tamanho_celula, tamanho_celula) * .5f, 30, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(ObterPosiçãoMundial(x, y), ObterPosiçãoMundial(x, y + 1), Color.white, 100f);
                    Debug.DrawLine(ObterPosiçãoMundial(x, y), ObterPosiçãoMundial(x + 1, y), Color.white, 100f);
                }
            }
            Debug.DrawLine(ObterPosiçãoMundial(0, altura), ObterPosiçãoMundial(largura, altura), Color.white, 100f);
            Debug.DrawLine(ObterPosiçãoMundial(largura, 0), ObterPosiçãoMundial(largura, altura), Color.white, 100f);

            OnGridValueChanged += (object sender, OnGriValueChangedEventArgs eventArgs) => {
                debugTextMatriz[eventArgs.x, eventArgs.y].text = matriz_Grade[eventArgs.x, eventArgs.y].ToString();
            };
        }
    }

    public int PegarLargura()
    {
        return largura;
    }

    public int PegarAltura()
    {
        return altura;
    }

    public float PegarTamanhoDaCelula()
    {
        return tamanho_celula;
    }

    private Vector3 ObterPosiçãoMundial(int x, int y)
    {
        return new Vector3(x, y) * tamanho_celula + originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / tamanho_celula);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / tamanho_celula);
    }

    public void DefinirValor(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < largura && y < altura)
        {
            matriz_Grade[x, y] = value;
            if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGriValueChangedEventArgs { x = x, y = y });
        }
    }

    public void DefinirValor(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        DefinirValor(x, y, value);
    }

    public TGridObject Pegar_Valor(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < largura && y < altura)
        {
            return matriz_Grade[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject Pegar_Valor(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return Pegar_Valor(x, y);
    }
}