using CY.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// alt +f12 so powerful. lol
// ctrl-m toggles expand/collapse

namespace NoteTaker
{
  class Program
  {

    // Note storage
    // We will use only a local list for now - it will not persist between runs of course
    private static List<Note> notes;

    static void Main(string[] args)
    {

      BasicTests();  // ctrl-dot gives the popup 

      MenuTests();

      //SomeNewFunction(123, "abc");  // press ctrl-dot and just press entertpress F12. yes, didn't know that one!

      Console.WriteLine("Press ENTER to end...");  // 
      Console.ReadLine();
      // try view call hierachy ? LOL
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
            case "u": NoteView(); break;
            case "d": NoteUpdate(); break;
            case "v": NoteDelete(); break;
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

    private static void NoteDelete()
    {
      throw new NotImplementedException();
    }

    private static void NoteUpdate()
    {
      throw new NotImplementedException();
    }

    private static void NoteView()
    {
      throw new NotImplementedException();
    }

    private static void NoteAdd()
    {
      Note n = GetNote();
      if (n != null)
        notes.Add(n);
    }

    private static Note GetNote()
    {
      Console.WriteLine("Enter note text");
      string s = Console.ReadLine();
      if (!string.IsNullOrEmpty(s))
      {
        Note x = new Note(s);
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
      Console.WriteLine("x. Exit");
    }

    private static void BasicTests()
    {
      // Let's keep all these away from view here and we can just comment in/out the function call
      // ctrl-dot again
      // all shoudl now work
      Note n1 = new Note("Hello") { Text = "Hello again - overwrites value passed in constructor" };  // one way to initialise properties
      Note n2 = new Note("world");
      //Note Authorname = new Note( $ { Authorname: })
      //what i am trying to do is add a author name 
      // ok - easier than you think

      n2.Authorname = "Caleb"; // done!
      // how about asking user to input the name 
      //  Console.ReadLine();
      //n2.Text = "world";                        // another
      Console.WriteLine($"n2 length is {n2.Length}");

      Note n3 = new Note("aaa");
      Note n4 = new Note("bbb", "James"); // notice how we can do either/both - due to that "default parameter"

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
