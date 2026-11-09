using Práctica_2._Ahorcado.Modelo;
using System.Collections.ObjectModel;

namespace Práctica_2._Ahorcado;

public partial class PantallaAhorcado : ContentPage
{
	private Ahorcado? juego;
	public ObservableCollection<string> Letras { get; set; }

	public PantallaAhorcado()
	{
		InitializeComponent();
		Letras = new ObservableCollection<string>();

		// Inicializar todas las letras
		string alfabeto = "abcdefghijklmnopqrstuvwxyz";
		foreach (char c in alfabeto)
		{
			Letras.Add(c.ToString());
		}

		InicializarJuego();
	}

	private void InicializarJuego()
	{
		juego = new Ahorcado();
		BindingContext = juego;
		CrearBotones();
		ActualizarUI();
	}

	private void CrearBotones()
	{
		// Limpiar botones anteriores
		ContenedorLetras.Children.Clear();

		// Crear botones para cada letra
		foreach (var letra in Letras)
		{
			var boton = new Button
			{
				Text = letra,
				Margin = 1
                
            };
			boton.Clicked += OnLetraClicked;
			ContenedorLetras.Add(boton);
		}
	}

	private void OnLetraClicked(object? sender, EventArgs e)
	{
		if (juego?.JuegoTerminado ?? true) return;

		Button boton = sender as Button;
		if (boton == null) return;

		string? letra = boton.Text;
		if (string.IsNullOrEmpty(letra)) return;

		boton.IsEnabled = false;
		boton.Opacity = 0.5;

		juego?.IntentarLetra(char.Parse(letra));
		ActualizarUI();
	}

	private void OnResetClicked(object? sender, EventArgs e)
	{
		LabelEstado.Text = "";
		LabelEstado.TextColor = Colors.Red;
		InicializarJuego();
	}

	private void ActualizarUI()
	{
		if (juego == null) return;

		// Verificar estado del juego
		if (juego.JuegoTerminado)
		{
			if (juego.Ganado)
			{
				LabelEstado.Text = "You Won!";
				LabelEstado.TextColor = Colors.Green;
			}
			else
			{
				LabelEstado.Text = "You Lost!";
				LabelEstado.TextColor = Colors.Red;
			}
		}
	}
}