using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JuanJoseVillalbaRuiz;

public partial class Notas : ContentPage
{
    private NotesDatabase _database;
    public Notas()
    {
        InitializeComponent();

       
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notas.db3");
        _database = new NotesDatabase(dbPath);

        
        LoadNotesAsync();
    }

    private async void LoadNotesAsync()
    {
        var notes = await _database.GetNotesAsync();
        NotesCollectionView.ItemsSource = notes;
    }

    private async void OnSaveNoteClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text) || string.IsNullOrWhiteSpace(ContentEditor.Text))
        {
            await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
            return;
        }

        var newNote = new Note
        {
            Title = TitleEntry.Text,
            Content = ContentEditor.Text,
            CreatedAt = DateTime.Now
        };

        await _database.SaveNoteAsync(newNote);

       
        TitleEntry.Text = string.Empty;
        ContentEditor.Text = string.Empty;

        
        LoadNotesAsync();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        LoadNotesAsync();
    }
}

public class Note
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}
