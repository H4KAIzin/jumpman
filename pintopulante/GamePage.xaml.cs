namespace pintopulante;

public partial class GamePage : ContentPage
{
    const int Gravidade = 1;
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
}