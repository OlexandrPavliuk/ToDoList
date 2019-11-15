using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

public partial class Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button_Save_Click(object sender, EventArgs e)
    {
        ToDoDBModel.ToDoDBEntities entitiesContext = new ToDoDBModel.ToDoDBEntities();        

        DateTime? wasDoneDateTime = null;
        byte wasDone = 0;

        if (CheckBox_WasDone.Checked)
        {
            wasDoneDateTime = DateTime.Now;
            wasDone = 1;
        }
            
        var newItem = new ToDoDBModel.TodoItem
        {
            Description = TextBox_Decription.Text,
            AddedAt = DateTime.Now,
            AddedBy = User.Identity.GetUserId(),
            WasDone = wasDone,
            WasDoneAt = wasDoneDateTime,
            DueDate = GetDueDate()
        };

        entitiesContext.TodoItems.Add(newItem);
        entitiesContext.SaveChanges();

        Response.Redirect("Default.aspx");
    }

    private DateTime? GetDueDate()
    {
        DateTime dueDate = Calendar_DueDate.SelectedDate;

        // By default calendar control's SelectedDate contains the 01.01.01 value        
        if (dueDate.Year == 1 && dueDate.Month == 1 && dueDate.Day == 1)
        {
            return null;
        }

        return dueDate;
    }
}