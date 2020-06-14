using CinemaWpf.Model;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace CinemaWpf.Logic
{

    /// <summary>
    /// Class to generate tickets as pdf files
    /// </summary>
    public static class TicketManager
    {

        /// <summary>
        /// Generate pdf file with data from ticket model
        /// </summary>
        /// <param name="model">instance of Ticket class</param>
        public static void GenerateTicket(Ticket model)
        {
            using (PdfDocument document = new PdfDocument())
            {


                // Set the page size.
                document.PageSettings.Size = new SizeF(500,250);
                document.PageSettings.Orientation = PdfPageOrientation.Landscape;

                //Add a page to the document
                PdfPage page = document.Pages.Add();
                document.PageSettings.Margins.All = 0;

                //Create PDF graphics for a page
                PdfGraphics graphics = page.Graphics;

                //Create new PDF brush.
                PdfBrush brushSilver = new PdfSolidBrush(Color.FromArgb(212, 212, 220));
                PdfBrush brushDarkGrey = new PdfSolidBrush(Color.FromArgb(57, 63, 77));
                PdfBrush brushYellow = new PdfSolidBrush(Color.FromArgb(254, 218, 106));

                //Draw rectangle.
                graphics.DrawRectangle(brushDarkGrey, new RectangleF(0, 0, 500, 250));

                //Set the standard font
                PdfFont fontTitle = new PdfTrueTypeFont(new Font("Broadway", 40));
                PdfFont fontMovieTitle = new PdfTrueTypeFont(new Font("Letter Gothic Std", 25));
                PdfFont font = new PdfTrueTypeFont(new Font("Letter Gothic Std", 15));

                string title = "Kino Neptun";
                string movieTitle = model.MovieTitle;
                string rowNumber ="Rząd " + model.RowNumber.ToString();
                string seatNumber = "Miejsce " + model.SeatNumber.ToString();
                string showingDate = model.ShowingDateTime.ToString("dd MMM yyyy");
                string showingTime = model.ShowingDateTime.ToString("HH:mm");
                SizeF sizeTitle = fontTitle.MeasureString(title);
                SizeF sizeMovieTitle = fontMovieTitle.MeasureString(movieTitle);

                //Draw the text
                graphics.DrawString(title, fontTitle, brushSilver, new RectangleF(new PointF(20, 20), sizeTitle));
                graphics.DrawString(movieTitle, fontMovieTitle, brushYellow, new RectangleF(new PointF(20 , 80), sizeMovieTitle ));
                graphics.DrawString(rowNumber, font, brushSilver, new PointF(300, 120));
                graphics.DrawString(seatNumber, font, brushSilver, new PointF(300, 140));
                graphics.DrawString(showingDate, font, brushSilver, new PointF(20, 120));
                graphics.DrawString(showingTime, font, brushSilver, new PointF(20, 140));

                //Save the document
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string filepath = path + @"/Tickets/ticket_" + model.Id + ".pdf";
                document.Save(filepath);
            }
        }
    }
}
