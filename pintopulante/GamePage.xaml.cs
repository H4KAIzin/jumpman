using System.Runtime.CompilerServices;
﻿using Plugin.Maui.Audio;


namespace pintopulante;

public partial class GamePage : ContentPage
{
    int score = 0;
    const int Gravidade = 6;
    const int tempoEntreFrames = 30;
    bool estaMorto = false;
    double LarguraJanela = 3;
    double AlturaJanela = 0;
    int Velocidade = 14;
    const int forcaPulo = 70;
    const int maxTempoPulando = 0; //frames
    bool estaPulando = false;
    int tempoPulando = 0;
    const int aberturaMinima = 200;

 	public GamePage()
	{
		InitializeComponent();
	}

	void AplicaGravidade()
    {
        imgJunin.TranslationY += Gravidade;
    }


    async Task Desenha()
    {
        while (!estaMorto)
        {
            AplicaGravidade();
            await Task.Delay(tempoEntreFrames);
            GerenciaCanos();
            if(VerificaColisao())
            {
                estaMorto = true;
                SoundHelper.Play("morto.wav");
                frameGameOver.IsVisible = true;
                break;
            }
            if(estaPulando)
               AplicaPulo();
            else
                AplicaGravidade();
            await Task.Delay(tempoEntreFrames); 
        }
    }

    void OnGameOverClicked(object sender, EventArgs e)
    {
        frameGameOver.IsVisible = false;
        Inicializar();
        Desenha();
    }

    void Inicializar()
    {
        imgcanocima.TranslationX =- LarguraJanela;
        imgcanobaxo.TranslationY =- LarguraJanela;
        imgJunin.TranslationY = 0;
        imgJunin.TranslationX = 0;
        GerenciaCanos();
    }

    protected override void OnSizeAllocated(double w, double h)
    {
        base.OnSizeAllocated(w, h);
        LarguraJanela = w;
        AlturaJanela = h;
    }

    void GerenciaCanos()
    {
        imgcanocima.TranslationX -= Velocidade;
        imgcanobaxo.TranslationX -= Velocidade;
        if(imgcanobaxo.TranslationX < - LarguraJanela)
        {
            imgcanobaxo.TranslationX = 0;
            imgcanocima.TranslationX = 0;

            var alturaMax =- 100;
            var alturaMin =- imgcanobaxo.HeightRequest;
            imgcanocima.TranslationY = Random.Shared.Next((int)alturaMin, (int)alturaMax);
            imgcanobaxo.TranslationY = imgcanocima.TranslationY + aberturaMinima + imgcanobaxo.HeightRequest;
        }
    }   

    bool VerificaColisao()
    {
        if(!estaMorto)
        {
            if(VerificaColisaoTeto()|| 
            VerificaColisaoChao()||
            VerificaColisaoCanoCima()||
            VerificaColisaoCanoBaxo())
               {
                return true;
               }
        }
                return false;
    }

    bool VerificaColisaoTeto()
    {
        var minY =- AlturaJanela/2;
        if(imgJunin.TranslationY <= minY)
            return true;
        else
            return false;
    }

    bool VerificaColisaoChao()
    {
        var maxY = AlturaJanela/2 - imglauva.HeightRequest -imgJunin.HeightRequest;
        if(imgJunin.TranslationY >= maxY)
            return true;
        else
            return false;

    }

    void AplicaPulo()
    {
        imgJunin.TranslationY -= forcaPulo;
        tempoPulando++;
        if (tempoPulando >= maxTempoPulando)
        {
            estaPulando = false;
            tempoPulando = 0;
        }
    }

    void OnGridClicked(object sender, TappedEventArgs a)
    {
        estaPulando = true;
    }

    bool VerificaColisaoCanoCima()
    {
        var posHJunin = (LarguraJanela/2)-(imgJunin.WidthRequest/2);
        var posVJunin = (AlturaJanela/2)-(imgJunin.HeightRequest/2) + imgJunin.TranslationY;
        if (posHJunin >= Math.Abs(imgcanocima.TranslationX - imgcanocima.WidthRequest)&&
            posHJunin <= Math.Abs(imgcanocima.TranslationX + imgcanocima.WidthRequest)&&
            posVJunin <= imgcanocima.HeightRequest + imgJunin.TranslationY)
            {
                return true;
            } 
            else
            {
                return false;
            }
    }

    bool VerificaColisaoCanoBaxo()
    {
        var posVJunin = (LarguraJanela/2)-(imgJunin.WidthRequest/2);
        var posHJunin = (AlturaJanela/2)-(imgJunin.HeightRequest/2) + imgJunin.TranslationY;
        if (posVJunin >= Math.Abs(imgcanobaxo.TranslationX - imgcanobaxo.WidthRequest)&&
            posVJunin <= Math.Abs(imgcanobaxo.TranslationX + imgcanobaxo.WidthRequest)&&
            posHJunin <= imgcanobaxo.HeightRequest + imgJunin.TranslationY)
            {
                return true;
            } 
            else
            {
                return false;
            }
    }
}