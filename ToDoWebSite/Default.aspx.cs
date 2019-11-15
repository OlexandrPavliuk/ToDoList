using System;
using System.Collections.Generic;
using System.Data;
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
    protected void GridViewToDo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewToDo.PageIndex = e.NewPageIndex;
        BindData();
    }
    
    private void BindData()
    {
        GridViewToDo.Columns.Clear();

        string sortExpression = (ViewState["sort_expression"] != null) ? ViewState["sort_expression"].ToString() : null;        

        using (ToDoDBModel.ToDoDBEntities ctx = new ToDoDBModel.ToDoDBEntities())
        {
            if (!String.IsNullOrEmpty(sortExpression))
            {                
                if (GetSortDirection(sortExpression) == SortDirection.Ascending)
                {
                    var dataSource = ctx.TodoItems.AsEnumerable().OrderBy(x => this.GetSortingValue(x, sortExpression)).Select(
                       item => new
                       {
                           Id = item.Id,
                           Description = item.Description,
                           WasDone = (item.WasDone == 1) ? "True" : "False",
                           AddedAt = item.AddedAt,
                           AddedBy = this.GetUserName(item.AddedBy)
                       });

                    GridViewToDo.DataSource = dataSource.ToList();
                }
                else
                {
                    var dataSource = ctx.TodoItems.AsEnumerable().OrderByDescending(x => this.GetSortingValue(x, sortExpression)).Select(
                       item => new
                       {
                           Id = item.Id,
                           Description = item.Description,
                           WasDone = (item.WasDone == 1) ? "True" : "False",
                           AddedAt = item.AddedAt,
                           AddedBy = this.GetUserName(item.AddedBy)
                       });

                    GridViewToDo.DataSource = dataSource.ToList();
                }
            }
            else
            {
                var dataSource = ctx.TodoItems.AsEnumerable().Select(
                       item => new
                       {
                           Id = item.Id,
                           Description = item.Description,
                           WasDone = (item.WasDone == 1) ? "True" : "False",
                           AddedAt = item.AddedAt,
                           AddedBy = this.GetUserName(item.AddedBy)
                       });

                GridViewToDo.DataSource = dataSource.ToList();
            }
        }

        BoundField idField = new BoundField();
        idField.DataField = "Id";
        idField.Visible = false;
        GridViewToDo.Columns.Add(idField);
            
        BoundField descriptionField = new BoundField();
        descriptionField.DataField = "Description";
        descriptionField.HeaderText = "Description";
        descriptionField.SortExpression = "Description";
        GridViewToDo.Columns.Add(descriptionField);            

        BoundField wasDoneField = new BoundField();
        wasDoneField.DataField = "WasDone";
        wasDoneField.HeaderText = "Was Done";
        wasDoneField.SortExpression = "WasDone";
        GridViewToDo.Columns.Add(wasDoneField);

        BoundField addedAtField = new BoundField();
        addedAtField.DataField = "AddedAt";
        addedAtField.HeaderText = "Added At";
        addedAtField.SortExpression = "AddedAt";
        GridViewToDo.Columns.Add(addedAtField);

        BoundField addedByField = new BoundField();
        addedByField.DataField = "AddedBy";
        addedByField.HeaderText = "Added By";
        addedByField.SortExpression = "AddedBy";
        GridViewToDo.Columns.Add(addedByField);                

        GridViewToDo.DataBind();        
    }

    protected void GridViewToDo_RowDataBound(object sender, GridViewRowEventArgs e)
    {               
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridViewToDo, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";            
        }        
    }

    protected void GridViewToDo_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = GridViewToDo.SelectedRow.RowIndex;
        int itemId = (int)GridViewToDo.DataKeys[index].Value;

        Response.Redirect("Details.aspx?id=" + itemId.ToString());
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

    private object GetSortingValue(ToDoDBModel.TodoItem item, string sortExpression)
    {
        switch (sortExpression)
        {
            case "Description":
                return item.Description;
            case "WasDone":
                return item.WasDone;
            case "AddedAt":
                return item.AddedAt;
            case "AddedBy":
                return this.GetUserName(item.AddedBy);
        }

        return string.Empty;
    }

    protected void GridViewToDo_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["sort_expression"] = e.SortExpression;
        ChangeSortDirection(e.SortExpression);

        BindData();
    }

    private SortDirection GetSortDirection(string column)
    {
        SortDirection nextDir = SortDirection.Ascending; 

        if (ViewState["sort"] != null && ViewState["sort"].ToString() == column)
        {   
            nextDir = SortDirection.Descending;            
        }
        
        return nextDir;
    }

    private void ChangeSortDirection(string column)
    {
        if (ViewState["sort"] != null && ViewState["sort"].ToString() == column)
        {            
            ViewState["sort"] = null;
        }
        else
        {
            ViewState["sort"] = column;
        }
    }

    private const int ItemIdColumnIndex = 0;
    private const int DescriptionColumnIndex = 1;
    private const int WasDoneColumnIndex = 2;
    private const int AddedAtColumnIndex = 3;
    private const int AddedByColumnIndex = 4;    
    
}