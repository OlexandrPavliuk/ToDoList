﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.AspNet.Identity;
using ToDoWebSite;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Button_CreateTodo.Enabled = (User.Identity.IsAuthenticated); 

        BindData();
    }

    protected void Button_CreateTodo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Details.aspx");        
    }
    protected void GridViewToDo_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[DescriptionColumnIndex].Text = "Description";
            e.Row.Cells[WasDoneColumnIndex].Text = "Was Done";
            e.Row.Cells[AddedAtColumnIndex].Text = "Added At";
            e.Row.Cells[AddedByColumnIndex].Text = "Added By";
        }
    }

    protected void GridViewToDo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewToDo.PageIndex = e.NewPageIndex;
        BindData();
    }

    private void BindData()
    {
        ToDoDBModel.ToDoDBEntities ctx = new ToDoDBModel.ToDoDBEntities();

        var dataSource = ctx.TodoItems.AsEnumerable().Select(
            item => new {
                             Description = item.Description,
                             WasDone = (item.WasDone == 1) ? "True" : "False",
                             AddedAt = item.AddedAt,
                             AddedBy = this.GetUserName(item.AddedBy)
                         });

        GridViewToDo.DataSource = dataSource.ToList();
        GridViewToDo.DataBind();        
    }

    private string GetUserName(string userId)
    {
        var manager = new UserManager();

        ApplicationUser currentUser = manager.FindById(userId);

        if (currentUser != null)
        {
            return currentUser.UserName;
        }

        return string.Empty;
    }

    private const int DescriptionColumnIndex = 0;
    private const int WasDoneColumnIndex = 1;
    private const int AddedAtColumnIndex = 2;
    private const int AddedByColumnIndex = 3;

   
}