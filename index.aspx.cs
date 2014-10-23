using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class index : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["CalendarConnect"].ConnectionString);
    private List<DateTime> events = new List<DateTime>();
    private List<string> eventname = new List<string>();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GetEvents();
        }
    }

    private void GetEvents()
    {
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from CalendarEventTables", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                events.Add((DateTime)ds.Tables[0].Rows[i]["DateTime"]);
                eventname.Add((string)ds.Tables[0].Rows[i]["EventTitle"]);
            }
        }
        catch
        {

        }
        finally { con.Close(); }
    }

    protected void OpenCalendar_Load(object sender, DayRenderEventArgs e)
    {
        e.Cell.Text = string.Empty;
        Button btn = new Button();
        btn.ID = "Date";
        btn.Text = e.Day.Date.ToString("MM-dd-yyyy");
        string day = e.Day.Date.ToString("mmddyyyy");
        btn.Attributes.Add("onclick", "window.open('EventDay.aspx');");

        e.Cell.Controls.Add(btn);
        string tooltip = string.Empty;
        if (IsEventDay(e.Day.Date, out tooltip))
        {
            e.Cell.BackColor = System.Drawing.Color.Pink;
            e.Day.IsSelectable = false;
            e.Cell.ToolTip = tooltip;

            
            

        }
        if (e.Day.IsWeekend)
        {
            e.Cell.BackColor = System.Drawing.Color.Black;
            e.Cell.ForeColor = System.Drawing.Color.White;
            e.Day.IsSelectable = false;
        }

        
        

        
    }

    private bool IsEventDay(DateTime day, out string tooltipvalue)
    {
        tooltipvalue = string.Empty;
        for (int i = 0; i < events.Count; i++)
        {
            if (events[i] == day)
            {
                tooltipvalue = eventname[i];
                return true;
            }
        }
        return false;
    } 

    protected void OpenCalendar_SelectionChanged(object sender, MonthChangedEventArgs e)
    {
        GetEvents();
    }

    protected void Redirect(object sender, EventArgs e)
    {

    }
}