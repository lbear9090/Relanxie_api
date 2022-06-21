using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZXing;
using ZXing.Common;
using Avigma.Models;
using System.Text.RegularExpressions;
 
namespace Avigma.Repository.Lib
{
    public class QRCodeGenerator
    {
        Log log = new Log();
        private string GenerateQRCode(QRCodeModelDTO qRCodeModelDTO)
        {
            string imagePath = string.Empty;
            string DBimagePath = string.Empty;
            try
            {
                string folderPath = System.Configuration.ConfigurationManager.AppSettings["QRImagePath"];
                string strDBpath = System.Configuration.ConfigurationManager.AppSettings["QRImageDBPath"];
                var newfileName = Guid.NewGuid() + ".Jpeg";
                imagePath = folderPath + "\\" + newfileName;
                // If the directory doesn't exist then create it.
                //if (!Directory.Exists(HttpContext.Current.Server.MapPath(folderPath)))
                //{
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderPath));
                //}

                BarcodeWriter barcodeWriter = new BarcodeWriter();

                barcodeWriter.Format = BarcodeFormat.QR_CODE;
               
                var result = barcodeWriter.Write(qRCodeModelDTO.QRCodeText);

               // string barcodePath = HttpContext.Current.Server.MapPath(imagePath);
                string barcodePath = imagePath;
                Size size = new Size();
                size.Height = qRCodeModelDTO.QRCodeHeigth; 
                size.Width = qRCodeModelDTO.QRCodeWidth;
                var barcodeBitmap = new Bitmap(result,size);
               
              
                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(barcodePath, FileMode.Create, FileAccess.ReadWrite))
                    {
                         
                        barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                     
                       
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                        DBimagePath = strDBpath + newfileName;
                    }
                }

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);

                imagePath = ex.Message;
                
            }

            return DBimagePath;
        }


        public List<dynamic> GenerateQRImage(QRCodeModelDTO qRCodeModelDTO)
        {
            List<dynamic> objdynamicobj = new List<dynamic>();
           
            try
            {
                string imagePath = GenerateQRCode(qRCodeModelDTO);
                qRCodeModelDTO.QRCodeImagePath = imagePath;
                qRCodeModelDTO.QRCodeText = string.Empty;
                objdynamicobj.Add(qRCodeModelDTO);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                qRCodeModelDTO.QRCodeImagePath = ex.Message;
                qRCodeModelDTO.QRCodeText = string.Empty;
                objdynamicobj.Add(qRCodeModelDTO);

            }
            return objdynamicobj;

        }


    }
}