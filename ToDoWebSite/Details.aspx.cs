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
        if (!IsPostBack)
        {
            string itemIdValue = Request.QueryString["id"];
            ItemIdField.Value = itemIdValue;

            if (!String.IsNullOrEmpty(itemIdValue))
            {
                int editingItemId = Int32.Parse(itemIdValue);

                using (var ctx = new ToDoDBModel.ToDoDBEntities())
                {
                    var item = ctx.TodoItems.Where(x => x.Id == editingItemId).First();

                    if (item != null)
                    {
                        int len = item.Description.Length;

                        TextBox_Decription.Text = item.Description;
                        CheckBox_WasDone.Checked = (item.WasDone != 0);

                        if (item.DueDate.HasValue)
                        {
                            Calendar_DueDate.SelectedDate = (DateTime)item.DueDate;
                        }
                    }
                }
            }          
        }        
    }

    protected void Button_Save_Click(object sender, EventArgs e)
    {        
        DateTime? wasDoneDateTime = null;
        byte wasDone = 0;

        if (CheckBox_WasDone.Checked)
        {
            wasDoneDateTime = DateTime.Now;
            wasDone = 1;
        }

        using (var ctx = new ToDoDBModel.ToDoDBEntities())
        {
            string itemIdValue = ItemIdField.Value;

            ToDoDBModel.TodoItem item = null;

            if (!String.IsNullOrEmpty(itemIdValue))
            {
                int editingItemId = Int32.Parse(itemIdValue);

                item = ctx.TodoItems.Where(x => x.Id == editingItemId).First();

                if (item != null)
                {
                    item.Description = TextBox_Decription.Text;

                    if (item.WasDone != wasDone)
                    {
                        if (wasDone == 1)
                        {
                            item.WasDoneAt = wasDoneDateTime;
                        }
                        else
                        {
                            item.WasDoneAt = null;
                        }
                    }

                    item.WasDone = wasDone;
                    item.DueDate = GetDueDate();
                }
            }
            
            if (item == null)
            { 
                item = new ToDoDBModel.TodoItem
                {
                    Description = TextBox_Decription.Text,
                    AddedAt = DateTime.Now,
                    AddedBy = User.Identity.GetUserId(),
                    WasDone = wasDone,
                    WasDoneAt = wasDoneDateTime,
                    DueDate = GetDueDate()
                };

                ctx.TodoItems.Add(item);
            }                      

            ctx.SaveChanges();
        }

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