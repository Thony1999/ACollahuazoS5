using ACollahuazoS5.Clases;

namespace ACollahuazoS5
{
    public partial class App : Application
    {
        public static Clases.PersonRepository PersonRepo { get; set; }
        public App(PersonRepository personRepository)
        {
            InitializeComponent();
            PersonRepo = personRepository;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new Vistas.vPersona());
        }
    }
}