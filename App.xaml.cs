
namespace JuanJoseVillalbaRuiz
{
    public partial class App : Application
    {
        public static NotesDatabase Database { get; private set; }

        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.db3");
            Database = new NotesDatabase(dbPath);

            MainPage = new NavigationPage(new AppShell());
        }
    }
}
