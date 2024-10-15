namespace pintopulante;

public partial class GamePage : ContentPage
{
    const int Gravidade = 2;
    const int tempoEntreFrames = 25;
    bool estaMorto = false;
    double LarguraJanela = 3;
    double AlturaJanela = 0;
    int Velocidade = 7;

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
                frameGameOver.IsVisible = true;
                break;
            }
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
        imgJunin.TranslationY = 0;
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
        }
    }

    bool VerificaColisao()
    {
        if(!estaMorto)
        {
            if(VerificaColisaoTeto()|| 
            VerificaColisaoChao())
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
}