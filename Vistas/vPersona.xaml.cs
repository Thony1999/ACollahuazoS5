using ACollahuazoS5.Models;

namespace ACollahuazoS5.Vistas;

public partial class vPersona : ContentPage
{
	public vPersona()
	{
		InitializeComponent();
	}

    private async void BtnAgregar_Clicked(object sender, EventArgs e)
    {
        lblMensaje.Text = "";
        App.PersonRepo.addNewPerson(txtPersona.Text, txtApellido.Text, txtEdad.Text);
        lblMensaje.Text = App.PersonRepo.mensaje;

        await DisplayAlert("Mensaje", App.PersonRepo.mensaje, "OK");


        listaPersona.ItemsSource = App.PersonRepo.GetAllPeople();

        txtPersona.Text = "";
        txtApellido.Text = "";
        txtEdad.Text = "";
    }

    private async void btnListar_Clicked(object sender, EventArgs e)
    {
		lblMensaje.Text = "";
		List<Persona> person = App.PersonRepo.GetAllPeople();
		listaPersona.ItemsSource = person;
    }

    //actualizar
    private async void Button_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var persona = button?.CommandParameter as Persona;

        if (persona != null)
        {
            string nuevoNombre = await DisplayPromptAsync(
                "Editar Persona",
                $"Ingrese nuevo nombre para {persona.Nombre}",
                initialValue: persona.Nombre);


            if (!string.IsNullOrEmpty(nuevoNombre))
            {
                // Método sincrónico
                App.PersonRepo.UpdatePerson(persona.Id, nuevoNombre);
                lblMensaje.Text = App.PersonRepo.mensaje;

                await DisplayAlert("Mensaje", App.PersonRepo.mensaje, "OK");

                listaPersona.ItemsSource = App.PersonRepo.GetAllPeople();
            }
        }
    }

    //eliminar
    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var button = sender as Button;
        var persona = button?.CommandParameter as Persona;

        if (persona != null)
        {
            bool confirmar = await DisplayAlert("Confirmar eliminación",
                $"¿Deseas eliminar a {persona.Nombre}?",
                "Sí", "No");

            if (confirmar)
            {
                App.PersonRepo.DeletePerson(persona.Id);
                lblMensaje.Text = App.PersonRepo.mensaje;
                listaPersona.ItemsSource = App.PersonRepo.GetAllPeople();
            }
        }
    }
}
