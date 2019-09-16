using CY.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NoteTaker
{
  class Program
  {

    
    private static List<Note> notes;

    static void Main(string[] args)
    {

      BasicTests();  
      
      MenuTests();


      Console.WriteLine("Press ENTER to end...");  // 
      Console.ReadLine();

    }

    private static void NoteDeleteTests()
    {
      throw new NotImplementedException();
    }

    private static void NoteUpdateTests()
    {
      throw new NotImplementedException();
    }

    private static void NoteViewTests()
    {
      throw new NotImplementedException();
    }

    private static void NoteAddTests()
    {
      throw new NotImplementedException();
    }

    private static void SomeNewFunction(int v1, string v2)
    {
     
    }

    private static void MenuTests()
    {
      try
      {
        notes = new List<Note>();

        string s;
        do
        {
          ShowMenu();
          s = Console.ReadLine();
          if (s.Length == 0) continue;
          switch (s.Substring(0, 1))
          {
            case "a": NoteAdd(); break;
            case "v": NoteView("Notes:", notes); break;
            case "u": NoteUpdate(); break;
            case "d": NoteDelete(); break;
              // 1. Ask user for note id. 2. Check that the noteid exists. 3. Type 0 to cancel. 4. For record found to be deleted, do you want to delete  the note: y/n (using ReadKey()) ? 5. If user want to delete key, use list method - Notes.Remove(); 6. if user press no, they will go back to the main menu.  
            case "s": NoteSearch(); break;
              // Note Search Implementation: Search for text in the note. Search the list for the particular  
            case "x":
              Console.WriteLine("Exit Menu");
              break;
            default:
              Console.WriteLine("Option not valid");
              break;
          }
        } while (!s.ToLower().StartsWith("x"));
      }
      catch (Exception ex)
      {
        Console.WriteLine("ERROR: " + ex.Message);
      }
    }

    private static void NoteSearch()
    {
      Console.WriteLine("Enter text in note to search for:");
      string s = Console.ReadLine().Trim();
      List<Note> results = notes.Where(x => x.Text.ToLower().Contains(s.ToLower())).ToList();
      if (results.Count() == 0)
      {
        Console.WriteLine("Nothing found!");
      }
      else
      {
        Console.WriteLine("Results is:");
        NoteView("Results", results);
      }
    }

    private static void NoteDelete()
    {
      throw new NotImplementedException();
    }

    private static void NoteUpdate()
    {
      throw new NotImplementedException();
    }

    private static void NoteView(string header, List<Note> lst)
    {
      Console.WriteLine(header);
      Console.WriteLine("------------------------------------------------");
      foreach (Note note in lst)
        Console.WriteLine(note);
      Console.WriteLine("------------------------------------------------");
    }

    private static void NoteAdd()
    {
      Console.WriteLine("Enter note text, or leave empty to return");
      Note n = GetNote();
      if (n != null)
        notes.Add(n);
    }

    private static Note GetNote()
    {
     // Always think of later when we might need a general note-getting function
      string s = Console.ReadLine().Trim();
      // Trim removes whitespace from beginning and end of a string.. Trim is placed before  is nullorempty to check for nullorempty.
      if (!string.IsNullOrEmpty(s))
      {
        Note x = new Note(notes.Count() + 1, s);
        return x;
      }
      else
        return null;
    }

    private static void ShowMenu()
    {
      Console.WriteLine("Main Menu");
      Console.WriteLine("a: add note");
      Console.WriteLine("u: update note");
      Console.WriteLine("d: delete note");
      Console.WriteLine("v: view notes");
      Console.WriteLine("s: search notes");
      Console.WriteLine("x. Exit");
    }

    private static void BasicTests()
    {
     
      Note n1 = new Note(1, "Hello") { Text = "Hello again - overwrites value passed in constructor" };  // one way to initialise properties
      Note n2 = new Note(2, "world");
      

      n2.Authorname = "Caleb"; // done!
     
      Console.WriteLine($"n2 length is {n2.Length}");

      Note n3 = new Note(11, "aaa");
      Note n4 = new Note(22, "bbb", "James");



      List<Note> lst = new List<Note>();
      lst.Add(n1);
     

      Console.WriteLine($"There {(lst.Count == 1 ? "is" : "are")} {lst.Count} note({(lst.Count == 1 ? "" : "s")})"); // what does this do?
      foreach (var item in lst)
      {
        Console.WriteLine(item);
      }
    }
  }
}

