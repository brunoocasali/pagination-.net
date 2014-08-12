using System;
using System.Text.RegularExpressions;

public partial class UserControlPagination : System.Web.UI.UserControl
{
    public int Total { get; set; }
    public int TotalPerPage { get; set; }
    private Regex reg = new Regex(@"\b[&|?]p=[0-9]+\b");

    protected void Page_Load(object sender, EventArgs e)
    {
        //if in the page doesn't have the 'p' parameter on the query string add one
        if (!reg.IsMatch(Request.Url.PathAndQuery))
            Response.Redirect(Request.Url.PathAndQuery + "?p=1");
    }

    protected void MakePagination()
    {
        try
        {
            string lnkPrev = "", lnkNext = "";
            decimal pageCount = 1;

            int actual = (!string.IsNullOrEmpty(Request.QueryString["p"]) ? Convert.ToInt32(Request.QueryString["p"]) : 1);

            #region validate pages
            pageCount = Total != 0 ? Math.Ceiling(Convert.ToDecimal(Total) / Convert.ToDecimal(TotalPerPage)) : 1;

            if (actual == 1)
                lnkPrev = "javascript:;";
            else
                lnkPrev = mount(actual - 1);

            if (pageCount == actual)
                lnkNext = "javascript:;";
            else
                lnkNext = mount(actual + 1);
            #endregion

            #region Numbering pagination
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int total = Convert.ToInt32(pageCount);

            int ini = actual - 3,
                fim = 0;

            if (actual == 1)
                fim = actual + 4;

            else if (actual == 2)
                fim = actual + 3;

            else if (actual == 3)
                fim = actual + 2;

            else if (actual > 3)
                fim = actual + 3;

            if (ini < 1)
                ini = 1;

            if (fim > total)
                fim = total;

            if (actual >= total)
                fim = actual;

            while (ini < fim + 1)
            {
                if (ini == actual)
                    sb.AppendFormat(@"<a href=""javascript:;"">{0}</a>", ini);
                else
                    sb.AppendFormat(@"<a href=""{1}"" title=""Page {0}""> {0} </a>", ini, mount(ini));
                ini++;
            }
            #endregion

            #region Print buttons in the web page
            Response.Write(string.Format(@"<p style=""color:#000;"">Page {2} of {3}</p>
                                              <a href=""{0}"" title=""Previous page!"">&lt;&lt; Prev</a>
                                                {4} 
                                              <a href=""{1}"" title=""Next page!"">Next &gt;&gt;</a>",
                                            lnkPrev, lnkNext, actual, pageCount, sb.ToString()));
            #endregion
        }
        catch (Exception ex)
        {
            Response.Write("You have some erros, please verify and correct them!: ERROR MESSAGE:" + ex.Message);
        }
    }

    private string mount(int valor){
        //knows which type the 'p' are.. & or ?.
        string url = Request.Url.PathAndQuery;
        string tipo = url.Contains("?p=") ? "?" : url.Contains("&p=") ? "&" : "";

        return reg.Replace(url, tipo + "p=" + valor);
    }
}
