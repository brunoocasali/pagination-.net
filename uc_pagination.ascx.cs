using System;
using System.Text.RegularExpressions;

public partial class UserControlPagination : System.Web.UI.UserControl
{
    public int Total { get; set; }
    public int TotalPerPage { get; set; }
    private Regex reg = new Regex(@"\b[&|?]p=[0-9]+\b");

    protected void Page_Load(object sender, EventArgs e)
    {
        //se não existe a chave 'p' na querystring adiciona ela.
        if (!reg.IsMatch(Request.Url.PathAndQuery))
        {
            Response.Redirect(Request.Url.PathAndQuery + "?p=1");
        }
    }

    protected void MakePagination()
    {
        try
        {
            #region Declara variáveis
            string lnkAnterior = "", lnkProximo = "";
            decimal pageCount = 1;

            int pagAtual = (!string.IsNullOrEmpty(Request.QueryString["p"]) ? Convert.ToInt32(Request.QueryString["p"]) : 1);
            #endregion

            #region Valida Páginas
            pageCount = Total != 0 ? Math.Ceiling(Convert.ToDecimal(Total) / Convert.ToDecimal(TotalPerPage)) : 1;

            if (pagAtual == 1)
                lnkAnterior = "javascript:;";
            else
                lnkAnterior = mount(pagAtual - 1);

            if (pageCount == pagAtual)
                lnkProximo = "javascript:;";
            else
                lnkProximo = mount(pagAtual + 1);
            #endregion

            #region Paginação númerica
            System.Text.StringBuilder sbPaginacao = new System.Text.StringBuilder();
            int totalPagina = Convert.ToInt32(pageCount);

            int ini = pagAtual - 3,
                fim = 0;

            if (pagAtual == 1)
                fim = pagAtual + 4;

            else if (pagAtual == 2)
                fim = pagAtual + 3;

            else if (pagAtual == 3)
                fim = pagAtual + 2;

            else if (pagAtual > 3)
                fim = pagAtual + 3;

            if (ini < 1)
                ini = 1;

            if (fim > totalPagina)
                fim = totalPagina;

            if (pagAtual >= totalPagina)
                fim = pagAtual;

            while (ini < fim + 1)
            {
                if (ini == pagAtual)
                    sbPaginacao.AppendFormat(@"<a href=""javascript:;"" class=""paginaAtual"" title=""Página atual"">{0}</a>", ini);
                else
                    sbPaginacao.AppendFormat(@"<a href=""{1}"" title=""Página {0}""> {0} </a>", ini, mount(ini));
                ini++;
            }
            #endregion

            #region Imprime na tela botões para paginação
            Response.Write(string.Format(@"<p style=""color:#000;"">Página {2} de {3}</p>
                                              <a href=""{0}"" title=""Página anterior"">&lt;&lt; Anterior</a>
                                                {4} 
                                              <a href=""{1}"" title=""Proxima página"">Proximo &gt;&gt;</a>",
                                            lnkAnterior, lnkProximo, pagAtual, pageCount, sbPaginacao.ToString()));
            #endregion
        }
        catch (Exception ex)
        {
            Response.Write("Não foi possível montar a paginação ERRO: " + ex.Message);
        }
    }

    private string mount(int valor){
        //knows which type the 'p' are.. & or ?.
        string url = Request.Url.PathAndQuery;
        string tipo = url.Contains("?p=") ? "?" : url.Contains("&p=") ? "&" : "";

        return reg.Replace(url, tipo + "p=" + valor);
    }
}
