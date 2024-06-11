using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;

namespace QRCodeGenerate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the contact first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter the contact last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter the contact Phone Number:");
            string phone = Console.ReadLine();

            Console.WriteLine("Enter the contact Email:");
            string Email = Console.ReadLine();


            // Generate the vCard contact information
            string vCard = $"BEGIN:VCARD\nVERSION:3.0\nnN:{lastName};{firstName};;;\nFN:{firstName} {lastName}\nNICKNAME:{firstName}\nEMAIL:{Email}\nTEL;TYPE=cell:{phone}\nEND:VCARD";

            // Generate the QR code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(vCard, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(4);


            //// Save the QR code as a PNG image file
            //string fileName = data +"_"+ "QRCode.png";
            //qrCodeImage.Save(fileName, ImageFormat.Png);

            // Specify the folder path to save the QR code image
            string folderPath = @"D:\QRCODE";

            // Create the folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            // Load the overlay image using a relative path
            //string overlayImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Mostafa Radwan.png");
            //Bitmap overlayImage = new Bitmap(overlayImagePath);

            // Calculate the position to center the QR code on the overlay image
            int x = 0;
            int y = 0;

            // Create a graphics object to draw on the overlay image
            using (Graphics graphics = Graphics.FromImage(qrCodeImage))
            {
                // Draw the QR code onto the overlay image
                graphics.DrawImage(qrCodeImage, x, y, qrCodeImage.Width, qrCodeImage.Height);
            }


            // Save the final image
            string finalImagePath = Path.Combine(folderPath, firstName + "_" + "finalImage.png");
            qrCodeImage.Save(finalImagePath, ImageFormat.Png);

            // Display the QR code image using an image viewer application
            DisplayQRCodeImage(finalImagePath);

            Console.WriteLine("QR code with overlay image saved successfully!");
            Console.ReadLine();
        }
       
        static void DisplayQRCodeImage(string imagePath)
        {
            try
            {
                // Check if the file exists
                if (System.IO.File.Exists(imagePath))
                {
                    // Use the default image viewer to open and display the QR code image
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = imagePath,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
                else
                {
                    Console.WriteLine("QR code image not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

