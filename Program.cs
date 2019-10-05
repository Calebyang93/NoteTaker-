using CY.Data.Interface;
using CY.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CY.Data.Ado
{
  public class NoteRepository : INoteRepository
  {
    private string connStr = @"
       Integrated Security = SSPI; 
       Persist Security Info = false; 
       Data Source = localhost\SQLExpress; 
       Initial Catalog = NoteDB; 
       Connection Timeout = 1000";

    public object Date { get; private set; }
    public object Global { get; private set; }
    public object MessageBox { get; private set; }

    public List<Note> GetAll()
    {
      DataTable dt = new DataTable();
      using (SqlConnection conn = new SqlConnection(connStr))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand("select  * from Notes order by Date desc", conn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;
        adapter.Fill(dt);
      }
      // Read the data into a list
      List<Note> lst = new List<Note>();

      foreach (DataRow dr in dt.Rows)
      {
        Note n = new Note();
        n.ID = Convert.ToInt32(dr[0]);
        n.Date = DateTime.Now; //Convert.ToDateTime(dr[1]);
        n.Text = Convert.ToString(dr[2]);
        lst.Add(n);
      }
      return lst;
    }

    public List<Note> SearchByText(string s)
    {
      string sql = $"SELECT  * FROM Notes where note like '%{s}%'";

      return null;
    }

    public Note GetByID(int id)
    {
      DataTable dt = new DataTable();
      string sql = $@"select * from Notes where id = {id} ";
      using (SqlConnection conn = new SqlConnection(connStr))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;
        adapter.Fill(dt);
      }

      if (dt.Rows.Count == 1)
      {
        Note n = new Note();
        n.ID = Convert.ToInt32(dt.Rows[0][0]);
        n.Date = DateTime.Now; //Convert.ToDateTime(dt.Rows[0][1]);
        n.Text = Convert.ToString(dt.Rows[0][2]);
        return n;
      }
      else if (dt.Rows.Count > 1)
      {
        // shouldn't happen  -must only be one rec with given id
        return null;
      }
      else
        return null;
    }

    public Note GetLatest()
    {
      DataTable dt = new DataTable();
      string sql = $@"SELECT 
                        TOP 1 * 
                      FROM 
                        Notes 
                      ORDER BY 
                        date desc";
      using (SqlConnection conn = new SqlConnection(connStr))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = cmd;
        adapter.Fill(dt);
      }

      // 1. Check through list of notes.
      // 2. Sort data table list by record with date
      // 3. Selects Date of Note with latest Date.       //

      if (dt.Rows.Count == 1)
      {
        Note n = new Note();
        n.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
        n.Date = DateTime.Now; //Convert.ToDateTime(dt.Rows[0][1]);
        n.Text = Convert.ToString(dt.Rows[0][2]);
        return n;
      }
      else if (dt.Rows.Count > 1)
      {
        // shouldn't happen  -must only be one rec with given id
        return null;
      }
      else
        return null;
    }

    //public static void deletebyid(string table, string id, string columnname)
    //{
    //  try
    //  {
    //    using (sqlconnection conn = new sqlconnection(global.connectionstring))
    //    {
    //      conn.open();
    //      using (sqlcommand command = new sqlcommand("delete from" + table + "where" + columnname + " = " + id + "'", conn))
    //      {
    //        command.executenonquery();
    //      }
    //      conn.close();
    //    }
    //  }
    //  catch (systemexception ex)
    //  {
    //    messagebox.show(string.format(" an error occured: {0}", ex.message));
    //  }
    //}

    public Note DeleteByID(string ID)
    {
      // "DROP" is not the right verb nere. DROP is used for tables and database, not individual records.
      // Instead, we use DELETE e.g. "DELETE from Notes WHERE id = 123". Also "DELETE... ORDER BY" makes no so we can remove that part
      // The other important difference is in the code that follwos. When deleting, we are (obv) not getting data, so no need for
      // that datatable stuff - we just want to execute a command, and are not in the business of "getting" anything.

      DataTable dt = new DataTable();
      string sql = $@"delete from Notes where id = {ID}";
      using (SqlConnection conn = new SqlConnection(connStr))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.CommandType = CommandType.Text;
        cmd.ExecuteNonQuery(); // <<<< NB - execute a command that is NOT a query ("delete" is not a query per se)
                               // we could guage whether this has succeeded or failed, bu looking at the return value.
                               // what does the return value (of ExecuteNonQuery) tell us?
                               // type dot after cmd.. the tooltip/hint tells us that teh values return is not true/false a you might expect, but the number of rows affected
                               // which will be 1 in this case. So, anything other than 1 is a problem, and 0 is possible a problem - but maybe not...
                               // some one could say delete rec with ID 1000 and so what if there is no record 1000? And so, perhaps we don't bother with the
                               // return value!ok
                               // run the program?
                               // ok
                               // it works. Record deleted ok? 
                               //Remember, when using SQL commands like delete... you can always try them out with SSMS
                               // An SSD would speed up your machine!
        //  that will happen when i can get one. Remember that I have one right here!
        cmd.ExecuteNonQuery();
      }

      if (dt.Rows.Count == 1)
      {
        Note n = new Note();
        n.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
        n.Date = DateTime.Now; //Convert.ToDateTime(dt.Rows[0][1]);
        n.Text = Convert.ToString(dt.Rows[0][2]);
        return n;
      }
      else if (dt.Rows.Count > 1)
      {
        // shouldn't happen  -must only be one rec with given id
        return null;
      }
      else
        return null;
    }

    public void DeleteByID(int id)
    {
      throw new NotImplementedException();
    }

    // 1. Search through list of columns for data id.
    // 2. Based on the individual data Id, we select the id in sql management studio.
    // 3. In respect to the id column, select the id type. 
    // 4. Select the id of the data 
    // 5. Delete the string and row of the id 


    public void DeleteAllNotes(int Id)
    {
      
      DataTable dt = new DataTable();
      string sql = $@"DELETE all Notes where id = {Id}";
      using (SqlConnection conn = new SqlConnection(connStr))
      {
        conn.Open();
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.CommandType = CommandType.Text;
        cmd.ExecuteNonQuery();

      }
    }


    public void AddTestNotes(int Id)
 
      {
        DataTable dt = new DataTable();
        string sql = $@"Insert INTO Notes where id = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]";
        using (SqlConnection conn = new SqlConnection(connStr))
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand(sql, conn);
          cmd.CommandType = CommandType.Text;
          cmd.ExecuteNonQuery();
        }

      }
    

    //private void SaveNote(Note note, object n)
    //{
    //}

    private void DeleteById(int v, object iD)
    {
    }

    public void SaveNote(Note n)
    {
    }

  }

  internal class Notes : Note
  {
  }
}

