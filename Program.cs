using CY.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// alt +f12 so powerful. lol
// ctrl-m toggles expand/collapse

namespace NoteTaker
{
  class Program
  {

    // Note storage
    // We will use only a local list for now - it will not persist between runs of course
    private static List<Note> notes;
    private static string s1;
    private static string s2;

    static void Main(string[] args)
    {

      BasicTests();  // ctrl-dot gives the popup 
                     //NoteAddTests();
                     //NoteViewTests();
                     //// Implement view one note and view all notes but from all notes, you can select one note to view
                     //// Criteria: Author, Date, Get location from map API so that spelling or autocorrect does not change the correct spelling location. Recognisable format of location.   
                     //NoteUpdateTests();
                     ////1.  Display all notes. 2.  Enter Note id (choose 1 note) 3. Format of NoteId integer or guid or string?  4. 
                     //NoteDeleteTests();

      MenuTests();

      //ReadLineWithDefault();
   

      //SomeNewFunction(123, "abc");  // press ctrl-dot and just press entertpress F12. yes, didn't know that one!

      Console.WriteLine("Press ENTER to end...");  
      Console.ReadLine();
      // try view call hierachy ? LOL
    }

    static string ReadLine(string Default)
    {
      // See: https://stackoverflow.com/questions/7565415/edit-text-in-c-sharp-console-application
      int pos = Console.CursorLeft;
      Console.Write(Default);
      ConsoleKeyInfo info;
      List<char> chars = new List<char>();
      if (string.IsNullOrEmpty(Default) == false)
      {
        chars.AddRange(Default.ToCharArray());
      }

      while (true)
      {
        info = Console.ReadKey(true);
        if (info.Key == ConsoleKey.Backspace && Console.CursorLeft > pos)
        {
          chars.RemoveAt(chars.Count - 1);
          Console.CursorLeft -= 1;
          Console.Write(' ');
          Console.CursorLeft -= 1;

        }
        else if (info.Key == ConsoleKey.Enter) { Console.Write(Environment.NewLine); break; }
        //Here you need create own checking of symbols
        else if (char.IsLetterOrDigit(info.KeyChar) || char.IsPunctuation(info.KeyChar) || char.IsSeparator(info.KeyChar))
        {
          Console.Write(info.KeyChar);
          chars.Add(info.KeyChar);
        }
      }
      return new string(chars.ToArray());
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
      // notice that it has even added the parameters with made-up names!
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
      int id;
      Console.WriteLine("Enter note id to start");
      string numStr = Console.ReadLine();

      if (!Int32.TryParse(numStr, out id))
      {
        Console.WriteLine("Entry id is not valid. Enter a integer for the id");
        return;
      }
      Note n = notes.Where(x => x.ID == id).FirstOrDefault();

      // if found, do delete
      if (n != null)
      {
        notes.Remove(n);
      }
      else
        Console.WriteLine("Note not found");

      //prompt confirmation of delete note. 
    
    }

    private static void NoteUpdate()
    {
      // get id from user
      int id;
      Console.WriteLine("Enter note id to start");
      string numStr = Console.ReadLine();

      if (!Int32.TryParse(numStr, out id))
      {
        Console.WriteLine("Entry id is not valid. Enter a integer for the id");
        return;
      }

      // find notes
      Note n = notes.Where(x => x.ID == id).FirstOrDefault();

      // if found, do edit
      if (n != null)
      {
        SendKeys.SendWait(n.Text);
        string s = Console.ReadLine();
        n.Text = s;
      }
      else
        Console.WriteLine("Note not found");

      // save
    }

    private static void ReadLineWithDefault()
    {
      //string s1 = ReadLine("This is some existing text");
      //Console.WriteLine("Resultant string: " + s1);
      Console.WriteLine("Edit the following:");
      SendKeys.SendWait("This is other existing text");
      string s2 = Console.ReadLine();
      Console.WriteLine("Resultant string: " + s2);
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
      // Let's keep all these away from view here and we can just comment in/out the function call
      // ctrl-dot again
      // all shoudl now work
      Note n1 = new Note(1, "Hello") { Text = "Hello again - overwrites value passed in constructor" };  // one way to initialise properties
      Note n2 = new Note(2, "world");
      //Note Authorname = new Note( $ { Authorname: })
      //what i am trying to do is add a author name 
      // ok - easier than you think

      n2.Authorname = "Caleb"; // done!
      // how about asking user to input the name 
      //  Console.ReadLine();
      //n2.Text = "world";                        // another
      Console.WriteLine($"n2 length is {n2.Length}");

      Note n3 = new Note(11, "aaa");
      Note n4 = new Note(22, "bbb", "James"); // notice how we can do either/both - due to that "default parameter"

      // We could decide that a note can not exist without text and insist that text is provided
      // e.g. by requiring text in the constructor

      //Console.WriteLine(n1);
      //Console.WriteLine(n2);

      List<Note> lst = new List<Note>();
      lst.Add(n1);
      //lst.Add(n2);
      //lst.Add(new Note("blah"));

      Console.WriteLine($"There {(lst.Count == 1 ? "is" : "are")} {lst.Count} note({(lst.Count == 1 ? "" : "s")})"); // what does this do?
      foreach (var item in lst)
      {
        Console.WriteLine(item);
      }
    }
  }
}
