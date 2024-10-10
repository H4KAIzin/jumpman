namespace pintopulante;

public partial class GamePage : ContentPage
{
    const int Gravidade = 1;
    const int tempoEntreFrames = 25;
    bool estaMorto = false;
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
}