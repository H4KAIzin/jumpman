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

    protected override void OnAppearing()
    {
        base.OnApearring();
        Desenha();
    }
}