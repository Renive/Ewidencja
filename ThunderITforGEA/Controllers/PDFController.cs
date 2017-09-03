using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ThunderITforGEA.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.IO;
using RazorPDF;
using iTextSharp;
using System.Web.Hosting;


namespace ThunderITforGEA.Controllers
{
    public class PDFController : Controller
    {
        // GET: PDF
        public ActionResult Index()
        {
            return View();
        }

        public FileStreamResult zrobPDFUzytkownik()
        {
            Entities daneDoModelu = new Entities();
            var id = User.Identity.GetUserId();
            var dane = daneDoModelu.SprzetUzytkownika.Include(u => u.AspNetUsers).Where(u => u.AspNetUsers.Id == id);
            var lista = dane.ToList();
            string imieNazwisko = dane.First().AspNetUsers.imie + " " + dane.First().AspNetUsers.nazwisko ;
            string calyHTML = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Content").ToString() + "/wzor.html");        
            DateTime dzisiaj = DateTime.Today;
            calyHTML = calyHTML.Replace("###DATA###", dzisiaj.ToString("d"));
           calyHTML= calyHTML.Replace("###IMIENAZWISKO###", imieNazwisko);
            //cos takiego ma wyjsc tu
            /*
             * <LI><P LANG="pl-PL" CLASS="western" STYLE="margin-bottom: 0in"><FONT FACE="Georgia, serif"><FONT SIZE=3>telefon
	komórkowy marki Samsung Galaxy S4; nr abonencki 607070928</FONT></FONT></P>
	<LI><P LANG="pl-PL" CLASS="western" STYLE="margin-bottom: 0in"><FONT FACE="Georgia, serif"><FONT SIZE=3><SPAN LANG="en-US">laptop
	marki Dell Precision M4700; nr seryjny 1PN7NX1</SPAN></FONT></FONT></P>
	<LI><P CLASS="western" STYLE="margin-bottom: 0in"><FONT FACE="Georgia, serif"><FONT SIZE=3>dysk
	przenośny ADATA 1TB</FONT></FONT></P>
             **/
            //jeden wpis <LI><P LANG="pl-PL" CLASS="western" STYLE="margin-bottom: 0in"><FONT FACE="Georgia, serif"><FONT SIZE=3>telefon
//	komórkowy marki Samsung Galaxy S4; nr abonencki 607070928</FONT></FONT></P>
            StringBuilder sb = new StringBuilder();
           
           for (int i = 0; i < lista.Count; i++)  //dobre
            {
                sb.Append("<LI><P LANG=\"pl-PL\" CLASS=\"western\" STYLE=\"margin-bottom: 0in\"><FONT FACE=\"Georgia, serif\"><FONT SIZE=3>" + lista[i].rodzajSprzetu +" "+ lista[i].marka +" "+ lista[i].model + "</FONT></FONT></P>"); //troche bylo cudzysłowów do ucieczki, heh
            }
                     
           calyHTML= calyHTML.Replace("###LISTA###", sb.ToString());
           var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(calyHTML);
           MemoryStream ms = new MemoryStream();
           ms.Write(pdfBytes, 0, pdfBytes.Length);
           ms.Position = 0;
           Stream fileStream = ms;          
           HttpContext.Response.AddHeader("content-disposition",
       "attachment; filename=form.pdf");
           return new FileStreamResult(fileStream, "application/pdf");
        }
      
        public ActionResult zrobPDFAdmin()
        {
            Entities daneDoModelu = new Entities();
            var SprzetUzytkownika = daneDoModelu.SprzetUzytkownika.Include(u => u.AspNetUsers);
            var pdf = new PdfResult(SprzetUzytkownika.ToList(), "Admin");         
            var view = ViewEngines.Engines.FindView(this.ControllerContext, "Admin", null);            
            return new RazorPDF.PdfResult(SprzetUzytkownika.ToList(),"Admin");
        }
    }
}